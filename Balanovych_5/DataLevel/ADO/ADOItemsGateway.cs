using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
            string sqlExpression = "SELECT * FROM Items";
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
                            if (!reader.IsDBNull(3))
                            {
                                item.ItemGroupID = reader.GetInt32(3);
                            }
                            if (!reader.IsDBNull(4))
                            {
                                item.SupplierID = reader.GetInt32(4);
                            }
                            
                            list.Add(item);
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

        public List<ItemDL> FindByCondition(Func<ItemDL, bool> predicate)
        {
            List<ItemDL> allItems = FindAll();
            List<ItemDL> items = allItems.Where(predicate).ToList();
            return items;
        }

        public ItemDL FindByID(int ID)
        {
            ItemDL item = null;
            string sqlExpression = "SELECT * FROM Items WHERE Items.ID = " + ID;
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
                            item = new ItemDL();
                            item.ID = reader.GetInt32(0);
                            item.Code = reader.GetString(1);
                            item.Name = reader.GetString(2);
                            if (!reader.IsDBNull(3))
                            {
                                item.ItemGroupID = reader.GetInt32(3);
                            }
                            if (!reader.IsDBNull(4))
                            {
                                item.SupplierID = reader.GetInt32(4);
                            }
                        }
                    }

                    reader.Close();
                }
            }
            catch (SqlNullValueException ex)
            {
                throw ex;

            }
            return item;
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
