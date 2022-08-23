namespace KitchenPlanner.Data.Context;

public class MongoOptions
{
    public static string DefaultSection = "MongoDb";
    
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }
}