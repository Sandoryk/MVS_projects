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

    public class WorkFlowDiagramAdapter
    {
        public List<WorkFlowDiagramRequestModel> ParseJson(FormCollection form, string diagramMode)
        {
            // save current culture and change it to InvariantCulture for data parsing
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var diagramDataCollection = new List<WorkFlowDiagramRequestModel>();
            var prefixes = form["ids"].Split(',');

            foreach (var prefix in prefixes)
            {
                var diagramRequest = new WorkFlowDiagramRequestModel();

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
                        Duration = Int32.Parse(parse("duration")),
                        Progress = Decimal.Parse(parse("progress")),
                        ParentId = (parse("parent") != "0") ? Int32.Parse(parse("parent")) : (int?)null,
                        SortOrder = (parse("order") != null) ? Int32.Parse(parse("order")) : 0,
                        Type = parse("type")
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

                diagramDataCollection.Add(diagramRequest);
            }

            // return current culture back
            Thread.CurrentThread.CurrentCulture = currentCulture;

            return diagramDataCollection;
        }

        public JsonResult MakeJson()
        {
            List<TaskViewModel> viewTasks = new List<TaskViewModel>();
            List<LinkViewModel> viewLinks = new List<LinkViewModel>();
            TaskViewModel taskView = null;
            LinkViewModel linkView = null;

            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                IEnumerable<TaskWFM> gottenTasks = wfs.GetAllTasks();
                if (gottenTasks != null)
                {
                    foreach (var task in gottenTasks)
                    {
                        taskView = DataMapperView.DoMapping<TaskWFM, TaskViewModel>(task);
                        if (taskView != null)
                        {
                            viewTasks.Add(taskView);
                        }
                    }
                }
            }
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
    }
}