using GroceryStoreAPI.Models;
using FluentValidation;

namespace GroceryStoreAPI.Validators
{
    public class OrderDetailValidator:AbstractValidator<OrderDetailModel>
    {
        public OrderDetailValidator() 
        {
            RuleFor(o=>o.OrderID).
                 NotEmpty().WithMessage("Order ID is required!!!");
            
            RuleFor(o=>o.CustomerID)
                .NotEmpty().WithMessage("Customer ID is required!!!");

            RuleFor(o => o.Quantity)
                .NotEmpty().WithMessage("Quantity is required!!!")
                .NotEqual(0).WithMessage("Quantity can not be 0!!!");

            RuleFor(o => o.Amount)
                .NotEmpty().WithMessage("Amount is required!!!")
                .NotEqual(0).WithMessage("Amount can not be 0!!!");

            RuleFor(o => o.TotalAmount)
                .NotEmpty().WithMessage("Total Amount is required!!!")
                .NotEqual(0).WithMessage("Total Amount can not be 0!!!");
        }
    }
}
