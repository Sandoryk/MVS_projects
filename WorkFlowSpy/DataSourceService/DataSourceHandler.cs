using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceService
{
    public class DataSourceHandler : IDisposable
    {
        private WFlowContext db;
        private TaskGateway taskGateway;
        private LinkGateway linkGateway;
        private EmployeeGateWay employeeGateway;
        private bool disposed = false;

        public DataSourceHandler(string connectionStr)
        {
            db = new WFlowContext(connectionStr);
        }

        public TaskGateway Tasks
        {
            get
            {
                if (taskGateway == null)
                    taskGateway = new TaskGateway(db);
                return taskGateway;
            }
        }

        public LinkGateway Links
        {
            get
            {
                if (linkGateway == null)
                    linkGateway = new LinkGateway(db);
                return linkGateway;
            }
        }

        public EmployeeGateWay Employes
        {
            get
            {
                if (employeeGateway == null)
                    employeeGateway = new EmployeeGateWay(db);
                return employeeGateway;
            }
        }
        
        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
