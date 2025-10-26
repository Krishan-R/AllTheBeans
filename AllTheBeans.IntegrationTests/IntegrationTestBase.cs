using AllTheBeans.Database;
using DotNet.Testcontainers.Builders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MsSql;

namespace AllTheBeans.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected AllTheBeansDbContext AllTheBeansDbContext;

    private readonly MsSqlContainer _databaseContainer = new MsSqlBuilder()
        .WithWaitStrategy(Wait.ForUnixContainer())
        .WithAutoRemove(false)
        .Build();

    private Respawner _respawner;

    [OneTimeSetUp]
    public async Task TestcontainersOneTimeSetUp()
    {
        await _databaseContainer.StartAsync();

        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

        await Task.Delay(1000);

        var builder = new DbContextOptionsBuilder<AllTheBeansDbContext>();
        builder.UseSqlServer(_databaseContainer.GetConnectionString())
            .UseInternalServiceProvider(serviceProvider);

        AllTheBeansDbContext = new AllTheBeansDbContext(builder.Options);
        await AllTheBeansDbContext.Database.EnsureCreatedAsync();

        await using var connection = new SqlConnection(_databaseContainer.GetConnectionString());
        await connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            WithReseed = true,
            DbAdapter = DbAdapter.SqlServer,
            TablesToIgnore = ["Users"]
        });
    }

    [OneTimeTearDown]
    public async Task TearDownAsync()
    {
        await AllTheBeansDbContext.DisposeAsync();
        await _databaseContainer.StopAsync();
        await _databaseContainer.DisposeAsync();
    }

    [SetUp]
    public async Task TestcontainersSetupAsync()
    {
        await using var connection = new SqlConnection(_databaseContainer.GetConnectionString());
        await connection.OpenAsync();
        await _respawner.ResetAsync(connection);

        AllTheBeansDbContext.ChangeTracker.Clear();
    }
}