using KitchenPlanner.Data.Context;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;

namespace KitchenPlanner.Data;

public static class DataInjection
{
    public static void AddData(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped(sp => sp.GetRequiredService<DataContext>().GetCollection<IngredientModel>());
        services.AddScoped(sp => sp.GetRequiredService<DataContext>().GetCollection<RecipeModel>());
        
        services.AddScoped<IGenericRepository<IngredientModel>, IngredientRepository>();
        services.AddScoped<IGenericRepository<RecipeModel>, RecipeRepository>();

        services.AddSingleton<DataContext>();
        services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.DefaultSection));
    }
}