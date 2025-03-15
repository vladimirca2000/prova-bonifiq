namespace ProvaPub.Interfaces.Payment;

public interface IPaymentStrategy
{
    Task<bool> ProcessPayment(decimal amount, int customerId);
}
