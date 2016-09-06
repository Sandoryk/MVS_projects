using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    class ADOSuppliersGateway : ITableDataGateway<SupplierDL>
    {
        string connectionStr;
        public ADOSuppliersGateway(string con) 
        {
            connectionStr = con;
        }

        public List<SupplierDL> FindAll()
        {
            List <SupplierDL> list = new List<SupplierDL>();
            string sqlExpression = "SELECT * FROM Suppliers";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        SupplierDL supl = null;
                        while (reader.Read())
                        {
                            supl = new SupplierDL();

                            supl.ID = reader.GetString(0);
                            supl.Name = reader.GetString(1);

                            list.Add(supl);
                        }
                    }

                    reader.Close();
                }
            }
            catch (SqlNullValueException ex)
            {
                throw ex;

            }
            return list;
        }

        public List<SupplierDL> FindByCondition(Func<SupplierDL, bool> predicate)
        {
            List<SupplierDL> allSuppliers = FindAll();
            List<SupplierDL> suppliers = allSuppliers.Where(predicate).ToList();
            return suppliers;
        }

        public SupplierDL FindByID(string ID)
        {
            SupplierDL supplier = null;
            string sqlExpression = "SELECT * FROM Suppliers WHERE Suppliers.ID = " + ID;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            supplier = new SupplierDL();
                            supplier.ID = reader.GetString(0);
                            supplier.Name = reader.GetString(1);
                        }
                    }

                    reader.Close();
                }
            }
            catch (SqlNullValueException ex)
            {
                throw ex;

            }
            return supplier;
        }

        public bool Insert(SupplierDL item)
        {
            throw new NotImplementedException();
        }

        public bool Update(SupplierDL item)
        {
            throw new NotImplementedException();
        }
        public bool Delete(SupplierDL item)
        {
            throw new NotImplementedException();
        }
    }
}
