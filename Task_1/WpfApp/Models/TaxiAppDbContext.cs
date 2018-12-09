namespace WpfApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TaxiAppDbContext : DbContext
    {
        public TaxiAppDbContext()
            : base("TaxiApp")
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<TaxiDriver> TaxiDrivers { get; set; }
    }
}
