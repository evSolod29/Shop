using Microsoft.AspNetCore.Mvc;
using Shared.DTO.DTO.Products;
using Shop.Application.Exceptions;
using Shop.Application.Interfaces;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductsController : Controller
    {
        private readonly IProductService service;


        public ProductsController(IProductService service)
        {
            this.service = service;

        }

        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> Post(CreateProduct product)
        {
            try
            {
                return Ok(await service.Create(product));
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
        public async Task<ActionResult<long>> Put(long id, CreateProduct product)
        {
            try
            {
                return Ok(await service.Update(id, product));
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ViewProduct), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ViewProduct>> Get(long id)
        {
            try
            {
                return Ok(await service.GetProduct(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewProductFull>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ViewProductFull>>> Get(string? name = null,
                                                                        string? description = null,
                                                                        decimal? priceFrom = null,
                                                                        decimal? priceTo = null,
                                                                        string? commonNote = null,
                                                                        long? categoryId = null)
        {
            return Ok(await service.GetProducts(name, description, priceFrom, priceTo, commonNote, categoryId));
        }

        [HttpGet("full/{id:int}")]
        [ProducesResponseType(typeof(ViewProductFull), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ViewProductFull>> GetFull(long id)
        {
            try
            {
                return Ok(await service.GetFullProduct(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("full")]
        [ProducesResponseType(typeof(IEnumerable<ViewProductFull>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ViewProductFull>>> GetFull(string? name = null,
                                                                        string? description = null,
                                                                        decimal? priceFrom = null,
                                                                        decimal? priceTo = null,
                                                                        string? commonNote = null,
                                                                        long? categoryId = null,
                                                                        string? additionalNote = null)
        {
            return Ok(await service.GetFullProducts(name, description, priceFrom, priceTo, commonNote, categoryId, additionalNote));
        }
    }
}
