using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces.Services;
using ProvaPub.Services;

namespace ProvaPub.Controllers;

/// <summary>
/// Ao rodar o código abaixo o serviço deveria sempre retornar um número diferente, mas ele fica retornando sempre o mesmo número.
/// 1 - Faça as alterações para que o retorno seja sempre diferente
/// /// 2 - Tome cuidado 
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class Parte1Controller : ControllerBase
{
    private readonly IRandomService _randomService;

    public Parte1Controller(IRandomService randomService)
    {
        _randomService = randomService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await _randomService.GetRandom();
            if (result == -1)
                return BadRequest("Não é possível sortear mais números");
            return Ok(result);
        }
        catch (Exception ex)
        {
            /// Poderia ser tratado com um retorno padrão com uma mensagem amigavel ao usuario e mostrar o erro no log
            return BadRequest($"Erro inesperado: {ex.Message}");
        }
    }
}
