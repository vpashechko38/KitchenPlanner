using KitchenPlanner.Api.Dtos;

namespace KitchenPlanner.Domain.Services.Interfaces;

public interface IIngredientService
{
    IQueryable<IngredientDto> Get();
    Task<IEnumerable<IngredientDto>> FindByName(string name);
    Task<IngredientDto> GetAsync(string id);
    Task AddAsync(IngredientDto ingredientDto);
    Task UpdateAsync(string id, IngredientDto ingredientDto);
    Task DeleteAsync(string id);
}