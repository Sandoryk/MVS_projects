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
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Displays available tasks/links
        /// </summary>
        /// <returns>Json response</returns>
        public ActionResult Diagram()
        {
            DiagramAdapter DAdapter = new DiagramAdapter();
            JsonResult json = new JsonResult();

            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                IEnumerable<TaskWFM> gottenTasks = wfs.GetAllTasks();
                List<TaskViewModel> viewTasks = new List<TaskViewModel>();
                List<LinkViewModel> viewLinks = new List<LinkViewModel>();
                TaskViewModel taskView = null;
                LinkViewModel linkView = null;

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

                json = DAdapter.CreateJson(viewTasks, viewLinks);
            }
            return View(json);
        }

        /// <summary>
        /// Update Diagram tasks/links: insert/update/delete 
        /// </summary>
        /// <param name="form">Diagram data</param>
        /// <returns>XML response</returns>
        [HttpPost]
        public ContentResult SaveDiagramChanges(FormCollection form)
        {
            DiagramAdapter DAdapter = new DiagramAdapter();
            DAdapter.ParseJson(form, Request.QueryString["gantt_mode"]);
            
            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                DAdapter.MakeUpdate(wfs);
            }

            return Content(DAdapter.ResposeToDiagram().ToString(), "text/xml");
        }

        /// <summary>
        /// Displays tasks report 
        /// </summary>
        /// <returns>HTML response</returns>
        public ActionResult TasksReport()
        {
            List<TaskViewModel> tasks = new List<TaskViewModel>();

            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                List<TaskWFM> gottenTasks = wfs.GetAllTasks();
                if (gottenTasks.Count>0)
                {
                    TaskViewModel task = null;
                    foreach (var gottenTask in gottenTasks)
                    {
                        task = DataMapperView.DoMapping<TaskWFM, TaskViewModel>(gottenTask);
                        tasks.Add(task);
                    }
                }
                ViewBag.ProjectList = wfs.GetTasksProjects();
            }
            if (Request.IsAjaxRequest())
            {
                string holder = HttpContext.Request.Form["Holder"];
                string project = HttpContext.Request.Form["Project"];
                List<TaskViewModel> filteredtasks = tasks.
                    Where(t => (t.Holder == (String.IsNullOrEmpty(holder) ? t.Holder : holder) 
                        && t.Text == (String.IsNullOrEmpty(project) ? t.Text : project)))
                        .ToList();
                return PartialView("TasksReportPartial", filteredtasks);
            }
            return View(tasks);
        }
    }
}