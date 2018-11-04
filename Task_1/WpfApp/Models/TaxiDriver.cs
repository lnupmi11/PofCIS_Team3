using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    class TaxiDriver
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
