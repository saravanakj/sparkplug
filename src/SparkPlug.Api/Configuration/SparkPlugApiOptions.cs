namespace SparkPlug.Api.Configuration;

public class SparkPlugApiOptions
{
    public const string ConfigPath = "SparkPlug:Api";
    public SparkPlugApiOptions()
    {
        ApplicationName = string.Empty;
        PathBase = "api";
    }

    [Required]
    public string ApplicationName { get; set; }
    public string PathBase { get; set; }
    public bool IsMultiTenant { get => PathBase.Contains($"{{{SparkPlugApiConstants.Tenant}}}"); }
}
