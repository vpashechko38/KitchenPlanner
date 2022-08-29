using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KitchenPlanner.Api.Controllers;

[Authorize(Roles = "admin")]
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost("Set")]
    public async Task<IActionResult> SetRoles(string userId, string roleId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest();
        }

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return BadRequest();
        }

        var result = await _userManager.AddToRoleAsync(user, role.Name);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest();
    }
    
    [HttpPost("Unset")]
    public async Task<IActionResult> UnsetRoles(string userId, string roleId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest();
        }

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return BadRequest();
        }

        var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromQuery]string name)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole(name));
        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete(string name)
    {
        var role = await _roleManager.FindByNameAsync(name);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
            return Ok();
        }

        return BadRequest();
    }
}