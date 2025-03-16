using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;
using ProvaPub.Interfaces.Services;
using System.Globalization;
using System.Threading.Tasks;

namespace ProvaPub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly IOrderService _orderService;

        public Parte3Controller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("orders")]
        public async Task<IActionResult> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            try
            {
                var order = await _orderService.PayOrder(paymentMethod, paymentValue, customerId);

                var orderResponse = new
                {
                    order.Id,
                    order.Value,
                    order.CustomerId,
                    OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo")).ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("pt-BR"))
                };

                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                /// Poderia ser tratado com um retorno padrão com uma mensagem amigavel ao usuario e mostrar o erro no log
                return BadRequest($"Erro inesperado: {ex.Message}");
            }

        }
    }
}
