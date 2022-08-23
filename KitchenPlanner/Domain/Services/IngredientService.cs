using AutoMapper;
using AutoMapper.QueryableExtensions;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Data.Models;
using KitchenPlanner.Data.Repositories;
using KitchenPlanner.Domain.Services.Interfaces;

namespace KitchenPlanner.Domain.Services;

public class IngredientService : IIngredientService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<IngredientModel> _ingredientRepository;

    public IngredientService(IMapper mapper, IGenericRepository<IngredientModel> ingredientRepository)
    {
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
    }

    public IQueryable<IngredientDto> Get()
    {
        return _ingredientRepository.Get()
            .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider);
    }

    public async Task<IEnumerable<IngredientDto>> FindByName(string name)
    {
        var ingredients = await _ingredientRepository.FindByName(name);
        return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
    }

    public async Task<IngredientDto> GetAsync(string id)
    {
        var ingredient = _ingredientRepository.Get()
            .SingleOrDefault(x => x.Id == id);
        return _mapper.Map<IngredientDto>(ingredient);
    }

    public async Task AddAsync(IngredientDto ingredientDto)
    {
        var ingredient = _mapper.Map<IngredientModel>(ingredientDto);
        await _ingredientRepository.AddAsync(ingredient);
    }

    public async Task UpdateAsync(string id, IngredientDto ingredientDto)
    {
        var ingredient = _mapper.Map<IngredientModel>(ingredientDto);
        await _ingredientRepository.UpdateAsync(id, ingredient);
    }

    public async Task DeleteAsync(string id)
    {
        var ingredient = await GetAsync(id);
        if (ingredient is null)
        {
            throw new ArgumentException("Указанный идентификатор не найден");
        }

        await _ingredientRepository.DeleteAsync(id);
    }
}