namespace SparkPlug.Sample.WebApi.Repositories;

[Api("person")]
public class PersonRepository : MongoRepository<ObjectId, Person>
{
    public PersonRepository(IMongoDbContext context, ILogger<PersonRepository> logger) : base(context, logger) { }
}
