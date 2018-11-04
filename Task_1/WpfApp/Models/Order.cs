using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Time { get; set; }
        public string Destination { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }

        public Order()
        {
            Id = 0;
            Price = 0;
            Time = "";
            Mobile = "";
        }

        public Order(int id, double price, string time, string destination, string mobile)
        {
            Id = id;
            Price = price;
            Time = time;
            Mobile = mobile;
        }
    }
}
