namespace SparkPlug.Persistence.MongoDb.Models;

[BsonIgnoreExtraElements]
public abstract class BaseEntity<TId> : IBaseEntity<TId> where TId : new()
{
    [BsonElement("_id")]
    [BsonId]
    [BsonIgnoreIfDefault]
    [BsonRepresentation(BsonType.String)]
    public virtual TId Id { get; set; } = new();

    public virtual TId GetId()
    {
        return Id;
    }
}
