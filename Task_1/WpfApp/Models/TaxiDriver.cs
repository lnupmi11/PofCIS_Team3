using System;
using System.Collections.Generic;

namespace WpfApp.Models
{
    /// <summary>
	/// Taxi driver class.
	/// </summary>
    [Serializable]
    public class TaxiDriver
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountOfOrders { get; set; }

        public List<int> orderIds = new List<int>();
        public string OrderIds
        {
            get
            {
                return string.Join(", ", orderIds);
            }
        }

        /// <summary>
        /// Constructors of taxi driver class.
        /// </summary>
        public TaxiDriver()
        {
            Id = 0;
            Name = "undefined";
        }

        public TaxiDriver(int id, string name, int count, List<int> orders)
        {
            Id = id;
            Name = name;
            CountOfOrders = count;
            orderIds = orders;
        }
        /// <summary>
        /// Assign order to the driver's orders list.
        /// </summary>
        /// <param name="or">Order to be assigned.</param>
        public void AssignOrder(Order or)
        {
            CountOfOrders++;
            orderIds.Add(or.Id);
        }

        /// <summary>
        /// Overrided ToString method.
        /// </summary>
        public override string ToString()
        {
            string s = String.Format("{0} {1} {2}", Id, Name, CountOfOrders);
            for (int i = 0; i < CountOfOrders; i++)
            {
                s += " ";
                s += orderIds[i];
            }
            return s;
        }
        
    }
}
