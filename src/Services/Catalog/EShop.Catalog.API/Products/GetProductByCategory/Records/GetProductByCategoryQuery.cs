namespace EShop.Catalog.API.Products.GetProductByCategory.Records;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
