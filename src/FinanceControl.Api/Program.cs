using FinanceControl.Api.Endpoints;
using FinanceControl.Application.DependencyInjection;
using FinanceControl.Application.Features.FixedIncomes.Commands;
using FinanceControl.Application.Features.FixedIncomes.Projections;
using FinanceControl.Infrastructure.DependencyInjection;
using Marten.Events.Daemon;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Configurações de serviços (Dependency Injection)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseWolverine(opts =>
{
    opts.Policies.AutoApplyTransactions();
    opts.UseFluentValidation();
    opts.Discovery.IncludeAssembly(typeof(AddFixedIncomeHandler).Assembly);
});

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🌐 Middlewares e Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        // Fluent API
        options
            .WithTitle("Custom API")
            .WithSidebar(false)
            .WithCdnUrl("https://cdn.jsdelivr.net/npm/@scalar/api-reference");

        // Object initializer
        options.ShowSidebar = false;
    });
}

//using (var scope = app.Services.CreateScope())
//{
//    var daemon = scope.ServiceProvider.GetRequiredService<IProjectionDaemon>();
//    await daemon.RebuildProjectionAsync<FixedIncomeSummaryProjection>(CancellationToken.None);
//}

// ✳️ Mapeamento de endpoints
app.UseHttpsRedirection();
app.MapFixedIncomeEndpoints();

app.Run();
