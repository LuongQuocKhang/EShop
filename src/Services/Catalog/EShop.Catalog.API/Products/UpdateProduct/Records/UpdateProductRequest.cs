namespace EShop.Catalog.API.Products.UpdateProduct.Records;

public record UpdateProductRequest(
Guid Id,
string Name,
List<string> Category,
string Description,
string ImageFile,
decimal Price);
