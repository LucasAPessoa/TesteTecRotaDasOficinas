using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;
using RO.DevTest.Application.Features.Sale.Queries.GetAllSalesQuery;
using RO.DevTest.Application.Features.Sale.Queries.GetSaleAnalysisQuery;
using RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;
using RO.DevTest.Application.Features.Sales.Queries.GetSaleById;


namespace RO.DevTest.WebApi.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request)
        {
            var result = await _mediator.Send(request);
            return Created(HttpContext.Request.GetDisplayUrl(), result);
        }

        [HttpPatch("{saleId}")]
        [ProducesResponseType(typeof(UpdateSaleResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateSaleResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSale([FromRoute] Guid saleId, [FromBody] UpdateSaleCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllSalesResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAllSalesResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSales([FromQuery] GetAllSalesQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{saleId}")]
        [ProducesResponseType(typeof(GetSaleByIdResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetSaleByIdResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSaleById([FromRoute] Guid saleId)
        {
            var result = await _mediator.Send(new GetSaleByIdQuery(saleId));
            return Ok(result);
        }

        [HttpDelete("{saleId}")]
        [ProducesResponseType(typeof(DeleteSaleResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteSaleResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid saleId)
        {
            var result = await _mediator.Send(new DeleteSaleCommand(saleId));
            return Ok(result);
        }

        [HttpGet("analysis")]
        [ProducesResponseType(typeof(GetSalesAnalysisResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetSalesAnalysisResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyzeSales([FromQuery] GetSalesAnalysisQuery request)
        {
    

            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}