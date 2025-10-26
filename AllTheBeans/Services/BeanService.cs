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

    public async Task<List<Bean>> GetAllBeansAsync()
    {
        _logger.LogDebug("{Method} called", nameof(GetAllBeansAsync));
        return await _beanRepository.GetAllBeansAsync();
    }

    public async Task<Bean?> GetBeanAsync(string beanId)
    {
        _logger.LogDebug("{Method} called with BeanId: '{BeanId}'", nameof(GetBeanAsync), beanId);
        return await _beanRepository.GetBeanAsync(beanId);
    }

    public async Task<Bean> GetBeanOfTheDayAsync()
    {
        _logger.LogDebug("{Method} called", nameof(GetBeanOfTheDayAsync));

        var beanOfTheDay = await _beanRepository.GetBeanOfTheDayAsync();

        if (beanOfTheDay is not null)
        {
            return beanOfTheDay.Bean;
        }

        _logger.LogInformation("Generating a new Bean of the Day");

        await GenerateBeanOfTheDayAsync();

        return await GetBeanOfTheDayAsync();
    }

    private async Task GenerateBeanOfTheDayAsync()
    {
        var lastBeanOfTheDay = await _beanRepository.GetLastBeanOfTheDayAsync();

        var beans = await _beanRepository.GetAllBeansAsync();

        var previousBeanOfTheDay = beans
            .Where(b => b.IsBOTD)
            .Select(b =>
            {
                b.IsBOTD = false;
                return b;
            }).ToList();

        await _beanRepository.UpdateBeansAsync(previousBeanOfTheDay);

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
        await _beanRepository.UpdateBeanAsync(eligibleBeans[chosenBean]);

        await _beanRepository.SetNewBeanOfTheDayAsync(newBeanOfTheDay);
    }
}