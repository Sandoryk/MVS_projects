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
    public class ADOItemsGateway : ITableDataGateway<ItemDL>
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
                            item.Name = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                            {
                                item.ItemGroupID = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                item.SupplierID = reader.GetString(3);
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

        public ItemDL FindByID(string ID)
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
                            item.Name = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                            {
                                item.ItemGroupID = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                item.SupplierID = reader.GetString(3);
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
