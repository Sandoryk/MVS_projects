using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class ADOItemGroupsGateway : ITableDataGateway<ItemGroupDL>
    {
        string connectionStr;
        public ADOItemGroupsGateway(string con) 
        {
            connectionStr = con;
        }

        public List<ItemGroupDL> FindAll()
        {
            List <ItemGroupDL> list = new List<ItemGroupDL>();
            string sqlExpression = "SELECT * FROM ItemGroups";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        ItemGroupDL group = null;
                        while (reader.Read())
                        {
                            group = new ItemGroupDL();

                            group.ID = reader.GetString(0);
                            group.Name = reader.GetString(1);

                            list.Add(group);
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

        public List<ItemGroupDL> FindByCondition(Func<ItemGroupDL, bool> predicate)
        {
            List<ItemGroupDL> allItemGroups = FindAll();
            List<ItemGroupDL> groups = allItemGroups.Where(predicate).ToList();
            return groups;
        }

        public ItemGroupDL FindByID(string ID)
        {
            ItemGroupDL group = null;
            string sqlExpression = "SELECT * FROM ItemGroups WHERE ItemGroups.ID = " + ID;
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
                            group = new ItemGroupDL();
                            group.ID = reader.GetString(0);
                            group.Name = reader.GetString(1);
                        }
                    }

                    reader.Close();
                }
            }
            catch (SqlNullValueException ex)
            {
                throw ex;

            }
            return group;
        }

        public bool Insert(ItemGroupDL item)
        {
            throw new NotImplementedException();
        }

        public bool Update(ItemGroupDL item)
        {
            throw new NotImplementedException();
        }
        public bool Delete(ItemGroupDL item)
        {
            throw new NotImplementedException();
        }
    }
}
