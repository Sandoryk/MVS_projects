using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    class EFItemGroupsGateway : ITableDataGateway<ItemGroupDL>
    {
        string connectionStr;
        public EFItemGroupsGateway(string con)
        {
            connectionStr = con;
        }
        public List<ItemGroupDL> FindAll()
        {
            List<ItemGroupDL> list = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                list = dBcont.ItemGroups.ToList<ItemGroupDL>();
            }

            return list;
        }

        public ItemGroupDL FindByID(int ID)
        {
            ItemGroupDL group = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                group = dBcont.ItemGroups.Where(t => t.ID == ID).First();
            }

            return group;
        }

        public List<ItemGroupDL> FindByCondition(Func<ItemGroupDL, bool> predicate)
        {
            List<ItemGroupDL> groups = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                groups = dBcont.ItemGroups.Where(predicate).ToList();
            }

            return groups;
        }

        public bool Insert(ItemGroupDL obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(ItemGroupDL obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ItemGroupDL obj)
        {
            throw new NotImplementedException();
        }
    }
}
