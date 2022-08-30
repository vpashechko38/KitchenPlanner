using System.Net;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Api.Dtos.Recipe;
using KitchenPlanner.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KitchenPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    
    /// <summary>
    /// Получение всех рецептов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IQueryable<RecipeDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult GetAsync()
    {
        var result = _recipeService.Get().ToList();
        return Ok(result);
    }

    /// <summary>
    /// Получение рецепта по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IngredientDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAsync([FromRoute]Guid id)
    {
        var result = await _recipeService.GetAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Получение рецептов по категории
    /// </summary>
    /// <remarks>
    /// Список возможных категорий:<br/>
    /// <p/>Первое - 1,<br/>
    /// <p/>Второе - 2,<br/>
    /// <p/>Выпечка - 3,<br/>
    /// <p/>Закуска - 4,<br/>
    /// <p/>Салат - 5,<br/>
    /// <p/>Гарнир - 6,<br/>
    /// <p/>Завтрак - 7
    /// </remarks>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<IngredientDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult Get([FromRoute]int category)
    {
        var result = _recipeService.Get(category);
        return Ok(result);
    }

    /// <summary>
    /// Добавление нового рецепта
    /// </summary>
    /// <remarks>
    /// Example:<br/> 
    /// <p/> <code>{<br/>
    /// <p/> "pictureId": "Пока опустим, загрузка фото будет позже",<br/>
    /// <p/> "description": "Зеленый длинный"<br/>
    /// <p/> "category": 1<br/>
    /// <p/> "cookingTime": "ОДИН ЕБУЧИЙ ЧАС"<br/>
    /// <p/> }</code>
    /// </remarks>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddAsync(CreateRecipeDto recipeDto)
    {
        await _recipeService.AddAsync(recipeDto);
        return Ok();
    }

    /// <summary>
    /// Обновить существующий рецепт
    /// </summary>
    /// <remarks>
    /// Example:<br/> 
    /// <p/> <code>{<br/>
    /// <p/> "pictureId": "Пока опустим, загрузка фото будет позже",<br/>
    /// <p/> "description": "Зеленый длинный"<br/>
    /// <p/> "category": 1<br/>
    /// <p/> "cookingTime": "ОДИН ЕБУЧИЙ ЧАС"<br/>
    /// <p/> }</code>
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]CreateRecipeDto recipeDto)
    {
        await _recipeService.UpdateAsync(id, recipeDto);
        return Ok();
    }

    /// <summary>
    /// Удалить существующий рецепт
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
    {
        await _recipeService.DeleteAsync(id);
        return Ok();
    }

    [HttpDelete("{recipeId}/ingredient/{ingredientId}")]
    public async Task<IActionResult> RemoveIngredient([FromRoute]Guid recipeId, [FromRoute]Guid ingredientId)
    {
        await _recipeService.DeleteIngredientAsync(recipeId, ingredientId);
        return Ok();
    }

    [HttpPost("{recipeId}/ingredient/{ingredientId}")]
    public async Task<IActionResult> AddIngredient([FromRoute]Guid recipeId, [FromRoute]Guid ingredientId)
    {
        await _recipeService.AddIngredientAsync(recipeId, ingredientId);
        return Ok();
    }
}