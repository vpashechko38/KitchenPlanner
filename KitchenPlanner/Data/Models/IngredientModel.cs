namespace KitchenPlanner.Data.Models;

/// <summary>
/// Ингредиент
/// </summary>
public class IngredientModel : BaseModel
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
}