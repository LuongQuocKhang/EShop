
using EShop.Catalog.API.Exceptions;

namespace EShop.Catalog.API.Products.DeleteProduct;

#region Command, Result
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);
#endregion

#region Validator
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}
#endregion

#region Handler
internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommandHandler.Handle called with {@query}", command);

        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken) 
            ?? throw new ProductNotFoundException(command.Id);

        session.Delete<Product>(product);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
#endregion