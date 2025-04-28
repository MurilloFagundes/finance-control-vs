using FinanceControl.Application.Features.FixedIncomes.Projections;
using Marten;

namespace FinanceControl.Application.Features.FixedIncomes.Queries
{
    public static class GetFixedIncomesQueryHandler
    {
        public static IAsyncEnumerable<FixedIncomeSummary> Handle(GetFixedIncomesQuery query, IQuerySession session)
        {
            return session.Query<FixedIncomeSummary>().ToAsyncEnumerable();
        }
    }
}
