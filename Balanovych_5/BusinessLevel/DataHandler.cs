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
            ItemBL currentItem = null;
            
            List<ItemDL> dataItems = dataSource.Items.FindByCondition(t => t.ItemGroupID == itemGroup.PadRight(10));
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

        public List<SupplierBL> GetSuppliersByItemGroup(string itemGroup)
        {
            List<SupplierBL> suppliers = new List<SupplierBL>();
            List<String> supIDs = new List<String>();
            SupplierDL inSuppl = null;
            SupplierBL outSuppl = null;

            List<ItemDL> dataItems = dataSource.Items.FindByCondition(t => t.ItemGroupID == itemGroup.PadRight(10));
            foreach (var item in dataItems)
            {
                if (supIDs.Contains(item.SupplierID)==false)
                {
                    supIDs.Add(item.SupplierID);
                }
            }

            foreach (var supID in supIDs)
            {
                inSuppl = dataSource.Suppliers.FindByID(supID);
                if (inSuppl!=null)
                {
                    outSuppl = CustomDataMapper.DoMapping<SupplierDL, SupplierBL>(inSuppl);
                    suppliers.Add(outSuppl);
                }
            }

            return suppliers;
        }

        public List<ItemBL> GetItemsBySupplier(string supplier)
        {
            List<ItemBL> outItemsList = new List<ItemBL>();
            ItemBL currentItem = null;

            List<ItemDL> dataItems = dataSource.Items.FindByCondition(t => t.SupplierID == supplier.PadRight(10));
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
        public List<SupplierBL> GetAllSuppliers()
        {
            List<SupplierBL> outSuppliersList = new List<SupplierBL>();
            SupplierBL currentSupplier = null;

            List<SupplierDL> dataSuppliers = dataSource.Suppliers.FindAll();
            foreach (var suppl in dataSuppliers)
            {
                currentSupplier = CustomDataMapper.DoMapping<SupplierDL, SupplierBL>(suppl);
                if (currentSupplier != null)
                {
                    outSuppliersList.Add(currentSupplier);
                }
            }

            return outSuppliersList;
        }
    }
}
