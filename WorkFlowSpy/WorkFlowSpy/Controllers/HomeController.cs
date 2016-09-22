using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowSpy.Models;
using WorkFlowManager;
using WorkFlowManager.ModelsWFM;
using WorkFlowSpy.Tools;
using System.Xml.Linq;

namespace WorkFlowSpy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Diagram()
        {
            ViewBag.Message = "Your application description page.";

            /*using (DataSourceHandler dataSource = new DataSourceHandler("WorkFlowDbConnection"))
            {
                List<TaskDB> tasks = new List<TaskDB>()
                {
                    new TaskDB() { TaskId = 1, Text = "Project #2", StartDate = DateTime.Now.AddHours(-3),
                        Duration = 18, SortOrder = 10, Progress = 0.4m, ParentId = null, AssignedToId = 1 },
                    new TaskDB() { TaskId = 2, Text = "Task #1", StartDate = DateTime.Now.AddHours(-2),
                        Duration = 8, SortOrder = 10, Progress = 0.6m, ParentId = 1, AssignedToId = 1 },
                    new TaskDB() { TaskId = 3, Text = "Task #2", StartDate = DateTime.Now.AddHours(-1),
                        Duration = 8, SortOrder = 20, Progress = 0.6m, ParentId = 1, AssignedToId = 2 }
                };

                tasks.ForEach(s => dataSource.Tasks.Create(s));
                dataSource.Save();
            }*/

            WorkFlowDiagramAdapter WFDadapter = new WorkFlowDiagramAdapter();
            JsonResult json = WFDadapter.MakeJson();
            return View(json);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Update Diagram tasks/links: insert/update/delete 
        /// </summary>
        /// <param name="form">Diagram data</param>
        /// <returns>XML response</returns>
        [HttpPost]
        public ContentResult SaveDiagramChanges(FormCollection form)
        {
            WorkFlowDiagramAdapter WFDadapter = new WorkFlowDiagramAdapter();
            var dataActions = WFDadapter.ParseJson(form, Request.QueryString["gantt_mode"]);
            try
            {
                using (WorkFlowService WFserv = new WorkFlowService("WorkFlowDbConnection"))
                {
                    foreach (var action in dataActions)
                    {
                        switch (action.Mode)
                        {
                            case DiagramMode.Tasks:
                                UpdateTasks(action, WFserv);
                                break;
                            case DiagramMode.Links:
                                UpdateLinks(action, WFserv);
                                break;
                        }
                    }
                    WFserv.SaveAllChanges();
                }
            }
            catch
            {
                // return error to client if something went wrong
                dataActions.ForEach(g => { g.Action = DiagramAction.Error; });
            }
            return ResposeToDiagram(dataActions);
        }

        /// <summary>
        /// Update diagram tasks
        /// </summary>
        /// <param name="diagramData">DiagramData object</param>
        private void UpdateTasks(WorkFlowDiagramRequestModel diagramData, WorkFlowService WFserv)
        {
            TaskWFM task = null;

            switch (diagramData.Action)
            {
                case DiagramAction.Inserted:
                    // add new diagram task entity
                    task = DataMapperView.DoMapping<TaskViewModel, TaskWFM>(diagramData.UpdatedTask);
                    WFserv.SaveTask(task,false);
                    break;
                case DiagramAction.Deleted:
                    // remove diagram tasks
                    task = new TaskWFM { TaskId = (int)diagramData.SourceId };
                    WFserv.RemoveTask(task, false);
                    break;
                case DiagramAction.Updated:
                    // update diagram task
                    task = DataMapperView.DoMapping<TaskViewModel, TaskWFM>(diagramData.UpdatedTask);
                    WFserv.SaveTask(task, false);
                    break;
                default:
                    diagramData.Action = DiagramAction.Error;
                    break;
            }
        }

        /// <summary>
        /// Update diagram links
        /// </summary>
        /// <param name="diagramData">DiagramData object</param>
        private void UpdateLinks(WorkFlowDiagramRequestModel diagramData, WorkFlowService WFserv)
        {
            LinkWFM link = null;

            switch (diagramData.Action)
            {
                case DiagramAction.Inserted:
                    // add new diagram link
                    link = DataMapperView.DoMapping<LinkViewModel, LinkWFM>(diagramData.UpdatedLink);
                    WFserv.SaveLink(link, false);
                    break;
                case DiagramAction.Deleted:
                    // remove diagram link
                    link = new LinkWFM { LinkId = (int)diagramData.SourceId };
                    WFserv.RemoveLink(link, false);
                    break;
                case DiagramAction.Updated:
                    // update diagram link
                    link = DataMapperView.DoMapping<LinkViewModel, LinkWFM>(diagramData.UpdatedLink);
                    WFserv.SaveLink(link, false);
                    break;
                default:
                    diagramData.Action = DiagramAction.Error;
                    break;
            }
        }

        /// <summary>
        /// Create XML response for Diagram
        /// </summary>
        /// <param name="diagramData">Diagram data</param>
        /// <returns>XML response</returns>
        private ContentResult ResposeToDiagram(List<WorkFlowDiagramRequestModel> diagramDataCollection)
        {
            var actions = new List<XElement>();
            foreach (var diagramData in diagramDataCollection)
            {
                var action = new XElement("action");
                action.SetAttributeValue("type", diagramData.Action.ToString().ToLower());
                action.SetAttributeValue("sid", diagramData.SourceId);
                action.SetAttributeValue("tid", (diagramData.Mode == DiagramMode.Tasks) ? diagramData.UpdatedTask.TaskId : diagramData.UpdatedLink.LinkId);
                actions.Add(action);
            }

            var data = new XDocument(new XElement("data", actions));
            data.Declaration = new XDeclaration("1.0", "utf-8", "true");
            return Content(data.ToString(), "text/xml");
        }
    }
}