using System;

namespace WpfApp.Models
{
    /// <summary>
	/// Order class.
	/// </summary>
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Time { get; set; }
        public string Destination { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// Constructors of order class.
        /// </summary>
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

        /// <summary>
        /// Overrided ToString method.
        /// </summary>
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", Id, Price, Time, Destination, Mobile, Status);
        }
    }
}
