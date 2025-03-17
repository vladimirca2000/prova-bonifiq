using Microsoft.Identity.Client;
using ProvaPub.Interfaces;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Models;
using ProvaPub.Repository.Data;
using System.Security;

namespace ProvaPub.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderService _ordemService;
    /// <summary>
    /// Quantidade de itens por página poderia ser passada por parametro ou ser variavel de ambiente ou appsettings
    /// </summary>
    private readonly int pageSize = 10;

    public CustomerService(ICustomerRepository customerRepository, IOrderService ordemService)
    {
        _customerRepository = customerRepository;
        _ordemService = ordemService;
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


    public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
    {
        var datePurchase = DateTime.Now;

        //estas duas linhas deve ser melhoradas e testaveis pelo xunit
        if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));
        if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

        //Business Rule: A customer can purchases only during business hours and working days
        HourPurchase(datePurchase);

        //Business Rule: The Customer must exist to make a purchase
        CustumerExist(customerId);


        //Business Rule: Checks if the customer meets the business rules
        var canPurchaseResult = _ordemService.CanPurchase(customerId, purchaseValue).Result;

        return true;
    }


    //Os dois metodos abaixo poderiam ser movidos para uma classe de regras de negocio e resolvido com notify
    //També melhorado os testes, mas não houve tempo.

    public void HourPurchase(DateTime hourPurchase)
    {
        if (hourPurchase.Hour < 8 || hourPurchase.Hour > 18 || hourPurchase.DayOfWeek == DayOfWeek.Saturday || hourPurchase.DayOfWeek == DayOfWeek.Sunday)
            throw new Exception("Fora do horário de funcionamento");
    }

    public void CustumerExist(int customerId)
    {
        if (!_customerRepository.ExistAsync(customerId).Result) 
            throw new Exception("O cliente não existe");
    }
}
