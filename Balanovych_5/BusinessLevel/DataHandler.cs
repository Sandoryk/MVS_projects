using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLevel;

namespace BusinessLevel
{
    public class DataHandler
    {
        DataSource dataSource;
        public DataHandler(string con)
        {
            dataSource = new DataSource(con,ConnectionWay.EF);
        }

        public List<ItemBL> GetItemsByItemGroup(string itemGroup)
        {
            List<ItemBL> outItemsList = new List<ItemBL>();

            return outItemsList;
        }

        public List<SupplierBL> GetSuppliersByItemGroup(string itemGroup)
        {
            List<SupplierBL> suppliers = new List<SupplierBL>();


            return suppliers;
        }

        public List<ItemBL> GetItemsBySupplier(string supplier)
        {
            List<ItemBL> items = new List<ItemBL>();


            return items;
        }

        public Dictionary<SupplierBL,int> GetSuppliersWithItemsGroupsAmount()
        {
            Dictionary<SupplierBL, int> suppliersVsItemGroups = new Dictionary<SupplierBL, int>();


            return suppliersVsItemGroups;
        }

        public List<ItemBL> GetAllItems()
        {
            List<ItemBL> outItemsList = new List<ItemBL>();
            ItemBL currentItem = null;

            List<ItemDL> dataItems = dataSource.Items.FindAll();
            foreach (var item in dataItems)
            {
                currentItem = CustomDataMapper.DoMapping<ItemDL, ItemBL>(item);
                if (currentItem != null)
                {
                    outItemsList.Add(currentItem);
                }
            }

            return outItemsList;
        }

    }
}
