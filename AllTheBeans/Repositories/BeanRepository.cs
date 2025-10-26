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

    public async Task<BeanOfTheDay?> GetBeanOfTheDayAsync()
    {
        _logger.LogDebug("{Method} called", nameof(GetBeanOfTheDayAsync));

        var beanOfTheDay = await _dbContext.BeanOfTheDay
            .Include(bean => bean.Bean)
            .Include(bean => bean.Bean.Country)
            .Include(bean => bean.Bean.Colour)
            .SingleOrDefaultAsync(bean => bean.Date.Date == DateTime.UtcNow.Date);

        return beanOfTheDay;
    }

    public async Task<BeanOfTheDay> GetLastBeanOfTheDayAsync()
    {
        return await _dbContext.BeanOfTheDay
            .Include(bean => bean.Bean)
            .Include(bean => bean.Bean.Country)
            .Include(bean => bean.Bean.Colour)
            .OrderByDescending(bean => bean.Date)
            .FirstAsync();
    }

    public async Task UpdateBeanAsync(Bean bean)
    {
        _dbContext.Beans.Update(bean);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBeansAsync(List<Bean> beans)
    {
        _dbContext.Beans.UpdateRange(beans);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SetNewBeanOfTheDayAsync(BeanOfTheDay beanOfTheDay)
    {
        _dbContext.BeanOfTheDay.Add(beanOfTheDay);
        await _dbContext.SaveChangesAsync();
    }
}