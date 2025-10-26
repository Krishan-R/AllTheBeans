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
        _mockBeanRepository.Setup(x => x.GetBeanOfTheDayAsync())
            .ReturnsAsync(_beans[0])
            .Verifiable(Times.Once);

        var result = await _beanService.GetBeanOfTheDayAsync();

        Assert.That(result, Is.EqualTo(_beans[0]));
        _mockBeanRepository.Verify();
    }
}