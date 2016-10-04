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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            IList<string> roles = new List<string>();
            EmployeeViewModel employee = null;
            IEnumerable<TaskWFM> gottenTasks = null;

            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                {
                    ApplicationUser user = userManager.FindByName(User.Identity.Name);
                    if (user != null)
                    {
                        roles = userManager.GetRoles(user.Id);
                        EmployeeWFM employeeWMF = wfs.GetEmployeeByIdentityID(user.Id);
                        employee = DataMapperView.DoMapping<EmployeeWFM, EmployeeViewModel>(employeeWMF);
                    }                    
                }
                if (roles.Contains("admin") || roles.Contains("manager") || employee != null)
                {
                    if (roles.Contains("admin") || roles.Contains("manager"))
                    {
                        gottenTasks = wfs.GetAllTasks();
                    }
                    else
                    {
                        gottenTasks = wfs.GetEmployeeTasks(employee.HolderCode);
                    }
                    List<TaskViewModel> viewTasks = new ViewModelConverter().CreateTaskRange(gottenTasks);

                    IEnumerable<LinkWFM> gottenLinks = wfs.GetAllLinks();
                    List<LinkViewModel> viewLinks = new ViewModelConverter().CreateLinkRange(gottenLinks);

                    json = DAdapter.CreateJson(viewTasks, viewLinks);
                }
                else
                {
                    return View(json);
                }
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
            IList<string> roles = new List<string>();
            EmployeeViewModel employee = null;
            List<TaskWFM> gottenTasks = null;
            List<TaskViewModel> tasks = new List<TaskViewModel>();

            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
                {
                    ApplicationUser user = userManager.FindByName(User.Identity.Name);
                    if (user != null)
                    {
                        roles = userManager.GetRoles(user.Id);
                        EmployeeWFM employeeWMF = wfs.GetEmployeeByIdentityID(user.Id);
                        employee = DataMapperView.DoMapping<EmployeeWFM, EmployeeViewModel>(employeeWMF);
                    }
                }
                if (roles.Contains("admin") || roles.Contains("manager") || employee != null)
                {
                    if (roles.Contains("admin") || roles.Contains("manager"))
                    {
                        gottenTasks = wfs.GetAllTasks();
                    }
                    else
                    {
                        gottenTasks = wfs.GetEmployeeTasks(employee.HolderCode);
                    }
                }
                if (gottenTasks!=null && gottenTasks.Count > 0)
                {
                    tasks = new ViewModelConverter().CreateTaskTypedRange(gottenTasks);
                }
                ViewBag.ProjectList = wfs.GetTasksProjects();
                ViewBag.HolderList = wfs.GetTasksHolders();
            }
            if (Request.IsAjaxRequest())
            {
                string holder = HttpContext.Request.Form["Holder"];
                string project = HttpContext.Request.Form["Project"];
                List<TaskViewModel> filteredtasks = tasks.
                    Where(t => (t.Holder == (String.IsNullOrEmpty(holder) ? t.Holder : holder)
                        && t.ProjectName == (String.IsNullOrEmpty(project) ? t.ProjectName : project)))
                        .ToList();
                return PartialView("TasksReportPartial", filteredtasks);
            }
            return View(tasks);
        }
    }
}