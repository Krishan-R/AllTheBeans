using AllTheBeans.Database;
using AllTheBeans.Models;

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

    public Task<List<Bean>> GetAllBeansAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Bean> GetBeanAsync(string beanId)
    {
        throw new NotImplementedException();
    }

    public Task<Bean> GetBeanOfTheDayAsync()
    {
        throw new NotImplementedException();
    }
}