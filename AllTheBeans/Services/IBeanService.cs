using AllTheBeans.Models;

namespace AllTheBeans.Services;

public interface IBeanService
{
    public Task<List<Bean>> GetAllBeansAsync();
    public Task<Bean?> GetBeanAsync(string beanId);
    public Task<Bean> GetBeanOfTheDayAsync();
}