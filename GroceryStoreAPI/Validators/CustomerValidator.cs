using GroceryStoreAPI.Models;
using FluentValidation;

namespace GroceryStoreAPI.Validators
{
    public class CustomerValidator:AbstractValidator<CustomerModel>
    {
        public CustomerValidator() 
        {
            RuleFor(c=>c.CustomerName)
                .NotEmpty().WithMessage("Customer Name is required!!!")
                .MaximumLength(50).WithMessage("User Name should contain 50 Character");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required!!!")
                .EmailAddress();

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required!!!").
                MaximumLength(10).WithMessage("Password should contain 10 character");

            RuleFor(c => c.City)
               .NotEmpty().WithMessage("City is required!!!").
               MaximumLength(40).WithMessage("City should contain 40 character");

            RuleFor(c => c.MobileNo)
               .NotEmpty().WithMessage("Mobile no. is required!!!").
               MaximumLength(10).WithMessage("Mobile should contain 10 digits");

            RuleFor(c => c.PinCode)
               .NotEmpty().WithMessage("Pincode is required!!!").
               MaximumLength(7).WithMessage("Pincode should contain 7 character");

            RuleFor(c => c.Address)
               .NotEmpty().WithMessage("Address is required!!!");

            RuleFor(c => c.IsActive)
               .NotEmpty().WithMessage("Is Active Field is required!!!");  
        }
    }
}
