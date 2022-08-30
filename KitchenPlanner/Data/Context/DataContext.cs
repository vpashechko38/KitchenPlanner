using KitchenPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenPlanner.Data.Context;

public class DataContext : DbContext
{
    public DbSet<IngredientModel> Ingredients { get; set; }

    public DbSet<RecipeModel> Recipes { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeModel>()
            .HasMany(x => x.Ingredients)
            .WithMany(x => x.Recipes)
            .UsingEntity(x => x.ToTable("IngredientsRecipes"));
    }
}