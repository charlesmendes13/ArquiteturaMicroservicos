using FluentValidation;
using Payment.Application.ViewModels;

namespace Payment.Application.Validators
{
    public class CreatePaymentViewModelValidator : AbstractValidator<CreatePaymentViewModel>
    {
        public CreatePaymentViewModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().NotNull()
               .WithMessage("O UserId não pode ser nulo");
        }
    }
}
