using KitchenPlanner.Api.Dtos;

namespace KitchenPlanner.Domain.Validations;

public interface IRecipeValidator
{
    bool ValidateAdd(RecipeDto recipeDto);
}