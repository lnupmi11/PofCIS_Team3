namespace WpfApp.Utils
{
    using System.Collections.Generic;
    using WpfApp.Models;

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
                new TaxiDriver(1, "Petro", 2,  "5, 6"),
                new TaxiDriver(2, "Vasyl", 2,  "1, 7")
            };

            //using (TaxiAppDbContext db = new TaxiAppDbContext())
            //{
            //    foreach (var it in init_drivers)
            //    {
            //        db.TaxiDrivers.Add(it);
            //    }
            //    db.SaveChanges();
            //}
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
                new Order(6, 65, "1hr 2min", "Puzata khata", "+23424", "not assigned"),
                new Order(1, 80, "12hr 32min", "Vokzal", "+12341", "not assigned"),
                new Order(5, 160, "15hr 46min", "Bondarenko", "+512341", "not assigned"),
                new Order(7, 120, "3hr 54min", "Ashan", "+9234", "not assigned"),
                new Order(2, 120, "17hr 00min", "Metro", "+83234", "not assigned"),
                new Order(3, 120, "14hr 23min", "politek 20", "+52342", "not assigned"),
                new Order(6, 65, "1hr 2min", "Yahoo", "+23424", "not assigned"),
                new Order(1, 80, "12hr 32min", "DataArt", "+12341", "not assigned"),
                new Order(5, 160, "15hr 46min", "Google", "+512341", "not assigned"),
                new Order(7, 120, "3hr 54min", "Datacenter", "+9234", "not assigned"),
                new Order(2, 120, "17hr 00min", "Center", "+83234", "not assigned"),
                new Order(3, 120, "14hr 23min", "Codejig", "+52342", "not assigned"),
                new Order(6, 65, "1hr 2min", "Vokzal", "+23424", "not assigned"),
                new Order(1, 80, "12hr 32min", "Hurtozhutok", "+12341", "not assigned"),
                new Order(5, 160, "15hr 46min", "Mukachevo", "+512341", "not assigned"),
                new Order(7, 120, "3hr 54min", "Airport", "+9234", "not assigned"),
                new Order(2, 120, "17hr 00min", "Vynnyky", "+83234", "not assigned"),
                new Order(3, 120, "14hr 23min", "Heroyiv UPA 72", "+52342", "not assigned"),
            };
            //using (TaxiAppDbContext db = new TaxiAppDbContext())
            //{
            //    foreach (var it in init_orders)
            //    {
            //        db.Orders.Add(it);
            //    }
            //    db.SaveChanges();
            //}

            ordersArr.AddRange(init_orders);
        }
    }
}
