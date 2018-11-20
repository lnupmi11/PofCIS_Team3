namespace WpfApp.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Taxi driver class.
    /// </summary>
    [Serializable]
    public class TaxiDriver
    {
        public TaxiDriver()
        {
            this.Id = 0;
            this.Name = "undefined";
        }

        public TaxiDriver(int id, string name, int count, List<int> orders)
        {
            this.Id = id;
            this.Name = name;
            this.CountOfOrders = count;
            this.orderIds = orders;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CountOfOrders { get; set; }

        public List<int> orderIds = new List<int>();

        public string OrderIds
        {
            get
            {
                return string.Join(", ", this.orderIds);
            }
        }

        /// <summary>
        /// Assign order to the driver's orders list.
        /// </summary>
        /// <param name="or">Order to be assigned.</param>
        public void AssignOrder(Order or)
        {
            this.CountOfOrders++;
            this.orderIds.Add(or.Id);
        }

        /// <summary>
        /// Overrided ToString method.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            string s = string.Format("{0} {1} {2}", this.Id, this.Name, this.CountOfOrders);
            for (int i = 0; i < this.CountOfOrders; i++)
            {
                s += " ";
                s += this.orderIds[i];
            }

            return s;
        }
    }
}
