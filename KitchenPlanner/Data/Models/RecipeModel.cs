using KitchenPlanner.Domain.Enums;

namespace KitchenPlanner.Data.Models;

/// <summary>
/// Рецепт
/// </summary>
public class RecipeModel : BaseModel
{
    public string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Идентификатор картинки
    /// </summary>
    public string PictureId { get; set; }
    
    /// <summary>
    /// Категория
    /// </summary>
    public Category Category { get; set; }
    
    /// <summary>
    /// Время приготовления
    /// </summary>
    public string CookingTime { get; set; }

    public virtual List<IngredientModel> Ingredients { get; set; }
}