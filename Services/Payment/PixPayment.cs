using ProvaPub.Interfaces;
using ProvaPub.Interfaces.Payment;

namespace ProvaPub.Services.Payments
{
    public class PixPayment : IPaymentStrategy
    {
        public async Task<bool> ProcessPayment(decimal amount, int customerId)
        {
            // Simulação de processamento via Pix
            await Task.Delay(500);
            return true;
        }
    }
}

