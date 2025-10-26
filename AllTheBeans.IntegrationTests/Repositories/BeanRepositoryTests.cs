using AllTheBeans.Repositories;
using Microsoft.Extensions.Logging.Abstractions;

namespace AllTheBeans.IntegrationTests.Repositories;

public class BeanRepositoryTests : IntegrationTestBase
{
    private BeanRepository _beanRepository;

    [SetUp]
    public void Setup()
    {
        _beanRepository = new BeanRepository(AllTheBeansDbContext, NullLogger<BeanRepository>.Instance);
    }

    [Test]
    public void testsas()
    {
        Assert.That(true, Is.EqualTo(true));
    }
}