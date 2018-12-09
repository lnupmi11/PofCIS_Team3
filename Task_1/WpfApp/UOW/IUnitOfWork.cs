namespace WpfApp.UOW
{
    using WpfApp.Models;
    using WpfApp.Repository;

    public interface IUnitOfWork
    {
        GenericRepository<TaxiDriver> Drivers { get; }

        GenericRepository<Order> Orders { get; }
    }
}
