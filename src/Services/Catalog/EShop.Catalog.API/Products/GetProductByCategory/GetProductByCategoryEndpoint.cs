using EShop.Catalog.API.Products.GetProductByCategory.Records;

namespace EShop.Catalog.API.Products.GetProductByCategory;

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            GetProductByCategoryQuery query = new(category);

            GetProductByCategoryResult result = await sender.Send(query);

            GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        }).WithName("Get Product By Category")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status302Found)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
