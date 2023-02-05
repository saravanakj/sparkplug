namespace SparkPlug.Sample.WebApi.Repositories;

[Api("department")]
public class DepartmentRepository : MongoRepository<ObjectId, Department>
{
    public DepartmentRepository(IMongoDbContext context, ILogger<DepartmentRepository> logger) : base(context, logger) { }
}
