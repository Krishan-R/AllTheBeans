using AllTheBeans.Database;
using AllTheBeans.Models;
using Microsoft.EntityFrameworkCore;

namespace AllTheBeans.Repositories;

public class BeanRepository : IBeanRepository
{
    private readonly AllTheBeansDbContext _dbContext;
    private readonly ILogger<BeanRepository> _logger;

    public BeanRepository(AllTheBeansDbContext dbContext, ILogger<BeanRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Bean>> GetAllBeansAsync()
    {
        _logger.LogDebug("{Method} called", nameof(GetAllBeansAsync));

        return await _dbContext.Beans
            .Include(bean => bean.Country)
            .Include(bean => bean.Colour)
            .ToListAsync();
    }

    public async Task<Bean?> GetBeanAsync(string beanId)
    {
        _logger.LogDebug("{Method} called", nameof(GetBeanAsync));

        return await _dbContext.Beans
            .Include(bean => bean.Country)
            .Include(bean => bean.Colour)
            .SingleOrDefaultAsync(bean => bean.Id == beanId);
    }

    public async Task<Bean> GetBeanOfTheDayAsync()
    {
        _logger.LogDebug("{Method} called", nameof(GetBeanOfTheDayAsync));

        var beanOfTheDay = await _dbContext.BeanOfTheDay
            .Include(bean => bean.Bean)
            .Include(bean => bean.Bean.Country)
            .Include(bean => bean.Bean.Colour)
            .SingleOrDefaultAsync(bean => bean.Date.Date == DateTime.UtcNow.Date);

        if (beanOfTheDay is not null)
        {
            return beanOfTheDay.Bean;
        }

        await GenerateBeanOfTheDayAsync();

        return await GetBeanOfTheDayAsync();
    }

    private async Task GenerateBeanOfTheDayAsync()
    {
        var lastBeanOfTheDay = await _dbContext.BeanOfTheDay
            .Include(bean => bean.Bean)
            .OrderByDescending(bean => bean.Date)
            .FirstAsync();

        var beans = await _dbContext.Beans
            .Include(bean => bean.Country)
            .Include(bean => bean.Colour)
            .ToListAsync();

        var previousBeanOfTheDay = beans
            .Where(b => b.IsBOTD)
            .Select(b =>
        {
            b.IsBOTD = false;
            return b;
        }).ToList();

        _dbContext.Beans.UpdateRange(previousBeanOfTheDay);

        var eligibleBeans = beans.Where(x => x.Id != lastBeanOfTheDay.BeanId).ToList();

        var random = new Random();
        var chosenBean = random.Next(eligibleBeans.Count);

        var newBeanOfTheDay = new BeanOfTheDay
        {
            Date = DateTime.UtcNow,
            BeanId = eligibleBeans[chosenBean].Id,
            Bean = eligibleBeans[chosenBean]
        };

        eligibleBeans[chosenBean].IsBOTD = true;
        _dbContext.Beans.Update(eligibleBeans[chosenBean]);

        _dbContext.BeanOfTheDay.Add(newBeanOfTheDay);
        await _dbContext.SaveChangesAsync();
    }
}