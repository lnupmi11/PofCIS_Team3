using System;
using System.Collections.Generic;

namespace WpfApp.Models
{
    [Serializable]
    public class TaxiDriver
    {
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

        public TaxiDriver()
        {
            Name = "undefined";
        }

        public TaxiDriver(string name, int count, List<int> orders)
        {
            Name = name;
            CountOfOrders = count;
            orderIds = orders;
        }
    }
}
