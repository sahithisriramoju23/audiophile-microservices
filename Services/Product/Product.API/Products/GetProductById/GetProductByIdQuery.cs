using BuidlingBlocks.CQRS;
using FluentValidation;
using Product.API.Models;

namespace Product.API.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(ProductDto Product);

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}
