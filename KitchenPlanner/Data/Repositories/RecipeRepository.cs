using KitchenPlanner.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KitchenPlanner.Data.Repositories;

/// <inheritdoc />
public class RecipeRepository : IGenericRepository<RecipeModel>
{
    private readonly IMongoCollection<RecipeModel> _collection;

    public RecipeRepository(IMongoCollection<RecipeModel> collection)
    {
        _collection = collection;
    }

    /// <inheritdoc />
    public IQueryable<RecipeModel> Get()
    {
        return _collection.AsQueryable();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RecipeModel>> FindByName(string name)
    {
        var filter = Builders<RecipeModel>
            .Filter.Regex(x => x.Name, new BsonRegularExpression(name));
        return await _collection.Find(filter).ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddAsync(RecipeModel entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    /// <inheritdoc />
    public async Task<RecipeModel> UpdateAsync(string id, RecipeModel entity)
    {
        var update = Builders<RecipeModel>.Update
            .Set(x=>x.Description, entity.Description)
            .Set(x=>x.CookingTime, entity.CookingTime)
            .Set(x=>x.PictureId, entity.PictureId)
            .Set(x=>x.Ingredients, entity.Ingredients)
            .Set(x=>x.Category, entity.Category);
        await _collection.UpdateOneAsync(x => x.Id == id, update);
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(id);
    }
}