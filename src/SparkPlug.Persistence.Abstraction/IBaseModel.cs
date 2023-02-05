namespace SparkPlug.Persistence.Abstractions;

public interface IBaseModel<TId>
{
    public TId? Id { get; set; }
    public TId? GetId();
}