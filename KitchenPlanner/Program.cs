using System.Reflection;
using KitchenPlanner.Data;
using KitchenPlanner.Domain;
using KitchenPlanner.Domain.MapperProfiles;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddData(builder.Configuration);
builder.Services.AddDomain();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(IngredientProfile)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Помощник хозяйке на кухне(название в разработке)",
            Description =
                "Позволяет хранить список продуктов дома, проверять рецепты на возможность их приготовить и тд (в разработке)",
            Contact = new OpenApiContact
            {
                Name = "Vladislav Pashechko",
                Email = "vpashechko38@gmail.com"
            }
        });
                
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

var app = builder.Build();

app.MapSwagger();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();