using ProvaPub.Models;

namespace ProvaPub.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> ListCustomers(int page);

        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
