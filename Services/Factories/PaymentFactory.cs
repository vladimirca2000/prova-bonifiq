using ProvaPub.Interfaces.Payment;
using ProvaPub.Services.Payments;

namespace ProvaPub.Services
{
    public class PaymentFactory
    {
        public static IPaymentStrategy GetPaymentStrategy(string paymentMethod)
        {
            return paymentMethod.ToLower() switch
            {
                "pix" => new PixPayment(),
                "creditcard" => new CreditCardPayment(),
                "paypal" => new PayPalPayment(),
                _ => throw new ArgumentException("Método de pagamento inválido")
            };
        }
    }
}
