using Carter;
using Mapster;
using MediatR;
using Product.API.Models;

namespace Product.API.Products.GetProductByCategory;

public record GetProductsByCategoryResponse(List<ProductDto> Products);
public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("/api/products/category/{category}", async (string category, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetProductsByCategoryQuery(category);
            var result = await sender.Send(query, cancellationToken);
            var response = result.Adapt<GetProductsByCategoryResponse>();
            return Results.Ok(response);
        }).WithDescription("Get Products By Category")
        .WithName("GetProductsByCategory")
        .WithTags("Products")
        .Produces<GetProductsByCategoryResponse>()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
