namespace WpfApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Order class.
    /// </summary>
    [Serializable]
    [Table("Orders")]
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

        [Key]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [MaxLength(10)]
        [MinLength(3)]
        [Required]
        public string Time { get; set; }

        [MaxLength(70)]
        [Required]
        public string Destination { get; set; }

        [MaxLength(15)]
        [Required]
        public string Mobile { get; set; }

        [MaxLength(20)]
        [Required]
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
