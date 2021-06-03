using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(p => p.EmailAddress)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.TotalPrice)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
