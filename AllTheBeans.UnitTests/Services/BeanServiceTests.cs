using AllTheBeans.Models;
using AllTheBeans.Repositories;
using AllTheBeans.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace AllTheBeans.UnitTests.Services;

public class BeanServiceTests : BeanTestBase
{
    private BeanService _beanService;
    private Mock<IBeanRepository> _mockBeanRepository;

    [SetUp]
    public void Setup()
    {
        _mockBeanRepository = new Mock<IBeanRepository>();

        _beanService = new BeanService(_mockBeanRepository.Object, NullLogger<BeanService>.Instance);
    }

    [Test]
    public async Task GetAllBeansAsync_CallsRepository()
    {
        _mockBeanRepository.Setup(x => x.GetAllBeansAsync())
            .ReturnsAsync(_beans)
            .Verifiable(Times.Once);

        var result = await _beanService.GetAllBeansAsync();

        Assert.That(result, Is.EqualTo(_beans));
        _mockBeanRepository.Verify();
    }

    [Test]
    public async Task GetBeanAsync_CallsRepository()
    {
        _mockBeanRepository.Setup(x => x.GetBeanAsync(It.IsAny<string>()))
            .ReturnsAsync(_beans[0])
            .Verifiable(Times.Once);

        var result = await _beanService.GetBeanAsync("66a374596122a40616cb8599");

        Assert.That(result, Is.EqualTo(_beans[0]));
        _mockBeanRepository.Verify();
    }

    [Test]
    public async Task GetBeanOfTheDayAsync_CallsRepository()
    {
        var beanOfTheDay = new BeanOfTheDay
        {
            Date = DateTime.UtcNow,
            BeanId = _beans[0].Id,
            Bean = _beans[0]
        };

        _mockBeanRepository.Setup(x => x.GetBeanOfTheDayAsync())
            .ReturnsAsync(beanOfTheDay)
            .Verifiable(Times.Once);

        var result = await _beanService.GetBeanOfTheDayAsync();

        Assert.That(result, Is.EqualTo(_beans[0]));
        _mockBeanRepository.Verify();
    }

    [Test]
    public async Task GetBeanOfTheDay_WhenRepositoryReturnsNull_GeneratesNewBeanOfTheDay()
    {
        var beanOfTheDay = new BeanOfTheDay
        {
            Date = DateTime.UtcNow,
            BeanId = _beans[0].Id,
            Bean = _beans[0]
        };

        var lastBeanOfTheDay = new BeanOfTheDay
        {
            Date = DateTime.UtcNow.AddDays(-1),
            BeanId = _beans[1].Id,
            Bean = _beans[1]
        };

        _mockBeanRepository.Setup(x => x.GetBeanOfTheDayAsync())
            .ReturnsAsync((BeanOfTheDay)null!)
            .Verifiable(Times.Once);

        _mockBeanRepository.Setup(x => x.GetLastBeanOfTheDayAsync())
            .ReturnsAsync(lastBeanOfTheDay)
            .Verifiable(Times.Once);

        _mockBeanRepository.Setup(x => x.GetAllBeansAsync())
            .ReturnsAsync(_beans)
            .Verifiable(Times.Once);

        _mockBeanRepository.Setup(x => x.UpdateBeansAsync(It.IsAny<List<Bean>>()))
            .Verifiable(Times.Once);

        _mockBeanRepository.Setup(x => x.UpdateBeanAsync(It.Is<Bean>(b => b.IsBOTD == true)))
            .Verifiable(Times.Once);

        _mockBeanRepository.Setup(x => x.SetNewBeanOfTheDayAsync(It.IsAny<BeanOfTheDay>()))
            .Callback(() =>
            {
                _mockBeanRepository.Setup(x => x.GetBeanOfTheDayAsync())
                    .ReturnsAsync(beanOfTheDay)
                    .Verifiable(Times.Once);
            });

        var result = await _beanService.GetBeanOfTheDayAsync();

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsBOTD, Is.True);
            Assert.That(result.Id, Is.EqualTo(_beans[0].Id));
            Assert.That(result.Id, Is.Not.EqualTo(lastBeanOfTheDay.Bean.Id));
        });
    }
}