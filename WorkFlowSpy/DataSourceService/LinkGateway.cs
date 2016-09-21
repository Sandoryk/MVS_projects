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
    public class LinkGateway : ITableDataGateway<LinkDB>
    {
        private WFlowContext db;

        public LinkGateway(WFlowContext context)
        {
            this.db = context;
        }

        public IEnumerable<LinkDB> GetAll()
        {
            return db.Links;
        }

        public LinkDB GetByID(int Id)
        {
            return db.Links.Find(Id);
        }

        public IEnumerable<LinkDB> GetByCondition(Func<LinkDB, bool> predicate)
        {
            return db.Links.Where(predicate);
        }

        public void Create(LinkDB link)
        {
            db.Links.Add(link);
        }

        public void Update(LinkDB link)
        {
            db.Entry(link).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            LinkDB link = db.Links.Find(Id);
            if (link != null)
                db.Links.Remove(link);
        }
    }
}
