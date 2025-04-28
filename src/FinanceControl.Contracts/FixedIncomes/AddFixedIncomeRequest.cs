namespace FinanceControl.Contracts.FixedIncomes
{
    public record AddFixedIncomeRequest(
    string Description,
    decimal Amount,
    DateOnly StartDate,
    DateOnly? EndDate);
}
