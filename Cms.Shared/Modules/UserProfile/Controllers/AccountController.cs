using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cms.Shared.Modules.UserProfile.Models;
using Cms.Shared.Modules.UserProfile.Services;
using Cms.Shared.Shared;
using Cms.Shared.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Shared.Modules.UserProfile.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AccountController : ControllerBase
{
    private readonly UserService _userService;
    private readonly GetProfileService _getProfileService;
    public AccountController(UserService userService, GetProfileService getProfileService)
    {
        _userService = userService;
        _getProfileService = getProfileService;
    }

    [HttpGet("getProfile")]
    public  IActionResult GetUser()
    {
       return Ok(_getProfileService.GetProfile());
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
    {
        try
        {
            var result = await _userService.RegisterUserAsync(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e));
        }
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] LoginModel model)
    {
        try
        {
            var result = await _userService.LoginUserAsync(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e));
        }
    }
    
    
}