using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkFlowManager.ModelsWFM;
using WorkFlowSpy.Models;

namespace WorkFlowSpy.Tools
{
    public class ViewModelConverter
    {
        public List<TaskViewModel> CreateTaskRange(IEnumerable<TaskWFM> gottenTasks)
        {
            List<TaskViewModel> viewTasks = new List<TaskViewModel>();
            TaskViewModel taskView = null;

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

            return viewTasks;
        }
        public List<LinkViewModel> CreateLinkRange(IEnumerable<LinkWFM> gottenLinks)
        {
            List<LinkViewModel> viewLinks = new List<LinkViewModel>();
            LinkViewModel linkView = null;

            if (gottenLinks != null)
            {
                foreach (var link in gottenLinks)
                {
                    linkView = DataMapperView.DoMapping<LinkWFM, LinkViewModel>(link);
                    if (linkView != null)
                    {
                        viewLinks.Add(linkView);
                    }
                }
            }

            return viewLinks;
        }
        public List<EmployeeViewModel> CreateEmployeeRange(IEnumerable<EmployeeWFM> inputList)
        {
            List<EmployeeViewModel> viewEmployees = new List<EmployeeViewModel>();
            ApplicationUser user = null;
            EmployeeViewModel employeeView = null;

            using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                foreach (var inputEmployee in inputList)
                {
                    employeeView = DataMapperView.DoMapping<EmployeeWFM, EmployeeViewModel>(inputEmployee);
                    if (employeeView != null)
                    {
                        if (!String.IsNullOrEmpty(employeeView.IdentityId))
                        {
                            user = userManager.FindById(employeeView.IdentityId);
                            if (user != null)
                            {
                                employeeView.IdentityFirstName = user.FirstName;
                                employeeView.IdentityLastName = user.LastName;
                            }
                        }
                        viewEmployees.Add(employeeView);
                    }
                }
            }

            return viewEmployees;
        }

        public EmployeeViewModel CreateEmployee(EmployeeWFM inputEmployee)
        {
            ApplicationUser user = null;
            EmployeeViewModel employeeView = null;

            using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                employeeView = DataMapperView.DoMapping<EmployeeWFM, EmployeeViewModel>(inputEmployee);
                if (employeeView != null)
                {
                    if (!String.IsNullOrEmpty(employeeView.IdentityId))
                    {
                        user = userManager.FindById(employeeView.IdentityId);
                        if (user != null)
                        {
                            employeeView.IdentityFirstName = user.FirstName;
                            employeeView.IdentityLastName = user.LastName;
                        }
                    }
                }
            }

            return employeeView;
        }

        public Dictionary<string, string> GetUsersInfo()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            using (ApplicationDbContext appdb = new ApplicationDbContext())
            {
                IEnumerable<ApplicationUser> users = appdb.Users.ToList();
                foreach (var user in users)
                {
                    dict.Add(user.Id, user.FirstName + " " + user.LastName);
                }
            }

            return dict;
        }

        public List<TaskViewModel> CreateTaskTypedRange(IEnumerable<TaskWFM> inputTasks)
        {
            List<TaskViewModel> viewTasks = new List<TaskViewModel>();
            TaskViewModel taskView = null;

            foreach (var inputTask in inputTasks)
            {
                if (!String.IsNullOrEmpty(inputTask.Type) && inputTask.Type.ToLower().Contains("task"))
                {
                    taskView = DataMapperView.DoMapping<TaskWFM, TaskViewModel>(inputTask);
                    if (taskView != null)
                    {
                        if (taskView.ParentId!=null)
                        {
                            taskView.ProjectName = inputTasks.Where(t => t.TaskId == taskView.ParentId).First().Text;
                        }
                        viewTasks.Add(taskView);
                    }
                }
            }

            return viewTasks;
        }
    }
}