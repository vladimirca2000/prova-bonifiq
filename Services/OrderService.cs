using ProvaPub.Interfaces.Payment;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Services;

public class OrderService : IOrderService
{
    private readonly IOrdemRepository _ordemRepository;

    public OrderService(IOrdemRepository ordemRepository)
    {
        _ordemRepository = ordemRepository;
    }

    public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
    {
        IPaymentStrategy paymentStrategy = PaymentFactory.GetPaymentStrategy(paymentMethod);

        bool paymentSuccessful = await paymentStrategy.ProcessPayment(paymentValue, customerId);
        if (!paymentSuccessful)
            throw new Exception("Erro ao processar pagamento");

        var order = new Order
        {
            Value = paymentValue,
            CustomerId = customerId
        };
        order.SetOrderDate(DateTime.UtcNow); // Salvar sempre em UTC

        return await _ordemRepository.InsertAsync(order);
    }

    public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
    {
        var baseDate = DateTime.UtcNow.AddMonths(-1);

        //checks if the customer has already purchased
        var hasBoughtBefore = await _ordemRepository.Purchase(customerId);
        if (!hasBoughtBefore && purchaseValue > 100)
            throw new Exception("O cliente não comprou antes e o valor da compra é maior que 100");

        //Business Rule: A customer can purchase only a single time per month
        var ordersInThisMonth = await _ordemRepository.CountAsync(customerId, baseDate);
        if (ordersInThisMonth > 0)
            throw new Exception("O cliente já comprou este mês");

        return true;
    }

}

