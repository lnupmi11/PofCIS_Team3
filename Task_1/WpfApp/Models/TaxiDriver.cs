using System;
using System.Collections.Generic;

namespace WpfApp.Models
{
    [Serializable]
    public class TaxiDriver
    {
        public string Name { get; set; }

        public List<int> orderIds = new List<int>();

        public TaxiDriver()
        {
            Name = "undefined";
        }

        public TaxiDriver(string name, List<int> orders)
        {
            Name = name;
            orderIds = orders;
        }
    }
}
