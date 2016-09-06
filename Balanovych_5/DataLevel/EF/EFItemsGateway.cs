using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class EFItemsGateway : ITableDataGateway<ItemDL>
    {
        string connectionStr;
        public EFItemsGateway(string con)
        {
            connectionStr = con;
        }
        public List<ItemDL> FindAll()
        {
            List<ItemDL> list = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                list = dBcont.Items.ToList<ItemDL>();
            }

            return list;
        }

        public ItemDL FindByID(string ID)
        {
            ItemDL item = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                item = dBcont.Items.Where(t => t.ID.ToString() == ID).FirstOrDefault();
            }

            return item;
        }

        public List<ItemDL> FindByCondition(Func<ItemDL, bool> predicate)
        {
            List<ItemDL> items = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                items = dBcont.Items.Where(predicate).ToList();
            }
            
            return items;
        }

        public bool Insert(ItemDL obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(ItemDL obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ItemDL obj)
        {
            throw new NotImplementedException();
        }
    }
}
