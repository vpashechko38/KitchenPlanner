using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;
using KitchenPlanner.Domain.Enums;
using KitchenPlanner.Domain.Services.Interfaces;
using KitchenPlanner.Domain.Validations;
using Microsoft.EntityFrameworkCore;

namespace KitchenPlanner.Domain.Services;

public class RecipeService : IRecipeService
{
    private readonly IGenericRepository<RecipeModel> _recipeRepository;
    private readonly IGenericRepository<IngredientModel> _ingredientRepository;
    private readonly IMapper _mapper;
    private readonly IRecipeValidator _recipeValidator;

    public RecipeService(
        IGenericRepository<RecipeModel> recipeRepository,
        IMapper mapper,
        IGenericRepository<IngredientModel> ingredientRepository,
        IRecipeValidator recipeValidator)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
        _recipeValidator = recipeValidator;
    }

    public IEnumerable<RecipeDto> Get()
    {
        var recipes = _recipeRepository.Get().ToList();
        return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
    }

    public async Task<RecipeDto> GetAsync(string id)
    {
        var recipe = _recipeRepository.Get()
            .SingleOrDefault(x => x.Id == id);

        return _mapper.Map<RecipeDto>(recipe);
    }

    /// <inheritdoc />
    public IEnumerable<RecipeDto> Get(int category)
    {
        return _recipeRepository.Get()
            .Where(x=>x.Category == (Category)category)
            .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider);
    }

    public async Task AddAsync(RecipeDto recipeDto)
    {
        var validationResult = _recipeValidator.ValidateAdd(recipeDto);
        if (!validationResult)
        {
            throw new ValidationException("ПНХ");
        }

        var ingredientIds = recipeDto.Ingredients.Select(x => x.Id);
        var ingredients = _ingredientRepository.Get()
            .Where(x => ingredientIds.Contains(x.Id))
            .ToList();
        
        var recipe = _mapper.Map<RecipeModel>(recipeDto);
        recipe.Ingredients = ingredients;
        await _recipeRepository.AddAsync(recipe);
    }

    public async Task UpdateAsync(string id, RecipeDto recipeDto)
    {
        var recipe = _mapper.Map<RecipeModel>(recipeDto);
        await _recipeRepository.UpdateAsync(id, recipe);
    }

    public async Task DeleteIngredientAsync(string id, string ingredientId)
    {
        var recipe = _recipeRepository.Get().SingleOrDefault(x => x.Id == id);
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
        await _recipeRepository.UpdateAsync(id, recipe);
    }

    public async Task AddIngredientAsync(string recipeId, string ingredientId)
    {
        var ingredient = _ingredientRepository.Get()
            .SingleOrDefault(x => x.Id == ingredientId);
        if (ingredient == null)
        {
            throw new ArgumentException("Ingredient not found");
        }

        var recipe = _recipeRepository.Get()
            .SingleOrDefault(x => x.Id == recipeId);
        if (recipe == null)
        {
            throw new ArgumentException("recipe not found");
        }
        
        recipe.Ingredients.Add(ingredient);
        await _recipeRepository.UpdateAsync(recipeId, recipe);
    }

    public async Task DeleteAsync(string id)
    {
        await _recipeRepository.DeleteAsync(id);
    }
}