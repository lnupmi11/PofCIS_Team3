using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.BL
{

    /// <summary>
    /// Business logic class.
    /// </summary>
    class BL
    {

        public List<TaxiDriver> TaxiDrivers { get; set; }
        public List<Order> Orders { get; set; }

        /// <summary>
		/// Method to find driver by Id.
		/// </summary>
		/// <param name="id">Driver's id.</param>
		/// <returns>Taxi driver type.</returns>
        public TaxiDriver FindTaxiDriverById(int id)
        {
            TaxiDriver rez = new TaxiDriver();
            foreach (TaxiDriver td in TaxiDrivers)
            {
                if (td.Id == id)
                {
                    rez = td;
                    break;
                }
            }
            return rez;
        }

        /// <summary>
		/// Method to find driver by Name.
		/// </summary>
		/// <param name="name">Driver's name.</param>
		/// <returns>Taxi driver type.</returns>
        public TaxiDriver FindTaxiDriverByName(string name)
        {
            TaxiDriver rez = new TaxiDriver();
            foreach (TaxiDriver td in TaxiDrivers)
            {
                if (td.Name == name)
                {
                    rez = td;
                    break;
                }
            }
            return rez;
        }

        /// <summary>
		/// Method to find order by Id.
		/// </summary>
		/// <param name="id">Order's Id.</param>
		/// <returns>Order type.</returns>
        public Order FindOrderById(int id)
        {
            Order rez = new Order();
            foreach (Order o in Orders)
            {
                if (o.Id == id)
                {
                    rez = o;
                    break;
                }
            }
            return rez;
        }

        /// <summary>
        /// Method to find orders by status.
        /// </summary>
        /// <param name="status">Order's status.</param>
        /// <returns>List of orders which has a specified status.</returns>
        public List<Order> FindOrderByStatus(string status)
        {
            List<Order> rez = new List<Order>();
            foreach (Order o in Orders)
            {
                if (o.Status == status)
                {
                    rez.Add(o);
                    break;
                }
            }
            return rez;
        }
    }
}
