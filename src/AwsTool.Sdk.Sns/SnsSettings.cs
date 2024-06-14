namespace AwsTool.Sdk.Sns;

/// <summary>
/// Represents the settings for connecting to topics on SNS.
/// </summary>
public class SnsSettings
{
    /// <summary>
    /// Access key.
    /// </summary>
    public string AccessKey { get; set; }
    /// <summary>
    /// Secret key.
    /// </summary>
    public string SecretKey { get; set; }
    /// <summary>
    /// URL to access a local SNS service.
    /// </summary>
    public string ServiceUrl { get; set; }
}