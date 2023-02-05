namespace SparkPlug.Api.Controllers;

public class BaseController<TRepo, TEntity, TId> : ControllerBase where TRepo : IRepository<TId, TEntity> where TEntity : IBaseModel<TId>
{
    protected readonly TRepo _repository;
    protected readonly ILogger<BaseController<TRepo, TEntity, TId>> _logger;

    public BaseController(ILogger<BaseController<TRepo, TEntity, TId>> logger, TRepo repository)
    {
        _logger = logger;
        _repository = repository;
    }
}
