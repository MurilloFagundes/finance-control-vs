namespace FinanceControl.Domain.Entities
{
    using FinanceControl.Domain.Common;
    using FinanceControl.Domain.Events.FixedIncomes;

    public class FixedIncome : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateOnly StartDate { get; private set; }

        // Necessário para Marten recriar o Aggregate
        protected FixedIncome() { }

        // Aplica evento para construir o objeto
        public FixedIncome(FixedIncomeAdded @event)
        {
            Id = @event.Id;
            Description = @event.Description;
            Amount = @event.Amount;
            StartDate = @event.StartDate;
        }

        // Método para reagir a eventos futuros (se quiser suportar updates, etc.)
        public void Apply(FixedIncomeAdded @event)
        {
            Id = @event.Id;
            Description = @event.Description;
            Amount = @event.Amount;
            StartDate = @event.StartDate;
        }
    }
}
