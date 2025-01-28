using GroceryStoreAPI.Models;
using FluentValidation;

namespace GroceryStoreAPI.Validators
{
    public class BillValidator:AbstractValidator<BillModel>
    {
        public BillValidator() 
        {
            RuleFor(b => b.BillNumber).
                NotEmpty().WithMessage("Bill Number is required!!!").
                MaximumLength(10).WithMessage("Bill Number contain 10 character");

            RuleFor(b => b.OrderID).
                NotEmpty().WithMessage("Order ID is required!!!");

            RuleFor(b => b.CustomerID).
                NotEmpty().WithMessage("Customer ID is required!!!");

            RuleFor(b => b.TotalAmount).
                NotEmpty().WithMessage("Total Amount is required!!!")
                .NotEqual(0).WithMessage("Total Amount can not be 0!!!");

            RuleFor(b => b.NetAmount).
                NotEmpty().WithMessage("Net Amount is required!!!")
                .NotEqual(0).WithMessage("Net amount can not be 0!!!");

            RuleFor(b => b.Discount).
                NotEmpty().WithMessage("Discount is required!!!");
        }
    }
}
