namespace AllTheBeans.Models;

public class Bean
{
    public Guid Id { get; init; }
    public bool IsBOTD { get; init; }
    public float CostInGBP { get; init; }
    public required string ImageUrl { get; init; }
    public required Colour Colour { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Country Country { get; init; }
}