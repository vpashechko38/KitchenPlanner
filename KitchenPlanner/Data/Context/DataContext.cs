using KitchenPlanner.Data.Configuration;
using KitchenPlanner.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KitchenPlanner.Data.Context;

public class DataContext
{
    private readonly Dictionary<string, string> _collectionNames = new Dictionary<string, string>
    {
        { typeof(IngredientModel).FullName, "ingredients" },
        { typeof(RecipeModel).FullName, "recipes" }
    };

    private readonly IMongoDatabase _database;

    public DataContext(IOptions<MongoOptions> options)
    {
        ModelConfiguration.Register();
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        _database = mongoClient.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>()
    {
        var typeName = typeof(T).FullName;
        var collectionName = _collectionNames.FirstOrDefault(x => x.Key == typeName).Value;
        if (collectionName == null)
        {
            throw new NotSupportedException($"Тип {typeName} не поддерживается.");
        }

        return _database.GetCollection<T>(collectionName);
    }
}