namespace KitchenPlanner.Data.Repositories;

/// <summary>
/// Интерфейс доступа к данным
/// </summary>
public interface IGenericRepository<T>
{
    /// <summary>
    /// Получить список сущностей
    /// </summary>
    IQueryable<T> Get();

    Task<IEnumerable<T>> FindByName(string name);
    
    /// <summary>
    /// Добавить сущность
    /// </summary>
    /// <param name="entity">Новая сущность</param>
    Task AddAsync(T entity);
    
    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="entity">Новые данные обновляемой сущности</param>
    Task<T> UpdateAsync(string id,T entity);
    
    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    Task DeleteAsync(string id);
}