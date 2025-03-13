using Microsoft.AspNetCore.Mvc;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Ao rodar o código abaixo o serviço deveria sempre retornar um número diferente, mas ele fica retornando sempre o mesmo número.
    /// 1 - Faça as alterações para que o retorno seja sempre diferente
    /// 2 - Tome cuidado 
    /// ---------
    /// 1 - Feito a injeção de dependência do serviço RandomService
    /// 2 - Alterado o método Index que retorna um número aleatório
    /// 3 - Adicionado um try catch para tratar exceções
    /// 4 - Poderia ter criado um objeto de retorno padrão para todas as requisições, onde teria os dados e uma mensagem de erro perdonalizada quando houvesse
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte1Controller : ControllerBase
    {
        private readonly RandomService _randomService;

        public Parte1Controller(RandomService randomService)
        {
            _randomService = randomService;
        }

        [HttpGet]
        public async Task<int> Index()
        {
            try
            {
                var result = await _randomService.GetRandom();
                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
