namespace WpfApp.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using WpfApp.Models;

    /// <summary>
    /// File manager class.
    /// </summary>
    public class FileManager
    {
        private readonly string driversFile;
        private readonly string ordersFile;

        public FileManager(string df, string of)
        {
            this.driversFile = df;
            this.ordersFile = of;
            this.TaxiDrivers = new List<TaxiDriver>();
            this.Orders = new List<Order>();
        }

        public List<TaxiDriver> TaxiDrivers { get; set; }

        public List<Order> Orders { get; set; }

        /// <summary>
        /// Method to read orders from file.
        /// </summary>
        public void ReadOrders()
        {
            string[] ordersString = File.ReadAllLines(this.ordersFile);
            foreach (string line in ordersString)
            {
                string[] order = line.Split(' ');
                this.Orders.Add(new Order(Convert.ToInt32(order[0]), Convert.ToDouble(order[1]), order[2], order[3], order[4], order[5]));
            }
        }

        /// <summary>
        /// Method to write orders to file.
        /// </summary>
        public void WriteOrders()
        {
            using (StreamWriter writer = new StreamWriter(this.ordersFile))
            {
                foreach (Order order in this.Orders)
                {
                    writer.WriteLine(order);
                }
            }
        }

        /// <summary>
        /// Method to change order by id.
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="o">Order</param>
        public void ChangeOrder(int id, Order o)
        {
            for (int i = 0; i < this.Orders.Count; i++)
            {
                if (this.Orders[i].Id == id)
                {
                    this.Orders[i] = o;
                    break;
                }
            }
        }

        /// <summary>
        /// Method to read drivers from file.
        /// </summary>
        public void ReadDrivers()
        {
            string[] driversString = File.ReadAllLines(this.driversFile);
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

                this.TaxiDrivers.Add(new TaxiDriver(id, taxiDriverName, count, ords));
            }
        }

        /// <summary>
        /// Method to write drivers to file.
        /// </summary>
        public void WriteDrivers()
        {
            using (StreamWriter writer = new StreamWriter(this.driversFile))
            {
                foreach (TaxiDriver taxiDriver in this.TaxiDrivers)
                {
                    writer.WriteLine(taxiDriver);
                }
            }
        }

        /// <summary>
        /// Method to change driver by id.
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="td">TaxiDriver</param>
        public void ChangeDriver(int id, TaxiDriver td)
        {
            for (int i = 0; i < this.TaxiDrivers.Count; i++)
            {
                if (this.TaxiDrivers[i].Id == id)
                {
                    this.TaxiDrivers[i] = td;
                    break;
                }
            }
        }
    }
}
