using KitchenPlanner.Domain.Enums;

namespace KitchenPlanner.Api.Dtos;

/// <summary>
/// Рецепт
/// </summary>
public class RecipeDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string? Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// Ингридиенты
    /// </summary>
    public List<IngredientDto> Ingredients { get; set; }
    
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