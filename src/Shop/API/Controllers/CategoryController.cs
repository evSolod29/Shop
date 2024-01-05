using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.DTO.Categories;
using Shared.Resources;
using Shop.Application.Exceptions;
using Shop.Application.Interfaces;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("/categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService service;

        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ViewCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.SuperUser + "," + Roles.User)]
        public async Task<ActionResult<ViewCategory>> Get(long id)
        {
            try
            {
                return Ok(await service.GetCategory(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewCategory>), StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.SuperUser + "," + Roles.User)]
        public async Task<ActionResult<IEnumerable<ViewCategory>>> Get(string? name)
        {
            return Ok(await service.GetCategories(name));
        }

        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Roles.SuperUser)]
        public async Task<ActionResult<long>> Post(CreateCategory category)
        {
            try
            {
                return Ok(await service.Create(category));
            }
            catch (IncorrectParametersException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.SuperUser)]
        public async Task<ActionResult<int>> Put(long id, CreateCategory category)
        {
            try
            {
                return Ok(await service.Update(id, category));
            }
            catch (IncorrectParametersException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.SuperUser)]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await service.Delete(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
