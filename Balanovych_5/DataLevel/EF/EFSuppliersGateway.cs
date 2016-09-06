using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    class EFSuppliersGateway : ITableDataGateway<SupplierDL>
    {
        string connectionStr;
        public EFSuppliersGateway(string con)
        {
            connectionStr = con;
        }
        public List<SupplierDL> FindAll()
        {
            List<SupplierDL> list = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                list = dBcont.Suppliers.ToList<SupplierDL>();
            }

            return list;
        }

        public SupplierDL FindByID(int ID)
        {
            SupplierDL supplier = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                supplier = dBcont.Suppliers.Where(t => t.ID == ID).First();
            }

            return supplier;
        }

        public List<SupplierDL> FindByCondition(Func<SupplierDL, bool> predicate)
        {
            List<SupplierDL> suppliers = null;
            using (DBContext dBcont = new DBContext(connectionStr))
            {
                suppliers = dBcont.Suppliers.Where(predicate).ToList();
            }

            return suppliers;
        }

        public bool Insert(SupplierDL obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(SupplierDL obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(SupplierDL obj)
        {
            throw new NotImplementedException();
        }
    }
}
