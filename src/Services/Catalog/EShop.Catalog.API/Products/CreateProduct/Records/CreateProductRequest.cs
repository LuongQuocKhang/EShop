namespace EShop.Catalog.API.Products.CreateProduct.Records; 
public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);