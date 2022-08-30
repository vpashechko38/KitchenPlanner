using System.Net;
using KitchenPlanner.Api.Dtos;
using KitchenPlanner.Domain.Services;
using KitchenPlanner.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KitchenPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
    private readonly IIngredientService _ingredientService;

    public IngredientController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    /// <summary>
    /// Получение всех ингридиентов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IQueryable<IngredientDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult GetAsync()
    {
        var result = _ingredientService.Get();
        return Ok(result);
    }

    /// <summary>
    /// Поиск рецептов по части названия
    /// </summary>
    [HttpGet("find/{name}")]
    public async Task<IActionResult> FindByName([FromRoute]string name)
    {
        var result = await _ingredientService.FindByName(name);
        return Ok(result);
    }

    /// <summary>
    /// Получение ингредиента по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IngredientDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAsync([FromRoute]Guid id)
    {
        var result = await _ingredientService.GetAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Добавление нового ингредиента
    /// </summary>
    /// <remarks>
    /// Example:<br/> 
    /// <p/> <code>{<br/>
    /// <p/> "name": "Огурец",<br/>
    /// <p/> "description": "Зеленый длинный"<br/>
    /// <p/> }</code>
    /// </remarks>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddAsync([FromBody]IngredientDto ingredientDto)
    {
        await _ingredientService.AddAsync(ingredientDto);
        return Ok();
    }

    /// <summary>
    /// Обновить существующий ингредиент
    /// </summary>
    /// <remarks>
    /// Example:<br/> 
    /// <p/> <code>{<br/>
    /// <p/> "name": "Огурец",<br/>
    /// <p/> "description": "Зеленый длинный"<br/>
    /// <p/> }</code>
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]IngredientDto ingredientDto)
    {
        await _ingredientService.UpdateAsync(id, ingredientDto);
        return Ok();
    }

    /// <summary>
    /// Удалить существующий ингредиент
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
    {
        await _ingredientService.DeleteAsync(id);
        return Ok();
    }
}