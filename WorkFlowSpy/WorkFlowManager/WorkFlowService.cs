using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSourceService;
using DataSourceService.DBModels;
using WFlowManager.ModelsWFM;

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

        public List<TaskWFM> GetAlltasks()
        {
            List<TaskWFM> outTaksList = new List<TaskWFM>();
            TaskWFM curTask = null;

            IEnumerable<TaskDB> dataItems = dataSource.Items.FindAll();
            foreach (var item in dataItems)
            {
               /* currentItem = CustomDataMapper.DoMapping<ItemDL, ItemBL>(item);
                if (currentItem != null)
                {
                    outItemsList.Add(currentItem);
                }*/
            }

            return outTaksList;
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
