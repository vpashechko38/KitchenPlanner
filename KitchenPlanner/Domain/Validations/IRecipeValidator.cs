using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Api.Dtos.Recipe;

namespace KitchenPlanner.Domain.Validations;

public interface IRecipeValidator
{
    bool ValidateAdd(RecipeDto recipeDto);
}