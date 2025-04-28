namespace FinanceControl.Application.Features.FixedIncomes.Commands
{
    using FinanceControl.Domain.Entities;
    using FinanceControl.Domain.Events.FixedIncomes;
    using Marten;

    public static class AddFixedIncomeHandler
    {
        public static async Task<Guid> Handle(AddFixedIncomeCommand command, IDocumentSession session)
        {
            var evt = new FixedIncomeAdded(
                Guid.NewGuid(),
                command.Description,
                command.Amount,
                command.StartDate
            );

            var streamId = session.Events.StartStream<FixedIncome>(evt);

            await session.SaveChangesAsync();
            return streamId.Id;
        }
    }
}
