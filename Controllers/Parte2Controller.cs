using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces.Services;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
    public class Parte2Controller : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public Parte2Controller(IProductService productService, ICustomerService customerService)
        {
            _productService = productService;
            _customerService = customerService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts(int page)
        {
            try
            {
                var products = await _productService.ListProducts(page);
                return Ok(products);
            }
            catch (Exception ex)
            {
                /// Poderia ser tratado com um retorno padrão com uma mensagem amigavel ao usuario e mostrar o erro no log
                return BadRequest($"Erro inesperado: {ex.Message}");
            }
            
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers(int page)
        {
            var customers = await _customerService.ListCustomers(page);
            return Ok(customers);
        }
    }
}
