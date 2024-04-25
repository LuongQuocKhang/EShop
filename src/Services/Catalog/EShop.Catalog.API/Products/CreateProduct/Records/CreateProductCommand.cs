namespace EShop.Catalog.API.Products.CreateProduct.Records;

public record CreateProductCommand(string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;