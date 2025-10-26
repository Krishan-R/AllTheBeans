using AllTheBeans.Models;

namespace AllTheBeans.Repositories;

public interface IBeanRepository
{
    public Task<List<Bean>> GetAllBeansAsync();
    public Task<Bean?> GetBeanAsync(string beanId);
    public Task<BeanOfTheDay?> GetBeanOfTheDayAsync();
    public Task<BeanOfTheDay> GetLastBeanOfTheDayAsync();
    public Task UpdateBeanAsync(Bean bean);
    public Task UpdateBeansAsync(List<Bean> beans);
    public Task SetNewBeanOfTheDayAsync(BeanOfTheDay beanOfTheDay);
}