using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowSpy.Models;
using WorkFlowManager;
using WorkFlowManager.ModelsWFM;
using DataSourceService; //temp
using DataSourceService.DBModels; //temp

namespace WorkFlowSpy.Controllers
{
    public class HomeController : Controller
    {
        private JsonResult MakeDiagramJson()
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
                       if (taskView!=null)
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Diagram()
        {
            ViewBag.Message = "Your application description page.";
            /*using (WorkFlowService WFserv = new WorkFlowService("DefaultConnection"))
            {

            }*/

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
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}