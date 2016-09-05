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
        ITableDataGateway<SupplierDL> suplliers;

        public DataSource(string con, ConnectionWay way)
        {
            connectionStr = con;
            switch (way)
            {
                case ConnectionWay.ADO:
                    items = new ADOItemsGateway<ItemDL>(connectionStr);
                    break;
                case ConnectionWay.EF:
                    items = new EFItemsGateway<ItemDL>(connectionStr);
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
            get { return suplliers; }
        }
    }
}
