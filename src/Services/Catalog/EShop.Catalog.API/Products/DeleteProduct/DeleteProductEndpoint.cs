using EShop.Catalog.API.Products.DeleteProduct.Records;

namespace EShop.Catalog.API.Products.DeleteProduct;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}",
            async (Guid id, ISender sender) =>
            {
                DeleteProductResult result = await sender.Send(new DeleteProductCommand(id));

                DeleteProductResponse response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithName("Delete Product")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}
