namespace KitchenPlanner.Data.Models;

/// <summary>
/// Ингредиент
/// </summary>
public class IngredientModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
}