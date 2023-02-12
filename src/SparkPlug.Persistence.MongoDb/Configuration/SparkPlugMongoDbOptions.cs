namespace SparkPlug.Persistence.MongoDb.Configuration;

public class SparkPlugMongoDbOptions
{
    public const string ConfigPath = "SparkPlug:MongoDb";
    public SparkPlugMongoDbOptions()
    {
        ConnectionString = string.Empty;
    }
    [Required]
    public string ConnectionString { get; set; }
}
