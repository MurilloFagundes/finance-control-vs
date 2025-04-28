namespace FinanceControl.Domain.Events.FixedIncomes
{
    public record FixedIncomeAdded(
    Guid Id,
    string Description,
    decimal Amount,
    DateOnly StartDate);
}
