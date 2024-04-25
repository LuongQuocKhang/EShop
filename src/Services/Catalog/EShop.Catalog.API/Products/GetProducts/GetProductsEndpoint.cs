using EShop.Catalog.API.Common;
using EShop.Catalog.API.Products.GetProducts.Records;

namespace EShop.Catalog.API.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (ISender sender, 
            int pageIndex = 0, 
            int pageSize = 10, 
            SortOrder SortOrder = SortOrder.ASC) =>
            {
                GetProductsResult result = await sender.Send(new GetProductsQuery()
                {
                    SortOrder = SortOrder,
                    pageSize = pageSize,
                    pageIndex = pageIndex
                });

                GetProductsResponse response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("Get Products")
            .Produces<GetProductsResponse>(StatusCodes.Status302Found)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}
