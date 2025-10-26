using AllTheBeans.Database;
using AllTheBeans.Repositories;
using AllTheBeans.Services;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AllTheBeans;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Could not find suitable Connection String");
        }

        builder.Services.AddHealthChecks()
            .AddSqlServer(connectionString);

        builder.Logging.AddOpenTelemetry(options => options
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(nameof(AllTheBeans)))
                .AddOtlpExporter()
        );

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resourceBuilder => resourceBuilder.AddService(nameof(AllTheBeans)))
            .WithTracing(providerBuilder => providerBuilder
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter())
            .WithMetrics(providerBuilder => providerBuilder
                .AddPrometheusExporter()
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter());

        builder.Services.AddDbContextPool<AllTheBeansDbContext>(opt => opt
            .UseSqlServer(connectionString));

        builder.Services.AddScoped<IBeanService, BeanService>();
        builder.Services.AddScoped<IBeanRepository, BeanRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseAuthorization();

        app.MapControllers();

        app.MapPrometheusScrapingEndpoint();

        app.MapHealthChecks("/Health");

        app.UseHealthChecksPrometheusExporter("/metrics");

        app.Run();
    }
}