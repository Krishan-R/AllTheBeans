using System.ComponentModel.DataAnnotations.Schema;

namespace AllTheBeans.Models;

public class BeanOfTheDay
{
    public int Id { get; init; }
    public DateTime Date { get; init; }

    [ForeignKey("BeanId")]
    public string BeanId { get; init; }
    public Bean Bean { get; init; }
}