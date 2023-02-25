namespace SparkPlug.Persistence.Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    object CreatedBy { get; set; }
    DateTime ModifiedAt { get; set; }
    object ModifiedBy { get; set; }
}
