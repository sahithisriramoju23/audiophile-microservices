using BuidlingBlocks.CQRS;
using Cart.API.Data;
using Cart.API.Models;
using FluentValidation;

namespace Cart.API.Cart.StoreCart;

public record StoreCartCommand(ShoppingCart Cart) : ICommand<StoreCartResult>;
public record StoreCartResult(string UserName);
public class StoreCartCommandValidator : AbstractValidator<StoreCartCommand>
{
    public StoreCartCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Shopping Cart cannot be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
    }
}
internal class StoreCartCommandHandler
    (ICartRepository repository)
    : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
    {
        //Call Grpc get discount service to apply discount
       // await DeductDiscount(command.Cart, cancellationToken);
        //Store Cart in database (use marten upsert - if exist update or create)
        var cart = await repository.StoreCart(command.Cart);

        return new StoreCartResult(command.Cart.UserName);
    }
    /*private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var getDiscountRequest = new GetDiscountRequest { ProductName = item.ProductName };
            var coupon = await discountProto.GetDiscountAsync(getDiscountRequest, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }*/
}
