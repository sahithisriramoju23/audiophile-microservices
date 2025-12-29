using Carter;
using Mapster;
using MediatR;

namespace Cart.API.Cart.DeleteCart;

public record DeleteCartResponse(bool IsSuccess);
public class DeleteCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/cart/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCartCommand(userName));
            var response = result.Adapt<DeleteCartResponse>();
            return Results.Ok(response);
        }).WithName("Delete Cart")
           .Produces<DeleteCartResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Delete Cart")
           .WithDescription("Delete Cart");
    }
}

