using EShop.Catalog.API.Common;

namespace EShop.Catalog.API.Products.GetProducts;

public record GetProductsQuery(int pageIndex = 0, int pageSize = 10, SortOrder SortOrder = SortOrder.ASC) : IQuery<GetProductsResult>;

public record GetProductsResult(IReadOnlyCollection<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@query}", query);

        IQueryable<Product> products = session
            .Query<Product>()
            .Skip(query.pageIndex * query.pageSize)
            .Take(query.pageSize);

        switch(query.SortOrder)
        {
            case SortOrder.ASC:
                {
                    products = products.OrderBy(x => x.Id);
                    break;
                }
            case SortOrder.DESC:
                {
                    products = products.OrderByDescending(x => x.Id);
                    break;
                }
            default:
                {
                    break;
                }
        }

        IReadOnlyCollection<Product> result = await products.ToListAsync(cancellationToken);

        return new GetProductsResult(result);
    }
}
