using AllTheBeans.Models;

namespace AllTheBeans.Repositories;

public interface IBeanRepository
{
    public Task<List<Bean>> GetAllBeansAsync();
    public Task<Bean> GetBeanAsync(string beanId);
    public Task<Bean> GetBeanOfTheDayAsync();
}