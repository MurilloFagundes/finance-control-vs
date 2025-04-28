namespace FinanceControl.Infrastructure.DependencyInjection
{
    using FinanceControl.Application.Features.FixedIncomes.Projections;
    using Marten;
    using Marten.Events.Projections;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Polly;
    using Polly.CircuitBreaker;
    using Polly.Retry;
    using Polly.Timeout;
    using Weasel.Core;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMarten(opts =>
            {
                opts.Connection(configuration.GetConnectionString("Database")
                                ?? "Host=localhost;Port=5431;Database=financecontrol;Username=postgres;Password=postgres");
                opts.DatabaseSchemaName = "public";

                opts.Events.StreamIdentity = Marten.Events.StreamIdentity.AsGuid;
                opts.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

                opts.ConfigurePolly(builder =>
                {
                    builder.AddRetry(new RetryStrategyOptions
                    {
                        MaxRetryAttempts = 3,
                        Delay = TimeSpan.FromSeconds(2),
                        BackoffType = DelayBackoffType.Exponential
                    });

                    builder.AddTimeout(new TimeoutStrategyOptions
                    {
                        Timeout = TimeSpan.FromSeconds(10)
                    });

                    builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions
                    {
                        FailureRatio = 0.5,             // 50% de falhas
                        MinimumThroughput = 10,          // Min 10 operações para calcular
                        BreakDuration = TimeSpan.FromSeconds(30)
                    });
                });

                opts.Projections.Add<FixedIncomeSummaryProjection>(ProjectionLifecycle.Async);
            })
            .UseLightweightSessions()
            .ApplyAllDatabaseChangesOnStartup()
            .AddAsyncDaemon(Marten.Events.Daemon.Resiliency.DaemonMode.HotCold);

            // Aqui você pode registrar outros serviços de infra (ex: repositórios, filas, cache)
            return services;
        }
    }
}
