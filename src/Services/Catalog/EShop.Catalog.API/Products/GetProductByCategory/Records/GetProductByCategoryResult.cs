namespace EShop.Catalog.API.Products.GetProductByCategory.Records;

public record GetProductByCategoryResult(IReadOnlyCollection<Product> Products);