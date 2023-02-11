namespace SparkPlug.Api.Configuration;

public class SparkPlugApiOptions
{
    public const string ConfigPath = "SparkPlug:Api";
    public SparkPlugApiOptions()
    {
        ApplicationName = string.Empty;
        IsMultiTenant = false;
    }

    [Required]
    public string ApplicationName { get; set; }
    public bool IsMultiTenant { get; set; }
}
