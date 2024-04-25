using EShop.Catalog.API.Products.CreateProduct.Records;

namespace EShop.Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Category)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => x.ImageFile)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Image File is required");

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}
