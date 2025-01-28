using GroceryStoreAPI.Models;
using FluentValidation;

namespace GroceryStoreAPI.Validators
{
    public class CategoryValidator:AbstractValidator<CategoryModel>
    { 
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty().WithMessage("Category Name is required!!!")
                .MaximumLength(50).WithMessage("Lenght should be of 50 character");
        }
    }
}
