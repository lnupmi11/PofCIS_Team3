using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Utils
{

    /// <summary>
	/// File manager class.
	/// </summary>
    class FileManager
    {
        private readonly string driversFile;
        private readonly string ordersFile;

        public List<TaxiDriver> TaxiDrivers { get; set; }
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Constructor of file manager class.
        /// </summary>
        /// <param name="df">Drivers file.</param>
        /// <param name="of">Orders file.</param>
        public FileManager(string df, string of)
        {
            driversFile = df;
            ordersFile = of;
            TaxiDrivers = new List<TaxiDriver>();
            Orders = new List<Order>();
        }

        /// <summary>
        /// Method to read orders from file.
        /// </summary>
        public void ReadOrders()
        {
            string[] ordersString = File.ReadAllLines(ordersFile);
            foreach (string line in ordersString)
            {
                string[] order = line.Split(' ');
                Orders.Add(new Order(Convert.ToInt32(order[0]), Convert.ToDouble(order[1]), order[2], order[3], order[4], order[5]));
            }
        }

        /// <summary>
        /// Method to write orders to file.
        /// </summary>
        public void WriteOrders()
        {
            using (StreamWriter writer = new StreamWriter(ordersFile))
            {
                foreach (Order order in Orders)
                {
                    writer.WriteLine(order);
                }
            }
        }

        /// <summary>
        /// Method to change order by id.
        /// </summary>
        public void ChangeOrder(int id, Order o)
        {
            for (int i = 0; i < Orders.Count; i++)
            {
                if (Orders[i].Id == id)
                {
                    Orders[i] = o;
                    break;
                }
            }
        }

        /// <summary>
        /// Method to read drivers from file.
        /// </summary>
        public void ReadDrivers()
        {
            string[] driversString = File.ReadAllLines(driversFile);
            foreach (string line in driversString)
            {
                string[] taxiDriver = line.Split(' ');
                int id = Convert.ToInt32(taxiDriver[0]);
                string taxiDriverName = taxiDriver[1];
                int count = Convert.ToInt32(taxiDriver[2]);
                List<int> ords = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    ords.Add(Convert.ToInt32(taxiDriver[i + 3]));
                }
                TaxiDrivers.Add(new TaxiDriver(id, taxiDriverName, count, ords));
            }
        }

        /// <summary>
        /// Method to write drivers to file.
        /// </summary>
        public void WriteDrivers()
        {
            using (StreamWriter writer = new StreamWriter(driversFile))
            {
                foreach (TaxiDriver taxiDriver in TaxiDrivers)
                {
                    writer.WriteLine(taxiDriver);
                }
            }
        }


        /// <summary>
        /// Method to change driver by id.
        /// </summary>
        public void ChangeDriver(int id, TaxiDriver td)
        {
            for (int i = 0; i < TaxiDrivers.Count; i++)
            {
                if (TaxiDrivers[i].Id == id)
                {
                    TaxiDrivers[i] = td;
                    break;
                }
            }
        }
    }
}
