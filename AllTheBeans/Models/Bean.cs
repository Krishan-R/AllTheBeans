using System.ComponentModel.DataAnnotations.Schema;

namespace AllTheBeans.Models;

public class Bean
{
    public required string Id { get; init; }
    public bool IsBOTD { get; set; }
    public double CostInGBP { get; init; }
    public required string ImageUrl { get; init; }
    public required string Name { get; set; }
    public required string Description { get; init; }

    [ForeignKey("CountryId")]
    public int CountryId { get; set; }
    public Country Country { get; init; }

    [ForeignKey("ColourId")]
    public int ColourId { get; set; }
    public Colour Colour { get; init; }
}