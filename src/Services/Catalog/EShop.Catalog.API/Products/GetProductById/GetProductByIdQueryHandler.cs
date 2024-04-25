using EShop.Catalog.API.Exceptions;
using EShop.Catalog.API.Products.GetProductById.Records;

namespace EShop.Catalog.API.Products.GetProductById;

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdHandler.Handle called with {@query}", query);

        Product? product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        return product is not null ? new GetProductByIdResult(product)
            : throw new ProductNotFoundException(query.Id);
    }
}
