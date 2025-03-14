using Microsoft.Extensions.DependencyInjection;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Repository;
using ProvaPub.Repository.Data;
using ProvaPub.Services;

namespace ProvaPub.Common;

public static class ConfigureServiceDependencyInjection
{
    public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
    {
        // Adicione aqui os serviços 
        serviceCollection.AddTransient<IRandomService, RandomService>();


        // Adicione aqui os repositórios
        serviceCollection.AddTransient<IRandomRepository, RandomRepository>();


        serviceCollection.AddScoped<TestDbContext>();
    }
}
