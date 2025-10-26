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
        var beans = MapBeans(jsonBeans, countries, colours);
        var beanOfTheDay = MapBeanOfTheDay(jsonBeans);

        return (beans, [], colours, countries);
        // return ([], [], colours, countries);
    }

    private static IEnumerable<Country> MapCountries(IEnumerable<JsonBean> jsonBeans)
    {
        var countries = jsonBeans.Select(x => x.Country).Distinct()
            .Select((y, index) => new Country { CountryName = y, Id = index + 1});

        return countries;
    }

    private static IEnumerable<Colour> MapColours(IEnumerable<JsonBean> jsonBeans)
    {
        var colours = jsonBeans.Select(x => x.colour).Distinct()
            .Select((y, index) => new Colour { ColourName = y, Id = index + 1});

        return colours;
    }

    private static IEnumerable<Bean> MapBeans(IEnumerable<JsonBean> jsonBeans, IEnumerable<Country> countries, IEnumerable<Colour> colours)
    {
        var beans = jsonBeans.Select(x =>
        {
            var country = countries.First(y => y.CountryName == x.Country);
            var colour = colours.First(y => y.ColourName == x.colour);

            return new Bean
            {
                Id = x._id,
                Name = x.Name,
                IsBOTD = x.isBOTD,
                CostInGBP = float.Parse(x.Cost[1..]),
                ImageUrl = x.Image,
                Description = x.Description,
                Country = country,
                // CountryId = country.Id,
                Colour = colour,
                // ColourId = colour.Id
            };
        });

        return beans;
    }

    private static IEnumerable<BeanOfTheDay> MapBeanOfTheDay(IEnumerable<JsonBean> jsonBeans)
    {
        var beanOfTheDay = jsonBeans.Where(x => x.isBOTD).Select((y, index) =>
            new BeanOfTheDay
            {
                Id = index + 1,
                Date = DateTime.UtcNow,
                BeanId = y._id
            });

        return beanOfTheDay;
    }
}