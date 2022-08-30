using KitchenPlanner.Domain.Enums;

namespace KitchenPlanner.Api.Dtos.Recipe;

/// <summary>
/// Рецепт
/// </summary>
public class CreateRecipeDto
{
    public string Name { get; set; }

    /// <summary>
    /// Ингридиенты
    /// </summary>
    public List<Guid> Ingredients { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Идентификатор картинки
    /// </summary>
    public Guid PictureId { get; set; }
    
    /// <summary>
    /// Категория
    /// </summary>
    public Category Category { get; set; }
    
    /// <summary>
    /// Время приготовления
    /// </summary>
    public string CookingTime { get; set; }
}