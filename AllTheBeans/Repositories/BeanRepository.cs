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

    public Task<Bean> GetBeanOfTheDayAsync()
    {
        throw new NotImplementedException();
    }
}