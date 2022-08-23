using KitchenPlanner.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace KitchenPlanner.Data.Configuration;

public class RecipeConfiguration
{
    public static void Add()
    {
        BsonClassMap.RegisterClassMap<RecipeModel>(options =>
        {
            options.AutoMap();
            options.GetMemberMap(x => x.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);
            options.GetMemberMap(x => x.PictureId).SetSerializer(new StringSerializer(BsonType.ObjectId));
        });
    }
}