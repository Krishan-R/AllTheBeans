using AllTheBeans.Models;
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
            Assert.That(result.Bean.IsBOTD, Is.True);
            Assert.That(result.Bean.Id, Is.Not.Null);
            Assert.That(result.Bean.CostInGBP, Is.Not.EqualTo(0));
            Assert.That(result.Bean.ImageUrl, Is.Not.Null);
            Assert.That(result.Bean.Name, Is.Not.Null);
            Assert.That(result.Bean.Description, Is.Not.Null);
            Assert.That(result.Bean.Country, Is.Not.Null);
            Assert.That(result.Bean.Colour, Is.Not.Null);
        });
    }

    [Test]
    public async Task GetBeanOfTheDayAsync_WithoutBeanOfTheDay_ReturnsNull()
    {
        UpdateCurrentBeanOfTheDay(DateTime.UtcNow.AddDays(-1));

        var result = await _beanRepository.GetBeanOfTheDayAsync();

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetLastBeanOfTheDayAsync_ReturnsTheLatestBeanOfTheDay()
    {
        UpdateCurrentBeanOfTheDay(DateTime.UtcNow.AddDays(-1));

        var result = await _beanRepository.GetLastBeanOfTheDayAsync();

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Bean.IsBOTD, Is.True);
            Assert.That(result.Bean.Id, Is.Not.Null);
            Assert.That(result.Bean.CostInGBP, Is.Not.EqualTo(0));
            Assert.That(result.Bean.ImageUrl, Is.Not.Null);
            Assert.That(result.Bean.Name, Is.Not.Null);
            Assert.That(result.Bean.Description, Is.Not.Null);
            Assert.That(result.Bean.Country, Is.Not.Null);
            Assert.That(result.Bean.Colour, Is.Not.Null);
        });
    }

    [Test]
    public async Task UpdateBeanAsync_UpdatesBean()
    {
        var bean = (await _beanRepository.GetAllBeansAsync()).First();

        bean.Name = "new name";

        await _beanRepository.UpdateBeanAsync(bean);

        var result = await _beanRepository.GetBeanAsync(bean.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("new name"));
    }

    [Test]
    public async Task UpdateBeansAsync_UpdatesBean()
    {
        var beans = (await _beanRepository.GetAllBeansAsync());

        beans = beans.Select(x =>
        {
            x.Name = "new name";
            return x;
        }).ToList();

        await _beanRepository.UpdateBeansAsync(beans);

        var result = await _beanRepository.GetAllBeansAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.All.With.Property("Name").EqualTo("new name"));
    }

    [Test]
    public async Task SetNewBeanOfTheDayAsync_AddsNewRecord()
    {
        UpdateCurrentBeanOfTheDay(DateTime.UtcNow.AddDays(-5));

        var beans = await _beanRepository.GetAllBeansAsync();
        var beanOfTheDay = new BeanOfTheDay
        {
            Date = DateTime.UtcNow,
            BeanId = beans[3].Id,
            Bean = beans[3]
        };

        await _beanRepository.SetNewBeanOfTheDayAsync(beanOfTheDay);

        var result = await _beanRepository.GetBeanOfTheDayAsync();

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.BeanId, Is.EqualTo(beans[3].Id));
            Assert.That(result.Bean, Is.EqualTo(beans[3]).UsingPropertiesComparer());
            Assert.That(result.Date, Is.EqualTo(DateTime.UtcNow).Within(3).Seconds);
            Assert.That(result.Id, Is.Not.EqualTo(0));
        });
    }
}