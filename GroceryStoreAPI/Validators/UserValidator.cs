using GroceryStoreAPI.Models;
using FluentValidation;

namespace GroceryStoreAPI.Validators
{
    public class UserValidator:AbstractValidator<UserModel>
    {
        public UserValidator()  
        {
            RuleFor(u=>u.UserName)
                .NotEmpty().WithMessage("User Name is required!!!")
                .MaximumLength(50).WithMessage("User Name should contain 50 Character");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required!!!")
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required!!!");

           
        }
    }
}
