using AllTheBeans.Models;

namespace AllTheBeans.Services;

public interface IBeanService
{
    public Task<List<Bean>> GetAllBeansAsync();
    Task<Bean> GetBeanAsync(string beanId);
    Task<Bean> GetBeanOfTheDayAsync();
}