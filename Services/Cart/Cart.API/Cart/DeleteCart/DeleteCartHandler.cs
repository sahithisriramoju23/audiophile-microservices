using BuidlingBlocks.CQRS;
using Cart.API.Data;
using FluentValidation;

namespace Cart.API.Cart.DeleteCart;

public record DeleteCartCommand(string UserName) : ICommand<DeleteCartResult>;
public record DeleteCartResult(bool IsSuccess);
public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
    }
}
internal class DeleteCartCommandHandler(ICartRepository repository) : ICommandHandler<DeleteCartCommand, DeleteCartResult>
{
    public async Task<DeleteCartResult> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        //TODO: Delete Cart from the database
        var result = await repository.DeleteCart(command.UserName);
        //TODO: Update cache
        return new DeleteCartResult(result);
    }
}
