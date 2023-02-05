namespace SparkPlug.Api.Configuration;

public class SparkPlugApiOptions
{
    public const string ConfigPath = "SparkPlug:Api";
    public SparkPlugApiOptions()
    {
        ApplicationName = string.Empty;
        IdentityProvider = new IdentityProvider();
        ExternalIdentityProvider = Array.Empty<ExternalIdentityProvider>();
    }

    [Required]
    public string ApplicationName { get; set; }
    public IdentityProvider IdentityProvider { get; set; }
    public ExternalIdentityProvider[] ExternalIdentityProvider { get; set; }
}

public class IdentityProvider
{
    public IdentityProvider()
    {
        Authority = string.Empty;
        ClientId = string.Empty;
        ClientSecret = string.Empty;
        Scopes = Array.Empty<string>();
        TokenCacheKey = string.Empty;
    }
    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string[] Scopes { get; set; }
    public string TokenCacheKey { get; set; }
}

public class ExternalIdentityProvider
{
    public ExternalIdentityProvider()
    {
        AuthProvider = string.Empty;
        ClientId = string.Empty;
        ClientSecret = string.Empty;
    }
    public string AuthProvider { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
