using System.Text.Json;
using AllTheBeans.Models;

namespace AllTheBeans;

public static class JsonBeanMapper
{
    public static (IEnumerable<Bean>, IEnumerable<BeanOfTheDay>, IEnumerable<Colour>, IEnumerable<Country>) MapFromJson(string json)
    {
        var jsonBeans = JsonSerializer.Deserialize<List<JsonBean>>(json);

        if (jsonBeans is null)
        {
            throw new InvalidOperationException("Unable to parse provided JSON");
        }

        var countries = MapCountries(jsonBeans);
        var colours = MapColours(jsonBeans);

        return ([], [], colours, countries);
    }

    private static IEnumerable<Colour> MapColours(List<JsonBean> jsonBeans)
    {
        var colours = jsonBeans.Select(x => x.colour).Distinct()
            .Select((y, index) => new Colour { ColourName = y, Id = index + 1});

        return colours;
    }

    private static IEnumerable<Country> MapCountries(List<JsonBean> jsonBeans)
    {
        var countries = jsonBeans.Select(x => x.Country).Distinct()
            .Select((y, index) => new Country { CountryName = y, Id = index + 1});

        return countries;
    }
}