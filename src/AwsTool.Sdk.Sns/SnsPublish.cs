using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace AwsTool.Sdk.Sns;

public class SnsPublish : ISnsPublish
{
    private readonly IAmazonSimpleNotificationService _snsClient;
    private readonly JsonSerializerOptions _jsonOption;

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

    public async Task ExecuteAsync(string topicArn, object message, CancellationToken cancellationToken)
    {
        var messageText = JsonSerializer.Serialize(message, _jsonOption);
        var request = new PublishRequest
        {
            TopicArn = topicArn, 
            Message = messageText
        };

        await _snsClient.PublishAsync(request, cancellationToken);
    }
}


public interface ISnsPublish
{
    Task ExecuteAsync(string topicArn, object message, CancellationToken cancellationToken);
}