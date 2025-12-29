using Cart.API.Models;
using Carter;
using Mapster;
using MediatR;

namespace Cart.API.Cart.StoreCart;

public record StoreCartRequest(ShoppingCart Cart);
public record StoreCartResponse(string UserName);
public class StoreCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cart", async (StoreCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreCartCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<StoreCartResponse>();
            return Results.Created($"/Cart/{response.UserName}", response);
        }).WithName("Store Cart")
         .Produces<StoreCartResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Store Cart")
         .WithDescription("Store Cart");
    }
}
