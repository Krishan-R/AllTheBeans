using AllTheBeans.Models;
using Microsoft.EntityFrameworkCore;

namespace AllTheBeans.Database;

public class AllTheBeansDbContext(DbContextOptions<AllTheBeansDbContext> options) : DbContext(options)
{
    public DbSet<Bean> Beans { get; set; }
    public DbSet<BeanOfTheDay> BeanOfTheDay { get; set; }
    public DbSet<Colour> Colours { get; set; }
    public DbSet<Country> Countries { get; set; }
}