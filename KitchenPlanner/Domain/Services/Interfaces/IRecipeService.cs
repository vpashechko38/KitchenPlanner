using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Api.Dtos.Recipe;

namespace KitchenPlanner.Domain.Services.Interfaces;

public interface IRecipeService
{
    IEnumerable<RecipeDto> Get();
    Task<RecipeDto> GetAsync(Guid id);
    
    /// <summary>
    /// Получение рецептов по категории
    /// </summary>
    IEnumerable<RecipeDto> Get(int category);
    Task AddAsync(CreateRecipeDto recipeDto);
    Task UpdateAsync(Guid id, CreateRecipeDto recipeDto);
    Task DeleteAsync(Guid id);
    Task DeleteIngredientAsync(Guid id, Guid ingredientId);
    Task AddIngredientAsync(Guid recipeId, Guid ingredientId);
}