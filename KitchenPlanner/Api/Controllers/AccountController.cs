using KitchenPlanner.Api.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KitchenPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]AccountDto accountDto)
    {
        var user = new IdentityUser
        {
            Email = accountDto.Email,
            UserName = accountDto.Name
        };
        var result = await _userManager.CreateAsync(user, accountDto.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest("Ошибся кажется");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("Password/Change")]
    public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto passwordDto)
    {
        var user = await _userManager.FindByEmailAsync(passwordDto.Email);
        var result = await _userManager.ChangePasswordAsync(user, passwordDto.OldPassword, passwordDto.NewPassword);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest("чето не то");
    }
}