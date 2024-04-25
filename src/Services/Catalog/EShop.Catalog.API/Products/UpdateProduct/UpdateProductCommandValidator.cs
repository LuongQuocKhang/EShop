using EShop.Catalog.API.Products.UpdateProduct.Records;

namespace EShop.Catalog.API.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

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
