using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Utils
{

    /// <summary>
    /// Seeds class.
    /// </summary>
    public class Seeds
    {
        /// <summary>
        /// Method to check the correctness of the execution.
        /// </summary>
        /// <param name="driversArr">List of taxi drivers.</param>
        public static void Execute(ref List<TaxiDriver> driversArr)
        {
            List<TaxiDriver> init_drivers = new List<TaxiDriver>()
            {
                new TaxiDriver(1, "Petro", 2,  new List<int>() { 5, 6 } ),
                new TaxiDriver(2, "Vasyl", 2,  new List<int>() { 1, 7 } )
            };

            driversArr.AddRange(init_drivers);
        }

        /// <summary>
        /// Method to check the correctness of the execution.
        /// </summary>
        /// <param name="ordersArr">List of orders.</param>
        public static void Execute(ref List<Order> ordersArr)
        {
            List<Order> init_orders = new List<Order>()
            {
                new Order(6, 65, "1hr 2min", "shop", "+23424", "done"),
                new Order(1, 80, "12hr 32min", "university", "+12341", "already assigned"),
                new Order(5, 160, "15hr 46min", "stadium", "+512341", "already assigned"),
                new Order(7, 120, "3hr 54min", "center", "+9234", "already assigned"),
                new Order(2, 120, "17hr 00min", "Forum", "+83234", "not assigned"),
                new Order(3, 120, "14hr 23min", "Horodotska 20", "+52342", "not assigned"),
            };

            ordersArr.AddRange(init_orders);
        }
    }
}
