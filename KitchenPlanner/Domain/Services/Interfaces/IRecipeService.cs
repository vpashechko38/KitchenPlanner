using KitchenPlanner.Api.Dtos;

namespace KitchenPlanner.Domain.Services.Interfaces;

public interface IRecipeService
{
    IEnumerable<RecipeDto> Get();
    Task<RecipeDto> GetAsync(string id);
    
    /// <summary>
    /// Получение рецептов по категории
    /// </summary>
    IEnumerable<RecipeDto> Get(int category);
    Task AddAsync(RecipeDto recipeDto);
    Task UpdateAsync(string id, RecipeDto recipeDto);
    Task DeleteAsync(string id);
    Task DeleteIngredientAsync(string id, string ingredientId);
    Task AddIngredientAsync(string recipeId, string ingredientId);
}