namespace SparkPlug.Persistence.EntityFramework.Context;

public interface IModelBuilderProvider
{
    public void Build(ModelBuilder modelBuilder);
}
