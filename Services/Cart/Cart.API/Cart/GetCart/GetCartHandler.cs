using Cart.API.Models;
using BuidlingBlocks.CQRS;
using Cart.API.Data;

namespace Cart.API.Cart.GetCart;

public record GetCartQuery(string Username) : IQuery<GetCartResult>;
public record GetCartResult(ShoppingCart Cart);
public class GetCartHandler(ICartRepository _cartRepository) : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCart(query.Username, cancellationToken);
        return new GetCartResult(cart);
    }
}
