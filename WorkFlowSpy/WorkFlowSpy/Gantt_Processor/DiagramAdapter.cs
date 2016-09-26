using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowSpy.Models;
using WorkFlowManager;
using WorkFlowManager.ModelsWFM;
using System.Threading;
using System.Globalization;
using System.Xml.Linq;

namespace WorkFlowSpy.Tools
{
    public enum DiagramMode
    {
        Tasks,
        Links
    }

    public enum DiagramAction
    {
        Inserted,
        Updated,
        Deleted,
        Error
    }

    public class DiagramAdapter
    {
        List<DiagramRequestModel> actions;

        public DiagramAdapter()
        {
            actions = new List<DiagramRequestModel>();
        }

        public List<DiagramRequestModel> RequestActions { get { return actions; } }

        public void ParseJson(FormCollection form, string diagramMode)
        {
            // save current culture and change it to InvariantCulture for data parsing
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var prefixes = form["ids"].Split(',');

            foreach (var prefix in prefixes)
            {
                var diagramRequest = new DiagramRequestModel();

                // lambda expression for form data parsing
                Func<string, string> parse = x => form[String.Format("{0}_{1}", prefix, x)];

                diagramRequest.Mode = (DiagramMode)Enum.Parse(typeof(DiagramMode), diagramMode, true);
                diagramRequest.Action = (DiagramAction)Enum.Parse(typeof(DiagramAction), parse("!nativeeditor_status"), true);
                diagramRequest.SourceId = Int64.Parse(parse("id"));

                // parse task
                if (diagramRequest.Action != DiagramAction.Deleted && diagramRequest.Mode == DiagramMode.Tasks)
                {
                    diagramRequest.UpdatedTask = new TaskViewModel()
                    {
                        TaskId = (diagramRequest.Action == DiagramAction.Updated) ? (int)diagramRequest.SourceId : 0,
                        Text = parse("text"),
                        StartDate = DateTime.Parse(parse("start_date")),
                        Duration = (String.IsNullOrEmpty(parse("duration"))) ? 0 : Int32.Parse(parse("duration")),
                        Progress = (String.IsNullOrEmpty(parse("progress"))) ? 0 : Decimal.Parse(parse("progress")),
                        ParentId = (parse("parent") != "0") ? Int32.Parse(parse("parent")) : (int?)null,
                        SortOrder = (parse("order") != null) ? Int32.Parse(parse("order")) : 0,
                        Type = parse("type"),
                        //AssignedToId = 
                    };
                }
                // parse link
                else if (diagramRequest.Action != DiagramAction.Deleted && diagramRequest.Mode == DiagramMode.Links)
                {
                    diagramRequest.UpdatedLink = new LinkViewModel()
                    {
                        LinkId = (diagramRequest.Action == DiagramAction.Updated) ? (int)diagramRequest.SourceId : 0,
                        SourceTaskId = Int32.Parse(parse("source")),
                        TargetTaskId = Int32.Parse(parse("target")),
                        Type = parse("type")
                    };
                }

                actions.Add(diagramRequest);
            }

            // return current culture back
            Thread.CurrentThread.CurrentCulture = currentCulture;
        }

        public JsonResult CreateJson(List<TaskViewModel> viewTasks, List<LinkViewModel> viewLinks)
        {
            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in viewTasks.AsEnumerable()
                    select new
                    {
                        id = t.TaskId,
                        text = t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.Duration,
                        order = t.SortOrder,
                        progress = t.Progress,
                        open = true,
                        parent = t.ParentId,
                        type = (t.Type != null) ? t.Type : String.Empty
                    }
                ).ToArray(),
                // create links array
                links = (
                    from l in viewLinks.AsEnumerable()
                    select new
                    {
                        id = l.LinkId,
                        source = l.SourceTaskId,
                        target = l.TargetTaskId,
                        type = l.Type
                    }
                ).ToArray()
            };
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Record updates came from diagram
        /// </summary>
        /// <param name="wfs">WorkFlowService object</param>
        public void MakeUpdate(WorkFlowService wfs)
        {
            try
            {
                if (actions.Count>0)
                {
                    foreach (var action in actions)
                    {
                        switch (action.Mode)
                        {
                            case DiagramMode.Tasks:
                                UpdateTasks(action, wfs);
                                break;
                            case DiagramMode.Links:
                                UpdateLinks(action, wfs);
                                break;
                        }
                    }

                    wfs.SaveAllChanges();

                    foreach (var action in actions)
                    {
                        if (action.Action == DiagramAction.Inserted)
                        {
                            switch (action.Mode)
                            {
                                case DiagramMode.Tasks:
                                    if (action.UpdatedTask.TaskId == 0)
                                    {
                                        action.UpdatedTask.TaskId = wfs.GetTaskIDByGUID(action.UpdatedTask.GUID);
                                    }
                                    break;
                                case DiagramMode.Links:
                                    if (action.UpdatedLink.LinkId == 0)
                                    {
                                        action.UpdatedLink.LinkId = wfs.GetLinkIDByGUID(action.UpdatedLink.GUID);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {
                // return error to client if something went wrong
                actions.ForEach(g => { g.Action = DiagramAction.Error; });
            }
        }

        /// <summary>
        /// Create XML response for Diagram
        /// </summary>
        /// <returns>XML response</returns>
        public XDocument ResposeToDiagram()
        {
            var outputActions = new List<XElement>();
            foreach (var diagramData in actions)
            {
                var action = new XElement("action");
                action.SetAttributeValue("type", diagramData.Action.ToString().ToLower());
                action.SetAttributeValue("sid", diagramData.SourceId);

                if (diagramData.Action == DiagramAction.Deleted)
                {
                    action.SetAttributeValue("tid", diagramData.SourceId);
                }
                else
                {
                    action.SetAttributeValue("tid", (diagramData.Mode == DiagramMode.Tasks) ? diagramData.UpdatedTask.TaskId : diagramData.UpdatedLink.LinkId);
                }

                //action.SetAttributeValue("tid", (diagramData.Mode == DiagramMode.Tasks) ? diagramData.UpdatedTask.TaskId : diagramData.UpdatedLink.LinkId);
                outputActions.Add(action);
            }

            var data = new XDocument(new XElement("data", outputActions));
            data.Declaration = new XDeclaration("1.0", "utf-8", "true");
            return data;
        }

        /// <summary>
        /// Update diagram tasks
        /// </summary>
        /// <param name="action">DiagramRequestModel object</param>
        /// <param wfs="diagramData">WorkFlowService object</param>
        private void UpdateTasks(DiagramRequestModel action, WorkFlowService wfs)
        {
            TaskWFM task = null;

            switch (action.Action)
            {
                case DiagramAction.Inserted:
                    // add new diagram task entity
                    if (action.UpdatedTask.GUID == Guid.Empty)
                    {
                        action.UpdatedTask.GUID = Guid.NewGuid();
                    }
                    task = DataMapperView.DoMapping<TaskViewModel, TaskWFM>(action.UpdatedTask);
                    wfs.SaveTask(task, false);
                    break;
                case DiagramAction.Deleted:
                    // remove diagram tasks
                    task = new TaskWFM { TaskId = (int)action.SourceId };
                    wfs.RemoveTask(task, false);
                    break;
                case DiagramAction.Updated:
                    // update diagram task
                    task = DataMapperView.DoMapping<TaskViewModel, TaskWFM>(action.UpdatedTask);
                    wfs.SaveTask(task, false);
                    break;
                default:
                    action.Action = DiagramAction.Error;
                    break;
            }
        }

        /// <summary>
        /// Update diagram links
        /// </summary>
        /// <param name="action">DiagramRequestModel object</param>
        /// <param name="wfs">WorkFlowService object</param>
        private void UpdateLinks(DiagramRequestModel action, WorkFlowService wfs)
        {
            LinkWFM link = null;

            switch (action.Action)
            {
                case DiagramAction.Inserted:
                    // add new diagram link
                    if (action.UpdatedLink.GUID == Guid.Empty)
                    {
                        action.UpdatedLink.GUID = Guid.NewGuid();
                    }
                    link = DataMapperView.DoMapping<LinkViewModel, LinkWFM>(action.UpdatedLink);
                    wfs.SaveLink(link, false);
                    break;
                case DiagramAction.Deleted:
                    // remove diagram link
                    link = new LinkWFM { LinkId = (int)action.SourceId };
                    wfs.RemoveLink(link, false);
                    break;
                case DiagramAction.Updated:
                    // update diagram link
                    link = DataMapperView.DoMapping<LinkViewModel, LinkWFM>(action.UpdatedLink);
                    wfs.SaveLink(link, false);
                    break;
                default:
                    action.Action = DiagramAction.Error;
                    break;
            }
        }
    }
}