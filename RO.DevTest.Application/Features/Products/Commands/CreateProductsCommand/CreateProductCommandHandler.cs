using FluentValidation.Results;
using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _repository = repository;

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        CreateProductCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult);
        }

        var existingProduct = await _repository.GetByNameAsync(request.Name);
        if (existingProduct is not null)
        {
            throw new ArgumentException("Já existe um produto com esse nome.");
        }


        var product = request.AssignTo();

        var newProduct = await _repository.CreateAsync(product);

        return new CreateProductResult(newProduct);
    }
}
