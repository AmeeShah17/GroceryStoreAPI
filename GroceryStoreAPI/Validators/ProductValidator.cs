using FluentValidation;
using GroceryStoreAPI.Models;


namespace GroceryStoreAPI.Validators
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator() 
        {
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Product Name is Required!!!")
                .MaximumLength(50).WithMessage("Product name should contain 50 character");

            RuleFor(p => p.ProductCode).
                NotEmpty().WithMessage("Product Code is Required!!!")
                .MaximumLength(10).WithMessage("Product Code should contain 50 character");

            RuleFor(p => p.ProductPrice)
                .NotEmpty().WithMessage("Product Price is Required!!!")
                .GreaterThan(0).WithMessage("Price should be rater than zero!!");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is Required!!!");

            
        }
    }
}
