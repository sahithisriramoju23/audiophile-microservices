using Carter;
using Mapster;
using MediatR;
using Product.API.Models;

namespace Product.API.Products.GetProductById;

public record GetProductByIdResponse(ProductDto Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/api/products/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var productQuery = new GetProductByIdQuery(id);
            var result = await sender.Send(productQuery,cancellationToken);
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
        .WithDescription("Get Product By Id")
        .Produces<GetProductByIdResponse>()
        .Produces(StatusCodes.Status404NotFound)
        .WithName("GetProductById")
        .WithTags("Products");
    }
}
