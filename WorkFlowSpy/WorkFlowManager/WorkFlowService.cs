﻿using System;
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

        public List<TaskWFM> GetUserTasks(string userCode)
        {
            List<TaskWFM> outTaksList = new List<TaskWFM>();
            TaskWFM curTask = null;

            IEnumerable<TaskDB> dataTasks = dataSource.Tasks.GetByCondition(x => x.Holder == userCode);
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

        public void RemoveTask(TaskWFM task, bool removeF = true)
        {
            if (task != null)
            {
                TaskDB foundTask = dataSource.Tasks.GetByID(task.TaskId);
                if (foundTask != null)
                {
                    dataSource.Tasks.Delete(foundTask.TaskId);
                    if (removeF)
                    {
                        SaveAllChanges();
                    }
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
            if (link != null)
            {
                LinkDB foundLink = dataSource.Links.GetByID(link.LinkId);
                if (foundLink != null)
                {
                    dataSource.Links.Update(foundLink);
                }
                else
                {
                    LinkDB inputLink = DataMapperWFM.DoMapping<LinkWFM, LinkDB>(link);
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

        public void RemoveLink(LinkWFM link, bool removeF = true)
        {
            if (link != null)
            {
                LinkDB foundLink = dataSource.Links.GetByID(link.LinkId);
                if (foundLink != null)
                {
                    dataSource.Links.Delete(foundLink.LinkId);
                    if (removeF)
                    {
                        SaveAllChanges();
                    }
                }
            }
        }

        public int GetTaskIDByGUID(Guid guid)
        {
            int id = 0;

            TaskDB resTask = dataSource.Tasks.GetByCondition(t => t.GUID == guid).FirstOrDefault();
            if (resTask != null)
            {
                id = resTask.TaskId;

            }
            return id;
        }

        public int GetLinkIDByGUID(Guid guid)
        {
            int id = 0;

            LinkDB resLink = dataSource.Links.GetByCondition(t => t.GUID == guid).FirstOrDefault();
            if (resLink != null)
            {
                id = resLink.LinkId;

            }
            return id;
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
