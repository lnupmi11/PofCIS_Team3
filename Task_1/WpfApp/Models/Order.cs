namespace WpfApp.Models
{
    using System;

    /// <summary>
    /// Order class.
    /// </summary>
    [Serializable]
    public class Order
    {
        public Order()
        {
            this.Id = 0;
            this.Price = 0;
            this.Time = string.Empty;
            this.Destination = string.Empty;
            this.Mobile = string.Empty;
            this.Status = string.Empty;
        }

        public Order(int id, double price, string time, string destination, string mobile, string status)
        {
            this.Id = id;
            this.Price = price;
            this.Time = time;
            this.Destination = destination;
            this.Mobile = mobile;
            this.Status = status;
        }

        public int Id { get; set; }

        public double Price { get; set; }

        public string Time { get; set; }

        public string Destination { get; set; }

        public string Mobile { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// Set's status to already assigned.
        /// </summary>
        public void GetAssigned()
        {
            if (this.Status != "not assigned")
            {
                throw new Exception("Order was already assigned or done!");
            }

            this.Status = "already assigned";
        }

        /// <summary>
        /// Overrided ToString method.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", this.Id, this.Price, this.Time, this.Destination, this.Mobile, this.Status);
        }
    }
}
