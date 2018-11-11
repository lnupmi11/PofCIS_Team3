using System;

namespace WpfApp.Models
{
    [Serializable]
    public class Order
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

        public Order(int id, double price, string time, string destination, string mobile, string status)
        {
            Id = id;
            Price = price;
            Time = time;
            Destination = destination;
            Mobile = mobile;
            Status = status;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", Id, Price, Time, Destination, Mobile, Status);
        }
    }
}
