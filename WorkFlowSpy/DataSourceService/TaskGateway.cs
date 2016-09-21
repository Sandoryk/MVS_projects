using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSourceService.Interfaces;
using DataSourceService.DBModels;
using System.Data.Entity;

namespace DataSourceService
{
    public class TaskGateway : ITableDataGateway<TaskDB>
    {
        private WFlowContext db;

        public TaskGateway(WFlowContext context)
        {
            this.db = context;
        }

        public IEnumerable<TaskDB> GetAll()
        {
            return db.Tasks;
        }

        public TaskDB GetByID(int Id)
        {
            return db.Tasks.Find(Id);
        }

        public IEnumerable<TaskDB> GetByCondition(Func<TaskDB, bool> predicate)
        {
            return db.Tasks.Where(predicate);
        }

        public void Create(TaskDB task)
        {
            db.Tasks.Add(task);
        }

        public void Update(TaskDB task)
        {
            db.Entry(task).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            TaskDB task = db.Tasks.Find(Id);
            if (task != null)
                db.Tasks.Remove(task);
        }
    }
}
