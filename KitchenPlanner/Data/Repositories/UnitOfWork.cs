using KitchenPlanner.Data.Context;
using KitchenPlanner.Data.Models;

namespace KitchenPlanner.Data.Repositories;

/// <summary>
/// Единица работы
/// </summary>
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IGenericRepository<IngredientModel> _ingredientRepository;

    /// <inheritdoc />
    public IGenericRepository<IngredientModel> Ingredients
    {
        get { return _ingredientRepository ??= new IngredientRepository(_context); }
    }

    private IGenericRepository<RecipeModel> _recipeRepository;

    /// <inheritdoc />
    public IGenericRepository<RecipeModel> Recipes
    {
        get { return _recipeRepository ??= new RecipeRepository(_context); }
    }

    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _context.Dispose();
    }
}