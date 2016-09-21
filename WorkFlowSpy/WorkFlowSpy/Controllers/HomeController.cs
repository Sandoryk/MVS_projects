using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFlowManager;
using DataSourceService; //temp
using DataSourceService.DBModels; //temp

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
            /*using (WorkFlowService WFserv = new WorkFlowService("DefaultConnection"))
            {

            }*/

            /*using (DataSourceHandler dataSource = new DataSourceHandler("WorkFlowDbConnection"))
            {
                List<TaskDB> tasks = new List<TaskDB>()
                {
                    new TaskDB() { TaskId = 1, Text = "Project #2", StartDate = DateTime.Now.AddHours(-3),
                        Duration = 18, SortOrder = 10, Progress = 0.4m, ParentId = null },
                    new TaskDB() { TaskId = 2, Text = "Task #1", StartDate = DateTime.Now.AddHours(-2),
                        Duration = 8, SortOrder = 10, Progress = 0.6m, ParentId = 1 },
                    new TaskDB() { TaskId = 3, Text = "Task #2", StartDate = DateTime.Now.AddHours(-1),
                        Duration = 8, SortOrder = 20, Progress = 0.6m, ParentId = 1 }
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