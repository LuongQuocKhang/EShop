using EShop.Catalog.API.Common;

namespace EShop.Catalog.API.Products.GetProducts.Records;

public record GetProductsQuery(int pageIndex = 0, int pageSize = 10, SortOrder SortOrder = SortOrder.ASC) : IQuery<GetProductsResult>;
