using EShop.Catalog.API.Exceptions;

namespace EShop.Catalog.API.Products.UpdateProduct;

#region Command, Result
public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);
#endregion

#region Valdiation
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

        RuleFor(x => x.Category)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => x.ImageFile)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Image File is required");

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}
#endregion

#region Handler
public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@command}", command);

        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken) 
            ?? throw new ProductNotFoundException(command.Id);
        
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update<Product>(product);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
#endregion