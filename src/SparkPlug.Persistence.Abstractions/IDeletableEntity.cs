namespace SparkPlug.Persistence.Abstractions;

public enum Status
{
    Live = 1,
    Deleted = 2
}
public interface IDeletableEntity
{
    Status Status { get; set; }
}
