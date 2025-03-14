﻿using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<IEnumerable<Customer>> ListCustomers(int page)
        {
            return _customerRepository.SelectAllAsync();
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
}
