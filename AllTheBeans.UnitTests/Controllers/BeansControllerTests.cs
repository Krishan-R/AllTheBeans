using AllTheBeans.Controllers;
using AllTheBeans.Models;
using AllTheBeans.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace AllTheBeans.UnitTests.Controllers;

public class BeansControllerTests : BeanTestBase
{
    private BeansController _beansController;
    private Mock<IBeanService> _mockBeanService;

    [SetUp]
    public void Setup()
    {
        _mockBeanService = new Mock<IBeanService>();

        _beansController = new BeansController(_mockBeanService.Object, NullLogger<BeansController>.Instance);
    }

    [Test]
    public async Task GetAllBeansAsync_CallsBeanService_Returns200()
    {
        _mockBeanService.Setup(x => x.GetAllBeansAsync())
            .ReturnsAsync(_beans)
            .Verifiable(Times.Once);

        var result = await _beansController.GetAllBeansAsync();

        Assert.That(((Microsoft.AspNetCore.Http.HttpResults.Ok<List<Bean>>)result).Value, Is.EqualTo(_beans));
        _mockBeanService.Verify();
    }

    [Test]
    public async Task GetAllBeansAsync_WhenExceptionThrown_Returns500()
    {
        _mockBeanService.Setup(x => x.GetAllBeansAsync())
            .ThrowsAsync(new Exception())
            .Verifiable(Times.Once);

        var result = await _beansController.GetAllBeansAsync();

        Assert.That((Microsoft.AspNetCore.Http.HttpResults.InternalServerError)result, Is.Not.Null);
        _mockBeanService.Verify();
    }

    [Test]
    public async Task GetBeanAsync_CallsBeanService_Returns200()
    {
        _mockBeanService.Setup(x => x.GetBeanAsync(It.IsAny<string>()))
            .ReturnsAsync(_beans[0])
            .Verifiable(Times.Once);

        var result = await _beansController.GetBeanAsync("66a374596122a40616cb8599");

        Assert.That(((Microsoft.AspNetCore.Http.HttpResults.Ok<Bean>)result).Value, Is.EqualTo(_beans[0]));
        _mockBeanService.Verify();
    }

    [Test]
    public async Task GetBeanAsync_WhenBeanNotFound_Returns404()
    {
        _mockBeanService.Setup(x => x.GetBeanAsync(It.IsAny<string>()))
            .ReturnsAsync((Bean)null!)
            .Verifiable(Times.Once);

        var result = await _beansController.GetBeanAsync("unknown bean");

        Assert.That((Microsoft.AspNetCore.Http.HttpResults.NotFound)result, Is.Not.Null);
        _mockBeanService.Verify();
    }

    [Test]
    public async Task GetBeanAsync_WhenExceptionThrown_Returns500()
    {
        _mockBeanService.Setup(x => x.GetBeanAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception())
            .Verifiable(Times.Once);

        var result = await _beansController.GetBeanAsync("66a374596122a40616cb8599");

        Assert.That((Microsoft.AspNetCore.Http.HttpResults.InternalServerError)result, Is.Not.Null);
        _mockBeanService.Verify();
    }

    [Test]
    public async Task GetBeanOfTheDayAsync_CallsBeanService_Returns200()
    {
        _mockBeanService.Setup(x => x.GetBeanOfTheDayAsync())
            .ReturnsAsync(_beans[0])
            .Verifiable(Times.Once);

        var result = await _beansController.GetBeanOfTheDayAsync();

        Assert.That(((Microsoft.AspNetCore.Http.HttpResults.Ok<Bean>)result).Value, Is.EqualTo(_beans[0]));
        _mockBeanService.Verify();
    }

    [Test]
    public async Task GetBeanOfTheDayAsync_WhenExceptionThrown_Returns500()
    {
        _mockBeanService.Setup(x => x.GetBeanOfTheDayAsync())
            .ThrowsAsync(new Exception())
            .Verifiable(Times.Once);

        var result = await _beansController.GetBeanOfTheDayAsync();

        Assert.That((Microsoft.AspNetCore.Http.HttpResults.InternalServerError)result, Is.Not.Null);
        _mockBeanService.Verify();
    }
}