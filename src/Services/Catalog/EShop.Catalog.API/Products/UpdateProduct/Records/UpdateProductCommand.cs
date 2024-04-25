namespace EShop.Catalog.API.Products.UpdateProduct.Records;

public record UpdateProductCommand(
Guid Id,
string Name,
List<string> Category,
string Description,
string ImageFile,
decimal Price) : ICommand<UpdateProductResult>;
