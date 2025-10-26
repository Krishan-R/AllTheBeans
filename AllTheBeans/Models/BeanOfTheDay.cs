namespace AllTheBeans.Models;

public class BeanOfTheDay
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public required string BeanId { get; init; }
}