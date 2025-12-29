using BuidlingBlocks.CQRS;
using FluentValidation;
using Product.API.Models;

namespace Product.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(List<ProductDto> Products);

public class GetProductsByCategoryQueryValidator : AbstractValidator<GetProductsByCategoryQuery>
{
    public GetProductsByCategoryQueryValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(20).WithMessage("Category must not exceed 20 characters.");
    }
}
