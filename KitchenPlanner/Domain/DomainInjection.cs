using KitchenPlanner.Domain.Services;
using KitchenPlanner.Domain.Services.Interfaces;
using KitchenPlanner.Domain.Validations;

namespace KitchenPlanner.Domain;

public static class DomainInjection
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IIngredientService, IngredientService>();
        services.AddScoped<IRecipeService, RecipeService>();

        services.AddScoped<IRecipeValidator, RecipeValidator>();
    }
}