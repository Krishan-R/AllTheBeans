using AllTheBeans.Models;
using Microsoft.EntityFrameworkCore;

namespace AllTheBeans.Database;

public class AllTheBeansDbContext(DbContextOptions<AllTheBeansDbContext> options) : DbContext(options)
{
    private const string AllTheBeansJson = "AllTheBeans 1 (1).json";

    public DbSet<Bean> Beans { get; set; }
    public DbSet<BeanOfTheDay> BeanOfTheDay { get; set; }
    public DbSet<Colour> Colours { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var json = File.ReadAllText(AllTheBeansJson);
        var (beans, beanOfTheDay, colours, countries) = JsonBeanMapper.MapFromJson(json);

        modelBuilder.Entity<Country>(builder =>
        {
            builder.HasIndex(c => c.CountryName).IsUnique();

            builder.HasData(countries);
        });

        modelBuilder.Entity<Colour>(builder =>
        {
            builder.HasIndex(c => c.ColourName).IsUnique();

            builder.HasData(colours);
        });

        modelBuilder.Entity<Bean>(builder =>
        {
            builder.HasData(beans);
        });

        modelBuilder.Entity<BeanOfTheDay>(builder =>
        {
            builder.HasData(beanOfTheDay);
        });
    }
}