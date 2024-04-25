using EShop.Catalog.API.Common;

namespace EShop.Catalog.API.Products.GetProducts.Records;

public record GetProductsRequest(int pageIndex = 0, int pageSize = 10, SortOrder SortOrder = SortOrder.ASC);
