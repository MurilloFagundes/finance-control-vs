namespace FinanceControl.Application.Features.FixedIncomes.Commands
{
    public record AddFixedIncomeCommand(string Description, decimal Amount, DateOnly StartDate);
}
