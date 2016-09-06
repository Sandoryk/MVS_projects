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
                Console.WriteLine("\tGet all Items :\n");
                List<ItemBL> items = dataHandler.GetAllItems();
                foreach (var item in items)
                {
                    Console.WriteLine(item.ID + "\t" + item.Name);
                }

                Console.WriteLine("----------------------------\n");
                Console.WriteLine("\tGet all Suppliers :\n");
                List<SupplierBL> suppls = dataHandler.GetAllSuppliers();
                foreach (var suppl in suppls)
                {
                    Console.WriteLine(suppl.ID + "\t" + suppl.Name);
                }

                Console.WriteLine("----------------------------\n");
                Console.WriteLine("\tItems by item group \"TEA\":\n");
                items = dataHandler.GetItemsByItemGroup("TEA");
                foreach (var item in items)
                {
                    Console.WriteLine(item.ID + "\t" + item.Name);
                }

                Console.WriteLine("----------------------------\n");
                Console.WriteLine("\tSuppliers by item group \"TEA\":\n");
                suppls = dataHandler.GetSuppliersByItemGroup("TEA");
                foreach (var suppl in suppls)
                {
                    Console.WriteLine(suppl.ID + "\t" + suppl.Name);
                }

                Console.WriteLine("----------------------------\n");
                Console.WriteLine("\tItems by item supplier \"UNILEVER\":\n");
                items = dataHandler.GetItemsByItemGroup("TEA");
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
