using EShop.Catalog.API.Products.GetProductById;
using EShop.Catalog.API.Products.UpdateProduct.Records;

namespace EShop.Catalog.API.Products.UpdateProduct;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            UpdateProductCommand command = request.Adapt<UpdateProductCommand>();

            UpdateProductResult result = await sender.Send(command);

            UpdateProductResponse response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        }).WithName("Update Product")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
