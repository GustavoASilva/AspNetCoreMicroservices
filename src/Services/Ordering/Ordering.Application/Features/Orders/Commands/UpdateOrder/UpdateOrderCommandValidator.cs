using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
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
