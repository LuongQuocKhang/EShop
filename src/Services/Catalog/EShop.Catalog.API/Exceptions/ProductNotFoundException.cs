using BuildingBlocks.Exceptions;

namespace EShop.Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {
    }
}
