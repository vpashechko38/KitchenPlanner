using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;

namespace KitchenPlanner.Domain.Validations;

public class RecipeValidator : IRecipeValidator
{
    private readonly IGenericRepository<RecipeModel> _recipeRepository;
    private readonly IGenericRepository<IngredientModel> _ingredientRepository;

    public RecipeValidator(IGenericRepository<RecipeModel> recipeRepository,
        IGenericRepository<IngredientModel> ingredientRepository)
    {
        _recipeRepository = recipeRepository;
        _ingredientRepository = ingredientRepository;
    }

    public bool ValidateAdd(RecipeDto recipeDto)
    {
        if (recipeDto.Ingredients == null || !recipeDto.Ingredients.Any())
        {
            return false;
        }
        
        var ingredientIds = recipeDto.Ingredients.Select(y => y.Id);
        var ingredients = _ingredientRepository.Get().Where(x => ingredientIds.Contains(x.Id));
        if (ingredients.Count() != recipeDto.Ingredients.Count)
        {
            //todo впихнуть тексты ошибок
            return false;
        }

        var recipeByName = _recipeRepository.Get().FirstOrDefault(x => x.Name == recipeDto.Name);
        if (recipeByName != null)
        {
            return false;
        }

        return true;
    }
}