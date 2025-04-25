using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Products.Commands.CreateProduct;
using RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Products.Commands.UpdateProduct;
using RO.DevTest.Application.Features.Products.Commands.UpdateProductCommand;
using RO.DevTest.Application.Features.Products.Queries.GetAllProductsQuery;
using RO.DevTest.Application.Features.Products.Queries.GetProductById;
using RO.DevTest.Application.Features.Products.Queries.GetProductByIdQuery;




namespace RO.DevTest.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [OpenApiTags("Products")]
    public class ProductsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(GetAllProductsResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts([FromQuery] GetAllProductsQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductByIdResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(response);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var response = await _mediator.Send(request);
            return Created(HttpContext.Request.GetDisplayUrl(), response);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UpdateProductResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateProductResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand request)
        {
            request.Id = id;
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DeleteProductResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteProductResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(response);
        }
    }
}
