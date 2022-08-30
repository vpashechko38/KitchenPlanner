using KitchenPlanner.Api.Dtos;

namespace KitchenPlanner.Domain.Services.Interfaces;

/// <summary>
/// Интерфейс для работы с ингридиентами
/// </summary>
public interface IIngredientService
{
    /// <summary>
    /// Получение ингредиентов
    /// </summary>
    IQueryable<IngredientDto> Get();
    
    /// <summary>
    /// Поиск ингридиента по названию или части названия
    /// </summary>
    Task<IEnumerable<IngredientDto>> FindByName(string name);
    
    /// <summary>
    /// Получение ингридиента по идентификатора
    /// </summary>
    Task<IngredientDto> GetAsync(Guid id);
    
    /// <summary>
    /// Создание ингридиента
    /// </summary>
    Task AddAsync(IngredientDto ingredientDto);
    
    /// <summary>
    /// Обновление ингридиента
    /// </summary>
    Task UpdateAsync(Guid id, IngredientDto ingredientDto);
    
    /// <summary>
    /// Удаление ингридиента
    /// </summary>
    Task DeleteAsync(Guid id);
}