namespace AllTheBeans.UnitTests;

public class JsonBeanMapperTests
{
    private string _json;

    [SetUp]
    public void Setup()
    {
        _json = File.ReadAllText("AllTheBeans.json");
    }

    [Test]
    public void MapFromJson_CorrectlyReturnsMappedModels()
    {
        var (beans, beanOfTheDay, colours, countries) = JsonBeanMapper.MapFromJson(_json);

        Assert.Multiple(() =>
        {
            Assert.That(beans.ToList(), Has.Count.EqualTo(15));
            Assert.That(beanOfTheDay.ToList(), Has.Count.EqualTo(1));
            Assert.That(colours.ToList(), Has.Count.EqualTo(5));
            Assert.That(countries.ToList(), Has.Count.EqualTo(5));
        });
    }
}