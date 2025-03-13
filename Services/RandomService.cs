using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.Data;
using System.Data;
using System.Linq;

namespace ProvaPub.Services
{
    public class RandomService : IRandomService
    {
        private readonly IRandomRepository _randomRepository;        

        public RandomService(IRandomRepository randomRepository)
        {
            _randomRepository = randomRepository;
        }

        public async Task<int> GetRandom()
        {
            int seed = Guid.NewGuid().GetHashCode();
            List<int> listaCompleta = Enumerable.Range(0, 101).ToList();


            var listaSorteada = await _randomRepository.GetRandom();
            if (listaSorteada.Count() == 100)
                throw new Exception("Não é possível sortear mais números");

            var QuantidadeNumerosSorteados = listaSorteada.Count();
            var number = new Random(seed).Next(100 - QuantidadeNumerosSorteados);

            List<int> numerosFaltantes = listaCompleta.Except(listaSorteada).ToList();

            if (!numerosFaltantes.Contains(number))
                throw new Exception("Número já sorteado");
            
            var randomNumber = new RandomNumber()
            {
                Number = number
            };
            await _randomRepository.InsertAsync(randomNumber);
            return number;
        }

    }
}
