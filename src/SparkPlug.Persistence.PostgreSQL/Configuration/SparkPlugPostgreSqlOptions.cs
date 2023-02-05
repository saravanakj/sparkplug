namespace SparkPlug.Persistence.PostgreSQL.Configuration;

public class SparkPlugPostgreSqlOptions
{
    public const string ConfigPath = "SparkPlug:PostgreSqlDb";
    public SparkPlugPostgreSqlOptions()
    {
        ConnectionString = string.Empty;
    }
    [Required]
    public string ConnectionString { get; set; }
}
