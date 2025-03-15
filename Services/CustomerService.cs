using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Models;
using ProvaPub.Repository.Data;

namespace ProvaPub.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    /// <summary>
    /// Quantidade de itens por página poderia ser passada por parametro ou ser variavel de ambiente ou appsettings
    /// </summary>
    private readonly int pageSize = 10;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> ListCustomers(int page)
    {
        if (_customerRepository.HasNext(page, pageSize).Result == false)
            throw new Exception("Não há mais produtos disponíveis para exibir");

        var customers = await _customerRepository.ListCustumeres(page, pageSize);
        return customers ?? Enumerable.Empty<Customer>();
    }

    public async Task<IEnumerable<Customer>> GetAllCustumers()
    {
        var customers = await _customerRepository.SelectAllAsync();
        return customers;
    }

    public int CountProduct()
    {
        var customers = GetAllCustumers().Result;
        return customers.Count();
    }







    //TestDbContext _ctx;

    //public CustomerService(TestDbContext ctx)
    //{
    //    _ctx = ctx;
    //}

    //public CustomerList ListCustomers(int page)
    //{
    //    return new CustomerList() { HasNext = false, TotalCount = 10, Customers = _ctx.Customers.ToList() };
    //}

    //public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
    //{
    //    if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

    //    if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

    //    //Business Rule: Non registered Customers cannot purchase
    //    var customer = await _ctx.Customers.FindAsync(customerId);
    //    if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

    //    //Business Rule: A customer can purchase only a single time per month
    //    var baseDate = DateTime.UtcNow.AddMonths(-1);
    //    var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
    //    if (ordersInThisMonth > 0)
    //        return false;

    //    //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
    //    var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
    //    if (haveBoughtBefore == 0 && purchaseValue > 100)
    //        return false;

    //    //Business Rule: A customer can purchases only during business hours and working days
    //    if (DateTime.UtcNow.Hour < 8 || DateTime.UtcNow.Hour > 18 || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
    //        return false;


    //    return true;
    //}

}
