namespace SparkPlug.Persistence.MongoDb.Context;

public interface IMongoDbContext : IDbContext<MongoClient>
{
    IMongoDatabase Database { get; }
    IMongoCollection<TEntity> GetCollection<TEntity>(String collectionName);
}
