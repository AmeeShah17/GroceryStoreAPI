using GroceryStoreAPI.Models;
using FluentValidation;
namespace GroceryStoreAPI.Validators
{
    public class SubCategoryValidator: AbstractValidator<SubCategoryModel>
    {
        public SubCategoryValidator() 
        {
            RuleFor(s => s.SubCategoryName)
                .NotEmpty().WithMessage("Sub Category name is required!!!")
                .MaximumLength(50).WithMessage("Lenght should be of 50 character");

            RuleFor(s => s.CategoryID)
                .NotEmpty().WithMessage("Category ID is required!!!");
        }
    }
}
