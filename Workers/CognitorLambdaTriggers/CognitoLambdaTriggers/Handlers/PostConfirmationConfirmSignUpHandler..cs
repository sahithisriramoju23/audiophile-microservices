using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;
using CognitoLambdaTriggers.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CognitoLambdaTriggers.Handlers;

internal class PostConfirmationConfirmSignUpHandler : CognitoTriggerHandler<PostConfirmationEvent>
{
    public const string TRIGGER_SOURCE = "PostConfirmation_ConfirmSignUp";

    public override string TriggerSource => TRIGGER_SOURCE;

    private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();

    public PostConfirmationConfirmSignUpHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public async override Task<JsonElement> HandleTriggerEventAsync()
    {
        var userId = TriggerEvent.UserName;
        var userEmail = TriggerEvent.Request.UserAttributes["email"];
        var givenName = TriggerEvent.Request.UserAttributes["given_name"];
        var familyName = TriggerEvent.Request.UserAttributes["family_name"];

        var user = new User(userId, userEmail, givenName, familyName);

        await StoreUser(user);

        return await base.HandleTriggerEventAsync();
    }

    private async Task StoreUser(User user)
    {
        try
        {
            DynamoDBContext context = new DynamoDBContext(client);
            await context.SaveAsync(user);
        }
        catch (AmazonDynamoDBException e) { Logger.LogError(e.Message); }
        catch (AmazonServiceException e) { Logger.LogError(e.Message); }
        catch (Exception e) { Logger.LogError(e.Message); }
    }
}