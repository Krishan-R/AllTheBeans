using AllTheBeans.Repositories;
using Microsoft.Extensions.Logging.Abstractions;

namespace AllTheBeans.IntegrationTests.Repositories;

public class BeanRepositoryTests : BeanTestBase
{
    private BeanRepository _beanRepository;

    [SetUp]
    public void Setup()
    {
        _beanRepository = new BeanRepository(AllTheBeansDbContext, NullLogger<BeanRepository>.Instance);
    }

    [Test]
    public async Task GetAllBeansAsync_ReturnsSeededBeans()
    {
        var results = await _beanRepository.GetAllBeansAsync();

        Assert.That(results, Is.Not.Null);
        Assert.That(results, Has.Count.EqualTo(15));
        Assert.Multiple(() =>
        {
            Assert.That(results.First().IsBOTD, Is.False);
            Assert.That(results.First().Id, Is.Not.Null);
            Assert.That(results.First().CostInGBP, Is.Not.EqualTo(0));
            Assert.That(results.First().ImageUrl, Is.Not.Null);
            Assert.That(results.First().Name, Is.Not.Null);
            Assert.That(results.First().Description, Is.Not.Null);
            Assert.That(results.First().Country, Is.Not.Null);
            Assert.That(results.First().Colour, Is.Not.Null);
        });
    }

    [Test]
    public async Task GetAllBeansAsync_ReturnsAllBeans()
    {
        InsertBean("66a214125123a", 25.32, "https://www.example.com", "nice bean", "a really nice bean", 1, 1);

        var results = await _beanRepository.GetAllBeansAsync();

        Assert.That(results, Is.Not.Null);
        Assert.That(results, Has.Count.EqualTo(16));
    }

    [Test]
    public async Task GetBeanAsync_ReturnsBean()
    {
        const string beanId = "3523nfas231245";
        const string imageUrl = "https://www.example.com";
        const string beanName = "nice bean";
        const string beanDescription = "a really nice bean";

        InsertBean(beanId, 12.34, imageUrl, beanName, beanDescription, 2, 2);

        var result = await _beanRepository.GetBeanAsync(beanId);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(beanId));
            Assert.That(result.ImageUrl, Is.EqualTo(imageUrl));
            Assert.That(result.Name, Is.EqualTo(beanName));
            Assert.That(result.Description, Is.EqualTo(beanDescription));
        });
    }

    [Test]
    public async Task GetBeanOfTheDayAsync_ReturnsBeanOfTheDay()
    {
        UpdateCurrentBeanOfTheDay(DateTime.UtcNow);

        var result = await _beanRepository.GetBeanOfTheDayAsync();

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsBOTD, Is.True);
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.CostInGBP, Is.Not.EqualTo(0));
            Assert.That(result.ImageUrl, Is.Not.Null);
            Assert.That(result.Name, Is.Not.Null);
            Assert.That(result.Description, Is.Not.Null);
            Assert.That(result.Country, Is.Not.Null);
            Assert.That(result.Colour, Is.Not.Null);
        });
    }

    [Test]
    public async Task GetBeanOfTheDayAsync_WithoutBeanOfTheDay_SetsNewBeanOfTheDay()
    {
        UpdateCurrentBeanOfTheDay(DateTime.UtcNow);
        var previousBeanOfTheDay = await _beanRepository.GetBeanOfTheDayAsync();

        UpdateCurrentBeanOfTheDay(DateTime.UtcNow.AddDays(-1));

        var result = await _beanRepository.GetBeanOfTheDayAsync();

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(previousBeanOfTheDay, Is.Not.EqualTo(result).UsingPropertiesComparer());
            Assert.That(result.IsBOTD, Is.True);
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.CostInGBP, Is.Not.EqualTo(0));
            Assert.That(result.ImageUrl, Is.Not.Null);
            Assert.That(result.Name, Is.Not.Null);
            Assert.That(result.Description, Is.Not.Null);
            Assert.That(result.Country, Is.Not.Null);
            Assert.That(result.Colour, Is.Not.Null);
        });
    }
}