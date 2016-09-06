using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLevel;
using System.Data.SqlTypes;

namespace Balanovych_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Alex\Documents\GitHub\MVS_projects\Balanovych_5\Balanovych_5\Catalog.mdf;Integrated Security=True";
            //connectionStr = @"Data Source = SANDORYK\SQLEXPRESS; Initial Catalog = Catalog; Integrated Security = True";
            DataHandler dataHandler = new DataHandler(connectionStr);
            try
            {
                List<ItemBL> items = dataHandler.GetAllItems();
                foreach (var item in items)
                {
                    Console.WriteLine(item.ID + "\t" + item.Name);
                }
            }
            catch (SqlNullValueException ex)
            {
                
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\n\nPress any key to close application");
            Console.ReadKey();
        }
    }
}
