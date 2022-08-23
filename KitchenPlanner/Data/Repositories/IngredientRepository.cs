using KitchenPlanner.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KitchenPlanner.Data.Repositories;

/// <inheritdoc />
public class IngredientRepository : IGenericRepository<IngredientModel>
{
    private readonly IMongoCollection<IngredientModel> _collection;

    public IngredientRepository(IMongoCollection<IngredientModel> collection)
    {
        _collection = collection;
    }

    public async Task<IEnumerable<IngredientModel>> FindByName(string name)
    {
        var filter = Builders<IngredientModel>
            .Filter.Regex(x => x.Name, new BsonRegularExpression(name));
        return await _collection.Find(filter).ToListAsync();
    }

    /// <inheritdoc />
    public IQueryable<IngredientModel> Get()
    {
        return _collection.AsQueryable();
    }

    /// <inheritdoc />
    public async Task AddAsync(IngredientModel entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    /// <inheritdoc />
    public async Task<IngredientModel> UpdateAsync(string id, IngredientModel entity)
    {
        var update = Builders<IngredientModel>.Update
            .Set(x => x.Name, entity.Name)
            .Set(x => x.Description, entity.Description);

        await _collection.UpdateOneAsync(id, update);
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(id);
    }
}