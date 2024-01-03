using Auth.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.DTO.Users;
using Shared.Resources;

namespace Auth.API.Controllers;

[ApiController]
[Route("users/")]
public class UsersController : Controller
{
    private readonly IUserService service;


    public UsersController(IUserService service)
    {
        this.service = service;

    }
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(IEnumerable<ViewUser>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<ViewUser>> GetUsers(string? name = null, string? email = null)
    {
        return await service.Get(name, email);
    }

    [HttpGet("profile")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<ViewUser>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ViewUser>>> GetUsers(string? name = null, string? email = null)
    {
        HttpContext.User.Claims
        return Ok(await service.Get(name, email));
    }
}
