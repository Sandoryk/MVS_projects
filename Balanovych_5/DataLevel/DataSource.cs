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

    class DataSource
    {
        string connectionStr;
        ITableDataGateway<ItemDL> items;
        ITableDataGateway<ItemGroupDL> itemGroups;
        ITableDataGateway<SupplierDL> suplliers;

        public DataSource(string con, ConnectionWay way)
        {
            connectionStr = con;
            connectionStr = @"Data Source=SANDORYK\SQLEXPRESS;Initial Catalog=Catalog;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            switch (way)
            {
                case ConnectionWay.ADO:
                    items = new ADOItemsGateway<ItemDL>(connectionStr);
                    break;
                case ConnectionWay.EF:
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
