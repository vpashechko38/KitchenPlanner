namespace KitchenPlanner.Data.Configuration;

public static class ModelConfiguration
{
    public static void Register()
    {
        IngredientModelConfiguration.Add();
        RecipeConfiguration.Add();
    }
}