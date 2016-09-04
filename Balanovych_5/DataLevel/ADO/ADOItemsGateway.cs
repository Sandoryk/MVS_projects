using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class ADOItemsGateway<ItemsDL> : ITableDataGateway<ItemDL>
    {
        string connectionStr;
        public ADOItemsGateway(string con)
        {
            connectionStr = con;
        }

        public List<ItemDL> FindAll()
        {
            List <ItemDL> list = new List<ItemDL>();
            string sqlExpression = "SELECT * FROM Users";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        ItemDL item = null;
                        while (reader.Read())
                        {
                            item = new ItemDL();

                            item.ID = reader.GetInt32(0);
                            item.Code = reader.GetString(1);
                            item.Name = reader.GetString(2);
                            item.ItemGroupID = reader.GetInt32(3);
                            item.SupplierID = reader.GetInt32(4);

                            list.Add(item);
                        }
                    }

                    reader.Close();
                }
            }
            catch
            {
                throw new Exception();

            }
            return list;
        }

        public List<ItemDL> FindByCondition(Expression<Func<ItemDL, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ItemDL FindByID(string ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ItemDL item)
        {
            throw new NotImplementedException();
        }

        public bool Update(ItemDL item)
        {
            throw new NotImplementedException();
        }
        public bool Delete(ItemDL item)
        {
            throw new NotImplementedException();
        }
    }
}
