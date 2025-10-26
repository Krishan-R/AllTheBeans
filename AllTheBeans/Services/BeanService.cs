using AllTheBeans.Models;
using AllTheBeans.Repositories;

namespace AllTheBeans.Services;

public class BeanService : IBeanService
{
    private readonly IBeanRepository _beanRepository;
    private readonly ILogger<BeanService> _logger;

    public BeanService(IBeanRepository beanRepository, ILogger<BeanService> logger)
    {
        _beanRepository = beanRepository;
        _logger = logger;
    }

    public Task<List<Bean>> GetAllBeansAsync()
    {
        _logger.LogDebug("{Method} called", nameof(GetAllBeansAsync));
        return _beanRepository.GetAllBeansAsync();
    }

    public Task<Bean> GetBeanAsync(string beanId)
    {
        _logger.LogDebug("{Method} called with BeanId: '{BeanId}'", nameof(GetBeanAsync), beanId);
        return _beanRepository.GetBeanAsync(beanId);
    }

    public Task<Bean> GetBeanOfTheDayAsync()
    {
        _logger.LogDebug("{Method} called", nameof(GetBeanOfTheDayAsync));
        return _beanRepository.GetBeanOfTheDayAsync();
    }
}