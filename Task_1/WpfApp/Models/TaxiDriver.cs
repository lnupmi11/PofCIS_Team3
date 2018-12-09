namespace WpfApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Taxi driver class.
    /// </summary>
    [Serializable]
    [Table("Orders")]
    public class TaxiDriver
    {
        public TaxiDriver()
        {
            this.Id = 0;
            this.Name = "undefined";
        }

        public TaxiDriver(int id, string name, int count, List<int> orders)
        {
            this.Id = id;
            this.Name = name;
            this.CountOfOrders = count;
            this.OrderIdValues = orders;
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        
        [Required]
        public int CountOfOrders { get; set; }

        [Required]
        public List<int> OrderIdValues = new List<int>();

        public string OrderIds
        {
            get
            {
                return string.Join(", ", this.OrderIdValues);
            }
        }

        /// <summary>
        /// Assign order to the driver's orders list.
        /// </summary>
        /// <param name="or">Order to be assigned.</param>
        public void AssignOrder(Order or)
        {
            this.CountOfOrders++;
            this.OrderIdValues.Add(or.Id);
        }

        /// <summary>
        /// Overrided ToString method.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            string s = string.Format("{0} {1} {2}", this.Id, this.Name, this.CountOfOrders);
            for (int i = 0; i < this.CountOfOrders; i++)
            {
                s += " ";
                s += this.OrderIdValues[i];
            }

            return s;
        }
    }
}
