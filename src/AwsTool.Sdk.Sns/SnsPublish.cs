using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace AwsTool.Sdk.Sns;

/// <summary>
/// Represents integration with the SNS topic.
/// </summary>
public class SnsPublish : ISnsPublish
{
    private readonly IAmazonSimpleNotificationService _snsClient;
    private readonly JsonSerializerOptions _jsonOption;

    /// <summary>
    /// Instantiates a <see cref="SnsPublish"/>.
    /// </summary>
    /// <param name="snsClient">Connection to the SNS.</param>
    public SnsPublish(IAmazonSimpleNotificationService snsClient)
    {
        _snsClient = snsClient;
        _jsonOption = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <summary>
    /// Posts a message to the specified topic.
    /// </summary>
    /// <param name="topicArn">Topic to be published.</param>
    /// <param name="message">Message to be published.</param>
    /// <param name="headers">Header publicado junto a mensagem.</param>
    /// <param name="cancellationToken">Token which can be used to cancel the task.</param>
    /// <returns>A Task that can be used to poll or wait for results, or both.</returns>
    public async Task ExecuteAsync(string topicArn, object message, Dictionary<string, string>? headers, CancellationToken cancellationToken)
    {
        var messageText = JsonSerializer.Serialize(message, _jsonOption);
        var request = new PublishRequest
        {
            TopicArn = topicArn, 
            Message = messageText,
            MessageAttributes = headers?.ToDictionary(
                header => header.Key,
                header => new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = header.Value
                })
        };

        await _snsClient.PublishAsync(request, cancellationToken);
    }
}

/// <summary>
/// Represents integration with the SNS topic.
/// </summary>
public interface ISnsPublish
{
    /// <summary>
    /// Posts a message to the specified topic.
    /// </summary>
    /// <param name="topicArn">Topic to be published.</param>
    /// <param name="message">Message to be published.</param>
    /// <param name="headers">Header publicado junto a mensagem.</param>
    /// <param name="cancellationToken">Token which can be used to cancel the task.</param>
    /// <returns>A Task that can be used to poll or wait for results, or both.</returns>
    Task ExecuteAsync(string topicArn, object message, Dictionary<string, string>? headers, CancellationToken cancellationToken);
}