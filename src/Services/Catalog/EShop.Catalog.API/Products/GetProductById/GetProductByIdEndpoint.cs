using EShop.Catalog.API.Products.GetProductById.Records;

namespace EShop.Catalog.API.Products.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            GetProductByIdQuery query = new(id);

            GetProductByIdResult result = await sender.Send(query);

            GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        }).WithName("Get Product By Id")
        .Produces<GetProductByIdResponse>(StatusCodes.Status302Found)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
