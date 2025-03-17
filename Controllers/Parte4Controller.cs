using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;
using ProvaPub.Interfaces.Services;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
  /// <summary>
    /// O Código abaixo faz uma chmada para a regra de negócio que valida se um consumidor pode fazer uma compra.
    /// Crie o teste unitário para esse Service. Se necessário, faça as alterações no código para que seja possível realizar os testes.
    /// Tente criar a maior cobertura possível nos testes.
    /// 
    /// Utilize o framework de testes que desejar. 
    /// Crie o teste na pasta "Tests" da solution
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte4Controller : ControllerBase
    {
        private readonly ICustomerService _custumerService;

        public Parte4Controller(ICustomerService custumerService)
        {
            _custumerService = custumerService;
        }

        [HttpGet("CanPurchase")]
        public async Task<IActionResult> CanPurchase(int customerId, decimal purchaseValue)
        {
            try
            {
                var canPurchase = await _custumerService.CanPurchase(customerId, purchaseValue);
                return Ok(canPurchase);

            }
            catch (Exception ex)
            {
                /// Poderia ser tratado com um retorno padrão com uma mensagem amigavel ao usuario e mostrar o erro no log
                return BadRequest($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
