using AutoMapper;
using AutoMapper.QueryableExtensions;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;
using KitchenPlanner.Domain.Services.Interfaces;

namespace KitchenPlanner.Domain.Services;

/// <inheritdoc />
public class IngredientService : IIngredientService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public IngredientService(IMapper mapper, IUnitOfWork uow)
    {
        _mapper = mapper;
        _uow = uow;
    }

    /// <inheritdoc />
    public IQueryable<IngredientDto> Get()
    {
        return _uow.Ingredients.Get()
            .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<IngredientDto>> FindByName(string name)
    {
        var ingredients = await _uow.Ingredients.FindByName(name);
        return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
    }

    /// <inheritdoc />
    public async Task<IngredientDto> GetAsync(Guid id)
    {
        var ingredient = _uow.Ingredients.Get()
            .SingleOrDefault(x => x.Id == id);
        return _mapper.Map<IngredientDto>(ingredient);
    }

    /// <inheritdoc />
    public async Task AddAsync(IngredientDto ingredientDto)
    {
        var ingredient = _mapper.Map<IngredientModel>(ingredientDto);
        await _uow.Ingredients.AddAsync(ingredient);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, IngredientDto ingredientDto)
    {
        var ingredient = _mapper.Map<IngredientModel>(ingredientDto);
        await _uow.Ingredients.UpdateAsync(id, ingredient);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var ingredient = await GetAsync(id);
        if (ingredient is null)
        {
            throw new ArgumentException("Указанный идентификатор не найден");
        }

        await _uow.Ingredients.DeleteAsync(id);
    }
}