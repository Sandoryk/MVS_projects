using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public enum ConnectionWay
    {
        ADO = 1,
        EF
    }

    public class DataSource
    {
        string connectionStr;
        ITableDataGateway<ItemDL> items;
        ITableDataGateway<ItemGroupDL> itemGroups;
        ITableDataGateway<SupplierDL> suppliers;

        public DataSource(string con, ConnectionWay way)
        {
            connectionStr = con;
            switch (way)
            {
                case ConnectionWay.ADO:
                    items = new ADOItemsGateway(connectionStr);
                    itemGroups = new ADOItemGroupsGateway(connectionStr);
                    suppliers = new ADOSuppliersGateway(connectionStr);
                    break;
                case ConnectionWay.EF:
                    items = new EFItemsGateway(connectionStr);
                    itemGroups = new EFItemGroupsGateway(connectionStr);
                    suppliers = new EFSuppliersGateway(connectionStr);
                    break;
            }
        }

        public ITableDataGateway<ItemDL> Items
        {
            get { return items; }
        }

        public ITableDataGateway<ItemGroupDL> ItemGroups
        {
            get { return itemGroups; }
        }

        public ITableDataGateway<SupplierDL> Supplirers
        {
            get { return suppliers; }
        }
    }
}
