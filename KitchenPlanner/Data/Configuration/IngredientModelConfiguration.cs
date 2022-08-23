using KitchenPlanner.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace KitchenPlanner.Data.Configuration;

public static class IngredientModelConfiguration
{
    public static void Add()
    {
        BsonClassMap.RegisterClassMap<IngredientModel>(options =>
        {
            options.AutoMap();
            options.GetMemberMap(x => x.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);
        });
    }
}