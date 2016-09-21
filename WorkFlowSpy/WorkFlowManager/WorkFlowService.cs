using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSourceService;

namespace WFlowManager
{
    public class WorkFlowService : IDisposable
    {
        DataSourceHandler dataSource;
        private bool disposed = false;

        public WorkFlowService(string connectionStr)
        {
            dataSource = new DataSourceHandler(connectionStr);
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
