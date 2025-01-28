using GroceryStoreAPI.Models;
using FluentValidation;

namespace GroceryStoreAPI.Validators
{
    public class OrderValidator:AbstractValidator<OrderModel>
    {
        public OrderValidator() 
        {
            RuleFor(o => o.OrderDate)
                .NotEmpty().WithMessage("Order Date is required!!!");

            RuleFor(o => o.CustomerID).
                NotEmpty().WithMessage("Customer ID is required!!!");

            RuleFor(o => o.Discount)
                .NotEmpty().WithMessage("Discount is Riquired");

            RuleFor(o => o.TotalAmount)
                .NotEmpty().WithMessage("Total Amount is required!!!")
                .NotEqual(0).WithMessage("Total amount can not be 0");

            RuleFor(o => o.ShippingAddress)
                .NotEmpty().WithMessage("Shipping Address is required!!!");

            RuleFor(o => o.PaymentMode)
                .NotEmpty().WithMessage("Payment mode is required!!!");
        }
    }
}
