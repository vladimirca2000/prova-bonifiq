using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.Data;
using System.Data;
using System.Linq;

namespace ProvaPub.Services;

public class RandomService : IRandomService
{
    private readonly IRandomRepository _randomRepository;        

    public RandomService(IRandomRepository randomRepository)
    {
        _randomRepository = randomRepository;
    }

    public async Task<int> GetRandom()
    {
        /// Poderia ser variavel de ambiente ou appsettings 
        var numeroMaximoItens = 100;
        List<int> listaCompleta = Enumerable.Range(1, numeroMaximoItens).ToList();

        var listaSorteada = await _randomRepository.GetRandom();
        if (listaSorteada.Count() >= numeroMaximoItens)
            return -1;

        List<int> numerosFaltantes = listaCompleta.Except(listaSorteada).ToList();

        int seed = Guid.NewGuid().GetHashCode();
        var random = new Random(seed);
        var number = numerosFaltantes[random.Next(numerosFaltantes.Count)];

        var randomNumber = new RandomNumber()
        {
            Number = number
        };
        await _randomRepository.InsertAsync(randomNumber);
        return number;
    }
}
