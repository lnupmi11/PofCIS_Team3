using System;
using System.Collections.Generic;

namespace WpfApp.Models
{
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
    }
}
