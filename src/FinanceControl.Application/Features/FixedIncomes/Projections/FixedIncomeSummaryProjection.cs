using FinanceControl.Domain.Events.FixedIncomes;
using Marten.Events.Aggregation;

namespace FinanceControl.Application.Features.FixedIncomes.Projections
{
    public class FixedIncomeSummaryProjection : SingleStreamProjection<FixedIncomeSummary>
    {
        public FixedIncomeSummary Create(FixedIncomeAdded @event)
        {
            return new FixedIncomeSummary
            {
                Id = @event.Id,
                Description = @event.Description,
                Amount = @event.Amount,
                StartDate = @event.StartDate
            };
        }
    }
}
