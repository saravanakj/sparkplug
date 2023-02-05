namespace SparkPlug.Sample.WebApi.Repositories;

[Api("department")]
public class DepartmentRepository : Repository<ObjectId, Department>
{
    public DepartmentRepository(IRepository<ObjectId, Department> repository) : base(repository) { }
}
