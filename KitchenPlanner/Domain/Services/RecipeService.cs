using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KitchenPlanner.Api.Dtos.Recipe;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;
using KitchenPlanner.Domain.Enums;
using KitchenPlanner.Domain.Services.Interfaces;
using KitchenPlanner.Domain.Validations;
using Microsoft.EntityFrameworkCore;

namespace KitchenPlanner.Domain.Services;

public class RecipeService : IRecipeService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IRecipeValidator _recipeValidator;

    public RecipeService(
        IUnitOfWork uow,
        IMapper mapper,
        IRecipeValidator recipeValidator)
    {
        _uow = uow;
        _mapper = mapper;
        _recipeValidator = recipeValidator;
    }

    public IEnumerable<RecipeDto> Get()
    {
        var recipes = _uow.Recipes.Get().ToList();
        return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
    }

    public async Task<RecipeDto> GetAsync(Guid id)
    {
        var recipe = _uow.Recipes.Get()
            .SingleOrDefault(x => x.Id == id);

        return _mapper.Map<RecipeDto>(recipe);
    }

    /// <inheritdoc />
    public IEnumerable<RecipeDto> Get(int category)
    {
        return _uow.Recipes.Get()
            .Where(x=>x.Category == (Category)category)
            .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider);
    }

    public async Task AddAsync(CreateRecipeDto recipeDto)
    {
        // var validationResult = _recipeValidator.ValidateAdd(recipeDto);
        // if (!validationResult)
        // {
        //     throw new ValidationException("ПНХ");
        // }
        
        var recipe = _mapper.Map<RecipeModel>(recipeDto);

        var ingredients = _uow.Ingredients
            .Get()
            .Where(x => recipeDto.Ingredients.Contains(x.Id))
            .ToList();
        
        recipe.Ingredients = ingredients;

        await _uow.Recipes.AddAsync(recipe);
        await _uow.SaveAsync();
    }

    public async Task UpdateAsync(Guid id, CreateRecipeDto recipeDto)
    {
        var recipe = await _uow.Recipes
            .Get()
            .SingleOrDefaultAsync(x => x.Id == id);
        if (recipe is null)
        {
            throw new ArgumentNullException();
        }
        
        var ingredients = _uow.Ingredients
            .Get()
            .Where(x => recipeDto.Ingredients.Contains(x.Id))
            .ToList();

        recipe.Ingredients.Clear();
        recipe.Ingredients.AddRange(ingredients);

        recipe.Category = recipeDto.Category;
        recipe.Description = recipeDto.Description;
        recipe.Name = recipeDto.Name;
        recipe.CookingTime = recipeDto.CookingTime;

        await _uow.SaveAsync();
    }

    public async Task DeleteIngredientAsync(Guid id, Guid ingredientId)
    {
        var recipe = _uow.Recipes.Get().SingleOrDefault(x => x.Id == id);
        if (recipe == null)
        {
            throw new ValidationException("Все ты врешь, нет такого айди");
        }

        var ingredient = recipe.Ingredients.SingleOrDefault(x => x.Id == ingredientId);
        if (ingredient == null)
        {
            throw new ArgumentException("Ай ай, нет такого ингридиента в рецепте");
        }
        
        recipe.Ingredients.Remove(ingredient);
        await _uow.SaveAsync();
    }

    public async Task AddIngredientAsync(Guid recipeId, Guid ingredientId)
    {
        var ingredient = _uow.Ingredients.Get()
            .SingleOrDefault(x => x.Id == ingredientId);
        if (ingredient == null)
        {
            throw new ArgumentException("Ingredient not found");
        }

        var recipe = _uow.Recipes.Get()
            .SingleOrDefault(x => x.Id == recipeId);
        if (recipe == null)
        {
            throw new ArgumentException("recipe not found");
        }
        
        recipe.Ingredients.Add(ingredient);
        await _uow.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _uow.Recipes.DeleteAsync(id);
        await _uow.SaveAsync();
    }
}