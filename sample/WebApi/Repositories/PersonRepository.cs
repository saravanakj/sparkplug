namespace SparkPlug.Sample.WebApi.Repositories;

[Api("person")]
public class PersonRepository : Repository<ObjectId, Person>
{
    public PersonRepository(IRepository<ObjectId, Person> repository) : base(repository) { }
}
