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
    }
}