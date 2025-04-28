namespace FinanceControl.Application.Features.FixedIncomes.Commands
{
    using FluentValidation;

    public class AddFixedIncomeValidator : AbstractValidator<AddFixedIncomeCommand>
    {
        public AddFixedIncomeValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(150).WithMessage("A descrição deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("A data de início é obrigatória.");
        }
    }
}
