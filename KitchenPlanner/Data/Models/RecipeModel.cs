using KitchenPlanner.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KitchenPlanner.Data.Models;

/// <summary>
/// Рецепт
/// </summary>
public class RecipeModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string Id { get; set; }

    public string Name { get; set; }

    public List<IngredientModel> Ingredients { get; set; }
    
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
}