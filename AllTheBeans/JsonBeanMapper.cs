using System.Text.Json;
using AllTheBeans.Models;

namespace AllTheBeans;

public static class JsonBeanMapper
{
    public static (List<Bean>, List<BeanOfTheDay>, List<Colour>, IEnumerable<Country>) MapFromJson(string json)
    {
        var jsonBeans = JsonSerializer.Deserialize<List<JsonBean>>(json);

        if (jsonBeans is null)
        {
            throw new InvalidOperationException("Unable to parse provided JSON");
        }

        var countries = MapCountries(jsonBeans);

        return ([], [], [], countries);
    }

    private static IEnumerable<Country> MapCountries(List<JsonBean> jsonBeans)
    {
        var countries = jsonBeans.Select(x => x.Country).Distinct().Select(y => new Country{ CountryName = y});

        return countries;
    }
}