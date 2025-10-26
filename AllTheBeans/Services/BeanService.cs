using AllTheBeans.Models;

namespace AllTheBeans.Services;

public class BeanService : IBeanService
{
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