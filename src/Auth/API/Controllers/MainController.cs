using System.Security.Claims;
using Auth.Application.Exceptions;
using Auth.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.DTO.DTO.Users;
using IAuthorizationService = Auth.Application.Interfaces.Base.IAuthorizationService;

namespace Auth.API.Controllers;

[ApiController]
[Route("/")]
public class MainController : Controller
{
    private readonly IAuthorizationService authService;
    private readonly IUserService userService;

    public MainController(IAuthorizationService authService, IUserService userService)
    {
        this.authService = authService;
        this.userService = userService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AuthDetails>> Login(string userName, string password)
    {
        try
        {
            return await authService.Authorization(userName, password);
        }
        catch (IncorrectParametersException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (AccessDenidedException)
        {
            return Forbid();
        }
    }

    [HttpGet("/profile")]
    [ProducesResponseType(typeof(ViewUser), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ViewUser>> GetUsers()
    {
        string? userName = HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userName))
            return Unauthorized();
        return Ok(await userService.GetByName(userName));
    }
}
