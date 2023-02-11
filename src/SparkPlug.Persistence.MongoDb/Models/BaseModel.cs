namespace SparkPlug.Persistence.MongoDb.Models;

[BsonIgnoreExtraElements]
public abstract class BaseEntity<TId> : IBaseEntity<TId>
{
    [BsonElement("_id")]
    [BsonId]
    [BsonIgnoreIfDefault]
    [BsonRepresentation(BsonType.ObjectId)]
    public virtual TId? Id { get; set; }

    public virtual TId? GetId()
    {
        return Id;
    }
}