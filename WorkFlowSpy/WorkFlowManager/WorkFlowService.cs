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

        public List<TaskWFM> GetUserTasks(int userId)
        {
            List<TaskWFM> outTaksList = new List<TaskWFM>();
            TaskWFM curTask = null;

            IEnumerable<TaskDB> dataTasks = dataSource.Tasks.GetByCondition(x => x.AssignedToId == userId);
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
                TaskDB foundTask = dataSource.Tasks.GetByID(task.TaskId);
                if (foundTask != null)
                {
                    dataSource.Tasks.Update(foundTask);
                }
                else
                {
                    TaskDB inputTask = DataMapperWFM.DoMapping<TaskWFM, TaskDB>(task);
                    dataSource.Tasks.Create(inputTask);
                }
                if (saveF)
                {
                    ForseSave();
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
                ForseSave();
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
                        ForseSave();
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
                    ForseSave();
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
                ForseSave();
            }
        }
        public void ForseSave()
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
                        ForseSave();
                    }
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
