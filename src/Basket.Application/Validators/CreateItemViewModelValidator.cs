using Basket.Application.ViewModels;
using FluentValidation;

namespace Basket.Application.Validators
{
    public class CreateItemViewModelValidator : AbstractValidator<CreateItemViewModel>
    {
        public CreateItemViewModelValidator()
        {
            RuleFor(x => x.ProductId)                
                .GreaterThan(0)
                .WithMessage("O ProductId deve ser maior que 0");

            RuleFor(x => x.Quantity)               
               .GreaterThan(0)
               .WithMessage("A Quantity deve ser maior que 0");
        }
    }
}
