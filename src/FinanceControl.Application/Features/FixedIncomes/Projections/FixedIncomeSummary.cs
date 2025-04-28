namespace FinanceControl.Application.Features.FixedIncomes.Projections
{
    public class FixedIncomeSummary
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateOnly StartDate { get; set; }
    }
}
