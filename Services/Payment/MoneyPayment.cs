using ProvaPub.Interfaces.Payment;

namespace ProvaPub.Services.Payments
{
    public class MoneyPayment : IPaymentStrategy
    {
        public async Task<bool> ProcessPayment(decimal amount, int customerId)
        {
            // Simulação de processamento via Dinheiro
            await Task.Delay(500);
            return true;
        }
    }
}
