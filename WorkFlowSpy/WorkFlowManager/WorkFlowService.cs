using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSourceService;
using DataSourceService.DBModels;
using WorkFlowManager.ModelsWFM;

namespace WorkFlowManager
{
    public class WorkFlowService : IDisposable
    {
        DataSourceHandler dataSource;
        private bool disposed = false;

        public WorkFlowService(string connectionStr)
        {
            dataSource = new DataSourceHandler(connectionStr);
        }

        public List<TaskWFM> GetAllTasks()
        {
            List<TaskWFM> outTaskList = new List<TaskWFM>();
            TaskWFM curTask = null;

            IEnumerable<TaskDB> dataTasks = dataSource.Tasks.GetAll();
            foreach (var dataTask in dataTasks)
            {
                curTask = DataMapperWFM.DoMapping<TaskDB, TaskWFM>(dataTask);
                if (curTask != null)
                {
                    outTaskList.Add(curTask);
                }
            }

            return outTaskList;
        }

        public string[] GetTasksProjects()
        {
            string[] projects = new string[0];

            IEnumerable<TaskDB> tasks = dataSource.Tasks.GetByCondition(t => !String.IsNullOrEmpty(t.Type) && t.Type.ToLower().Contains("project"));
            if (tasks!=null && tasks.Count() > 0)
	        {
                projects = tasks.Select(t => t.Text)
                .Distinct()
                .OrderBy(Text => Text)
                .ToArray();
	        }


            return projects;
        }

        public string[] GetTasksHolders()
        {
            string[] holders = new string[0];

            IEnumerable<TaskDB> tasks = dataSource.Tasks.GetByCondition(t => !String.IsNullOrEmpty(t.Holder));
            if (tasks != null && tasks.Count() > 0)
            {
                holders = tasks.Select(t => t.Holder)
                .Distinct()
                .OrderBy(Text => Text)
                .ToArray();
            }


            return holders;
        }

        public List<TaskWFM> GetEmployeeTasks(string HolderCode)
        {
            List<TaskWFM> outTaksList = new List<TaskWFM>();
            TaskWFM curTask = null;

            IEnumerable<TaskDB> dataTasks = dataSource.Tasks.GetByCondition(x => x.Holder == HolderCode);
            foreach (var dataTask in dataTasks)
            {
                curTask = DataMapperWFM.DoMapping<TaskDB, TaskWFM>(dataTask);
                if (curTask != null)
                {
                    outTaksList.Add(curTask);
                }
            }

            return outTaksList;
        }

        public void SaveTask(TaskWFM task, bool saveF = true)
        {
            if (task != null)
            {
                TaskDB inputTask = null;

                TaskDB foundTask = dataSource.Tasks.GetByID(task.TaskId);
                if (foundTask != null)
                {
                    inputTask = DataMapperWFM.DoMapping<TaskWFM, TaskDB>(task);
                    dataSource.Tasks.Update(inputTask);
                }
                else
                {
                    inputTask = DataMapperWFM.DoMapping<TaskWFM, TaskDB>(task);
                    dataSource.Tasks.Create(inputTask);
                }
                if (saveF)
                {
                    SaveAllChanges();
                }
            }
        }

        public void SaveRangeTasks(IEnumerable<TaskWFM> tasks)
        {
            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    SaveTask(task,false);
                }
                SaveAllChanges();
            }
        }

        public void RemoveTask(int id, bool removeF = true)
        {
            TaskDB foundTask = dataSource.Tasks.GetByID(id);
            if (foundTask != null)
            {
                dataSource.Tasks.Delete(foundTask.TaskId);
                if (removeF)
                {
                    SaveAllChanges();
                }
            }
        }

        public List<LinkWFM> GetAllLinks()
        {
            List<LinkWFM> outTaksList = new List<LinkWFM>();
            LinkWFM curLink = null;

            IEnumerable<LinkDB> dataLinks = dataSource.Links.GetAll();
            foreach (var dataTask in dataLinks)
            {
                curLink = DataMapperWFM.DoMapping<LinkDB, LinkWFM>(dataTask);
                if (curLink != null)
                {
                    outTaksList.Add(curLink);
                }
            }

            return outTaksList;
        }

        public void SaveLink(LinkWFM link, bool saveF = true)
        {
            LinkDB inputLink = null;

            if (link != null)
            {
                LinkDB foundLink = dataSource.Links.GetByID(link.LinkId);
                if (foundLink != null)
                {
                    inputLink = DataMapperWFM.DoMapping<LinkWFM, LinkDB>(link);
                    dataSource.Links.Update(inputLink);
                }
                else
                {
                    inputLink = DataMapperWFM.DoMapping<LinkWFM, LinkDB>(link);
                    dataSource.Links.Create(inputLink);
                }
                if (saveF)
                {
                    SaveAllChanges();
                }
            }
        }

        public void SaveRangeLinks(IEnumerable<LinkWFM> links)
        {
            if (links != null)
            {
                foreach (var link in links)
                {
                    SaveLink(link, false);
                }
                SaveAllChanges();
            }
        }
        public void SaveAllChanges()
        {
            dataSource.Save();
        }

        public void RemoveLink(int id, bool removeF = true)
        {

            LinkDB foundLink = dataSource.Links.GetByID(id);
            if (foundLink != null)
            {
                dataSource.Links.Delete(foundLink.LinkId);
                if (removeF)
                {
                    SaveAllChanges();
                }
            }
        }

        public int GetTaskIDByGUID(Guid guid)
        {
            int id = 0;

            TaskDB gottenTask = dataSource.Tasks.GetByCondition(t => t.GUID == guid).FirstOrDefault();
            if (gottenTask != null)
            {
                id = gottenTask.TaskId;

            }
            return id;
        }

        public int GetLinkIDByGUID(Guid guid)
        {
            int id = 0;

            LinkDB gottenLink = dataSource.Links.GetByCondition(t => t.GUID == guid).FirstOrDefault();
            if (gottenLink != null)
            {
                id = gottenLink.LinkId;

            }
            return id;
        }

        public List<EmployeeWFM> GetAllEmployees()
        {
            List<EmployeeWFM> outEmployeeList = new List<EmployeeWFM>();
            EmployeeWFM curEmployee = null;

            IEnumerable<EmployeeDB> dataEmployees = dataSource.Employees.GetAll();
            foreach (var dataTask in dataEmployees)
            {
                curEmployee = DataMapperWFM.DoMapping<EmployeeDB, EmployeeWFM>(dataTask);
                if (curEmployee != null)
                {
                    outEmployeeList.Add(curEmployee);
                }
            }

            return outEmployeeList;
        }

        public void SaveEmployee(EmployeeWFM employee, bool saveF = true)
        {
            EmployeeDB inputEmployee = null;

            if (employee != null)
            {
                EmployeeDB foundEmployee = dataSource.Employees.GetByID(employee.Id);
                if (foundEmployee != null)
                {
                    inputEmployee = DataMapperWFM.DoMapping<EmployeeWFM, EmployeeDB>(employee);
                    dataSource.Employees.Update(inputEmployee);
                }
                else
                {
                    inputEmployee = DataMapperWFM.DoMapping<EmployeeWFM, EmployeeDB>(employee);
                    dataSource.Employees.Create(inputEmployee);
                }
                if (saveF)
                {
                    SaveAllChanges();
                }
            }
        }

        public EmployeeWFM GetEmployeeByID(int id)
        {
            EmployeeWFM empoyee = null;

            EmployeeDB gottenEmployee = dataSource.Employees.GetByID(id);
            if (gottenEmployee!=null)
            {
                empoyee = DataMapperWFM.DoMapping<EmployeeDB,EmployeeWFM>(gottenEmployee);
            }

            return empoyee;
        }

        public void RemoveEmployee(int id, bool removeF = true)
        {
            EmployeeDB foundEmployee = dataSource.Employees.GetByID(id);
            if (foundEmployee != null)
            {
                dataSource.Employees.Delete(foundEmployee.Id);
                if (removeF)
                {
                    SaveAllChanges();
                }
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dataSource.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
