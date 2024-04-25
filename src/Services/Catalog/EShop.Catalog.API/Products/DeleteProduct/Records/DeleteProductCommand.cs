namespace EShop.Catalog.API.Products.DeleteProduct.Records;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
