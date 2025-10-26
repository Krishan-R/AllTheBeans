using System.ComponentModel.DataAnnotations.Schema;

namespace AllTheBeans.Models;

public class Bean
{
    public required string Id { get; init; }
    public bool IsBOTD { get; init; }
    public float CostInGBP { get; init; }
    public required string ImageUrl { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    public Country Country { get; init; }
    [ForeignKey("CountryId")]
    public int CountryId { get; set; }

    public Colour Colour { get; init; }
    [ForeignKey("ColourId")]
    public int ColourId { get; set; }
}