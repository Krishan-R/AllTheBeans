using AllTheBeans.Controllers;
using AllTheBeans.Models;
using AllTheBeans.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace AllTheBeans.UnitTests.Controllers;

public class BeansControllerTests
{
    private BeansController _beansController;
    private Mock<IBeanService> _mockBeanService;

    private readonly List<Bean> _beans =
    [
        new()
        {
            Id = "66a374596122a40616cb8599",
            IsBOTD = false,
            CostInGBP = 39.26,
            ImageUrl = "https://images.unsplash.com/photo-1672306319681-7b6d7ef349cf",
            Name = "TURNABOUT",
            Description =
                "Ipsum cupidatat nisi do elit veniam Lorem magna. Ullamco qui exercitation fugiat pariatur sunt dolore Lorem magna magna pariatur minim. Officia amet incididunt ad proident. Dolore est irure ex fugiat. Voluptate sunt qui ut irure commodo excepteur enim labore incididunt quis duis. Velit anim amet tempor ut labore sint deserunt.\r\n",
            CountryId = 1,
            ColourId = 1,
        },
        new()
        {
            Id = "66a374591a995a2b48761408",
            IsBOTD = false,
            CostInGBP = 18.57,
            ImageUrl = "https://images.unsplash.com/photo-1641399756770-9b0b104e67c1",
            Name = "ISONUS",
            Description =
                "Dolor fugiat duis dolore ut occaecat. Excepteur nostrud velit aute dolore sint labore do eu amet. Anim adipisicing quis ut excepteur tempor magna reprehenderit non ut excepteur minim. Anim dolore eiusmod nisi nulla aliquip aliqua occaecat.\r\n",
            CountryId = 2,
            ColourId = 2,
        }
    ];

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