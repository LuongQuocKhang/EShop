namespace EShop.Catalog.API.Products.GetProductById.Records;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
