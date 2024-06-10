using System.Diagnostics.CodeAnalysis;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AwsTool.Sdk.Sns.Extensions;

[ExcludeFromCodeCoverage]
public static class SnsSettingsExtensions
{
    public static IServiceCollection AddSns(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var awsSnsOptions = GetAwsLocalOptions(configuration);
            services.AddAWSService<IAmazonSimpleNotificationService>(awsSnsOptions);
        }
        else
        {
            services.AddScoped<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();
        }
        
        services.AddScoped<ISnsPublish, SnsPublish>();
        
        return services;
    }
    
    private static AWSOptions GetAwsLocalOptions(IConfiguration configuration)
    {
        var snsSettings = new SnsSettings();
        configuration.GetSection("Aws").GetSection("Sns").Bind(snsSettings);

        if (snsSettings == null)
            throw new InvalidOperationException("Aws.Sns not defined in configuration.");

        var awsOptions = new AWSOptions
        {
            Credentials = new BasicAWSCredentials(snsSettings.AccessKey, snsSettings.SecretKey)
        };

        if (!string.IsNullOrEmpty(snsSettings.ServiceUrl))
            awsOptions.DefaultClientConfig.ServiceURL = snsSettings.ServiceUrl;

        return awsOptions;
    }
}