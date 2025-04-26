using MediatR;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sales.Common;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public string User { get; set; } = string.Empty;
    public List<SaleItemDto> Items { get; set; } = new();
}