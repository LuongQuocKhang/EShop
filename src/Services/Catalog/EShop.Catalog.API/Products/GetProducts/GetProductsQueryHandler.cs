﻿using EShop.Catalog.API.Common;
using EShop.Catalog.API.Products.GetProducts.Records;
using Marten.Pagination;

namespace EShop.Catalog.API.Products.GetProducts;

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@query}", query);

        IPagedList<Product> products = await session
            .Query<Product>()
            .ToPagedListAsync(pageNumber: query.pageIndex, pageSize: query.pageSize, token: cancellationToken);

        //switch(query.SortOrder)
        //{
        //    case SortOrder.ASC:
        //        {
        //            products = products.OrderBy(x => x.Id);
        //            break;
        //        }
        //    case SortOrder.DESC:
        //        {
        //            products = products.OrderByDescending(x => x.Id);
        //            break;
        //        }
        //    default:
        //        {
        //            break;
        //        }
        //}

        //IReadOnlyCollection<Product> result = await products.ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}
