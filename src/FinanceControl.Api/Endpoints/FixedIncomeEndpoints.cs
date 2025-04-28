namespace FinanceControl.Api.Endpoints
{
    using FinanceControl.Application.Features.FixedIncomes.Commands;
    using FinanceControl.Application.Features.FixedIncomes.Projections;
    using FinanceControl.Application.Features.FixedIncomes.Queries;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Wolverine;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public static class FixedIncomeEndpoints
    {
        public static void MapFixedIncomeEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/fixed-incomes", async (AddFixedIncomeCommand command, IMessageBus bus) =>
            {
                var id = await bus.InvokeAsync<Guid>(command);
                return Results.Ok(id);
            });

            app.MapGet("/fixed-incomes", async (IMessageBus bus) =>
            {
                var id = await bus.InvokeAsync<IAsyncEnumerable<FixedIncomeSummary>>(new GetFixedIncomesQuery());
                return Results.Ok(id);
            });
        }
    }
}
