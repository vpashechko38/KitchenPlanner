using KitchenPlanner.Data.Context;
using KitchenPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenPlanner.Data.Repositories;

/// <inheritdoc />
public class RecipeRepository : IGenericRepository<RecipeModel>
{
    private readonly DataContext _context;
    public RecipeRepository(DataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public IQueryable<RecipeModel> Get()
    {
        return _context.Recipes.AsQueryable();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RecipeModel>> FindByName(string name)
    {
        // var filter = Builders<RecipeModel>
        //     .Filter.Regex(x => x.Name, new BsonRegularExpression(name));
        // return await _collection.Find(filter).ToListAsync();
        return new RecipeModel[0];
    }

    /// <inheritdoc />
    public async Task AddAsync(RecipeModel entity)
    {
        await _context.AddAsync(entity);
    }

    /// <inheritdoc />
    public async Task<RecipeModel> UpdateAsync(Guid id, RecipeModel entity)
    {
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(x=>x.Id == id);
        _context.Recipes.Remove(recipe);
    }
}