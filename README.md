# aws-sdk-toolkit-sns
Responsible for integrating with SNS, enabling publication of messages in topics.

To use the library you will need to initialize it by following the steps below:

* In your application initialization file, the SNS access configuration must be referenced:

```csharp
builder.Services
    .AddAwsDefaultSettings(builder.Configuration, builder.Environment)
    .AddSns(builder.Configuration, builder.Environment);
```

* The SDK contains a facilitator to operate locally, or even run the application locally and using the dynamo resource on AWS. To do this, simply add the configuration below in the **appsettings.[environment].json** file.

```json5
"Aws": {
    "Sns": {
        "AccessKey": "local",
        "SecretKey": "local",
        "ServiceUrl": "http://localhost:4566"
    }
}
```
