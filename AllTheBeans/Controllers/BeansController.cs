using AllTheBeans.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllTheBeans.Controllers;

[ApiController]
[Route("[controller]")]
public class BeansController
{
    private readonly IBeanService _beanService;
    private readonly ILogger<BeansController> _logger;

    public BeansController(IBeanService beanService, ILogger<BeansController> logger)
    {
        _beanService = beanService;
        _logger = logger;
    }

    [HttpGet("GetAllBeans")]
    public async Task<IResult> GetAllBeansAsync()
    {
        try
        {
            var beans = await _beanService.GetAllBeansAsync();
            return Results.Ok(beans);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when {Endpoint} called", nameof(GetAllBeansAsync));
            return Results.InternalServerError();
        }
    }

    [HttpGet("GetBean")]
    public async Task<IResult> GetBeanAsync(string beanId)
    {
        try
        {
            var bean = await _beanService.GetBeanAsync(beanId);

            if (bean is not null)
            {
                return Results.Ok(bean);
            }

            return Results.NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when {Endpoint} called", nameof(GetBeanAsync));
            return Results.InternalServerError();
        }
    }

    [HttpGet("BeanOfTheDay")]
    public async Task<IResult> GetBeanOfTheDayAsync()
    {
        try
        {
            var bean = await _beanService.GetBeanOfTheDayAsync();
            return Results.Ok(bean);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when {Endpoint} called", nameof(GetBeanOfTheDayAsync));
            return Results.InternalServerError();
        }
    }
}