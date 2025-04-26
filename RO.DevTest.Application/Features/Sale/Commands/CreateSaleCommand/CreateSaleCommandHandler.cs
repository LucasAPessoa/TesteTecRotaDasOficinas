
using FluentValidation;
using FluentValidation.Results;
using MediatR;

using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;
using RO.DevTest.Domain.Entities;


public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IIdentityAbstractor _identityAbstractor;

    public CreateSaleCommandHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IIdentityAbstractor identityAbstractor)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _identityAbstractor= identityAbstractor;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        CreateSaleCommandValidator _validator = new();
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }

        var user = await _identityAbstractor.FindUserByIdAsync(request.User)
                   ?? throw new ArgumentException($"Usuário com ID {request.User} não encontrado.");

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            User = user,
            SaleItems = new List<SaleItem>(),
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow,
        };

        foreach (var itemDto in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId)
                ?? throw new ArgumentException($"Produto com ID {itemDto.ProductId} não encontrado.");

            var saleItem = new SaleItem
            {
                Id = Guid.NewGuid(),
                Product = product,
                Quantity = itemDto.Quantity,
                Price = product.Price,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            sale.SaleItems.Add(saleItem);
        }

        await _saleRepository.CreateAsync(sale);

        return new CreateSaleResult(sale);
    }
}