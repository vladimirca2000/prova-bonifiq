using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;

namespace ProvaPub.Common;

public static class ConfigeServiceDataBase
{
    public static void ConfigDataBase(IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<TestDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
