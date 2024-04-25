using EShop.Catalog.API.Exceptions;
using EShop.Catalog.API.Products.UpdateProduct.Records;

namespace EShop.Catalog.API.Products.UpdateProduct;

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