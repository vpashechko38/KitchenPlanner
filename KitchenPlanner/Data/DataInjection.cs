using KitchenPlanner.Data.Context;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KitchenPlanner.Data;

public static class DataInjection
{
    public static void AddData(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("identity")));
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("default")));
    }
}