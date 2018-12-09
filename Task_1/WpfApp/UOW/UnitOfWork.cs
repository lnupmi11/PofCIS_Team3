namespace WpfApp.UOW
{
    using System;
    using WpfApp.Models;
    using WpfApp.Repository;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TaxiAppDbContext context = new TaxiAppDbContext();

        GenericRepository<TaxiDriver> drivers;

        GenericRepository<Order> orders;

        public GenericRepository<TaxiDriver> Drivers
        {
            get
            {
                if (this.drivers == null)
                {
                    this.drivers = new GenericRepository<TaxiDriver>(this.context);
                }

                return this.drivers;
            }
        }

        public GenericRepository<Order> Orders
        {
            get
            {
                if (this.orders == null)
                {
                    this.orders = new GenericRepository<Order>(this.context);
                }

                return this.orders;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
