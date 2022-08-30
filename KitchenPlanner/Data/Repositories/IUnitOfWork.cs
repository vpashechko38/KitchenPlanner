using KitchenPlanner.Data.Models;

namespace KitchenPlanner.Data.Repositories;

/// <summary>
/// Единица работы
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Интферфейс доступа к списку ингридиентов
    /// </summary>
    IGenericRepository<IngredientModel> Ingredients { get; }
    
    /// <summary>
    /// Интерфейс доступа к рецептам
    /// </summary>
    IGenericRepository<RecipeModel> Recipes { get; }
    
    /// <summary>
    /// Сохранение изменения
    /// </summary>
    Task SaveAsync();
}