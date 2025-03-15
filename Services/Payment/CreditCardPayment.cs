using ProvaPub.Interfaces;
using ProvaPub.Interfaces.Payment;

namespace ProvaPub.Services.Payments
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public async Task<bool> ProcessPayment(decimal amount, int customerId)
        {
            // Simulação de processamento via Cartão de Crédito
            await Task.Delay(500);
            return true;
        }
    }
}


