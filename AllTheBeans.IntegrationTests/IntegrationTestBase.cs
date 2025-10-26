using AllTheBeans.Database;
using DotNet.Testcontainers.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace AllTheBeans.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected AllTheBeansDbContext AllTheBeansDbContext;
    protected string ConnectionString;

    [SetUp]
    public async Task TestcontainersOneTimeSetUp()
    {
        var databaseContainer = new MsSqlBuilder()
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Recovery is complete"))
            .Build();

        await databaseContainer.StartAsync();

        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

        await Task.Delay(1000);

        ConnectionString = databaseContainer.GetConnectionString();

        var builder = new DbContextOptionsBuilder<AllTheBeansDbContext>();
        builder.UseSqlServer(ConnectionString)
            .UseInternalServiceProvider(serviceProvider);

        AllTheBeansDbContext = new AllTheBeansDbContext(builder.Options);
        await AllTheBeansDbContext.Database.EnsureCreatedAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await AllTheBeansDbContext.DisposeAsync();
    }
}