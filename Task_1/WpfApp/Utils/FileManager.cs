using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Utils
{
    class FileManager
    {
        private readonly string driversFile;
        private readonly string ordersFile;

        public List<TaxiDriver> TaxiDrivers { get; set; }
        public List<Order> Orders { get; set; }

        public FileManager(string df, string of)
        {
            driversFile = df;
            ordersFile = of;
            TaxiDrivers = new List<TaxiDriver>();
            Orders = new List<Order>();
        }

        public void ReadOrders()
        {
            string[] ordersString = File.ReadAllLines(ordersFile);
            foreach (string line in ordersString)
            {
                string[] order = line.Split(' ');
                Orders.Add(new Order(Convert.ToInt32(order[0]), Convert.ToDouble(order[1]), order[2], order[3], order[4], order[5]));
            }
        }

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

        public void ChangeOrder(Order o)
        {
            for (int i = 0; i < Orders.Count; ++i)
            {
                if (Orders[i].Id == o.Id)
                {
                    Orders[i] = o;
                    break;
                }
            }
        }

    }
}
