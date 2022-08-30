using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Api.Dtos.Recipe;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;

namespace KitchenPlanner.Domain.Validations;

public class RecipeValidator : IRecipeValidator
{
    private readonly IUnitOfWork _uow;

    public RecipeValidator(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public bool ValidateAdd(RecipeDto recipeDto)
    {
        if (recipeDto.Ingredients == null || !recipeDto.Ingredients.Any())
        {
            return false;
        }
        
        // var ingredientIds = recipeDto.Ingredients.Select(y => y.Id);
        // var ingredients = _ingredientRepository.Get().Where(x => ingredientIds.Contains(x.Id));
        // if (ingredients.Count() != recipeDto.Ingredients.Count)
        // {
        //     //todo впихнуть тексты ошибок
        //     return false;
        // }

        var recipeByName = _uow.Recipes.Get().FirstOrDefault(x => x.Name == recipeDto.Name);
        if (recipeByName != null)
        {
            return false;
        }

        return true;
    }
}