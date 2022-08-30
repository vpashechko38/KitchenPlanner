using KitchenPlanner.Data.Context;
using KitchenPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenPlanner.Data.Repositories;

/// <inheritdoc />
public class IngredientRepository : IGenericRepository<IngredientModel>
{
    private readonly DataContext _context;

    public IngredientRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IngredientModel>> FindByName(string name)
    {
        //todo add regexp to find by part name
        return new IngredientModel[0];
    }

    /// <inheritdoc />
    public IQueryable<IngredientModel> Get()
    {
        return _context.Ingredients.AsQueryable();
    }

    /// <inheritdoc />
    public async Task AddAsync(IngredientModel entity)
    {
        await _context.Ingredients.AddAsync(entity);
    }

    /// <inheritdoc />
    public async Task<IngredientModel> UpdateAsync(Guid id, IngredientModel entity)
    {
        // var update = Builders<IngredientModel>.Update
        //     .Set(x => x.Name, entity.Name)
        //     .Set(x => x.Description, entity.Description);

        //await _context.UpdateOneAsync(id, update);
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var ingredient = await Get().FirstOrDefaultAsync(x => x.Id == id);
        _context.Ingredients.Remove(ingredient);
    }
}