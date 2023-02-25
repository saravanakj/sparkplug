namespace SparkPlug.Sample.WebApi.Repositories;

public class PersonRepository : Repository<ObjectId, Person>
{
    public PersonRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
}
