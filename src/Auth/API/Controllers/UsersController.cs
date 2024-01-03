using System.Security.Claims;
using Auth.Application.Exceptions;
using Auth.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.DTO.Users;
using Shared.Resources;
using Role = Shared.DTO.Role;

namespace Auth.API.Controllers;

[ApiController]
[Route("users/")]
[Authorize(Roles = Roles.Admin)]
public class UsersController : Controller
{
    private readonly IUserService service;


    public UsersController(IUserService service)
    {
        this.service = service;

    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ViewUser>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ViewUser), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IEnumerable<ViewUser>> GetUsers(string? name = null, string? email = null)
    {
        return await service.Get(name, email);
    }

    [HttpGet("{id:string}")]
    [ProducesResponseType(typeof(ViewUser), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ViewUser>> Get(string id)
    {
        try
        {
            return Ok(await service.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id:string/block}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> BlockUser(string id)
    {
        try
        {
            await service.LockUser(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id:string/unblock}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UnblockUser(string id)
    {
        try
        {
            await service.UnlockUser(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:string}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await service.Delete(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id:string}/roles/{role:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddRole(string id, Role role)
    {
        try
        {
            await service.AddRole(id, role);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (IncorrectParametersException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id:string}/roles/{role:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRole(string id, Role role)
    {
        try
        {
            await service.RemoveRole(id, role);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (IncorrectParametersException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:string}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserRoles(string id)
    {
        try
        {
            return Ok(await service.GetUserRoles(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
