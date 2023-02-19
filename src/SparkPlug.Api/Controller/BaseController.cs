using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SparkPlug.Api.Controllers;

public abstract class BaseController<TRepo, TEntity, TId> : ControllerBase where TRepo : IRepository<TId, TEntity> where TEntity : class, IBaseEntity<TId>
{
    protected readonly TRepo _repository;
    protected readonly ILogger<BaseController<TRepo, TEntity, TId>> _logger;
    protected readonly IServiceProvider _serviceProvider;

    protected BaseController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _repository = serviceProvider.GetRequiredService<TRepo>();
        _logger = serviceProvider.GetRequiredService<ILogger<BaseController<TRepo, TEntity, TId>>>();
    }
    [NonAction]
    public OkObjectResult Ok([ActionResultObjectValue] IEnumerable<TEntity> data, [ActionResultObjectValue] IPageContext? pagecontext)
    {
        return Ok(new QueryResponse<TEntity>(data.ToArray(), pagecontext));
    }
    [NonAction]
    public OkObjectResult Ok([ActionResultObjectValue] TEntity data)
    {
        return Ok(new CommandResponse<TEntity>(data));
    }
}
