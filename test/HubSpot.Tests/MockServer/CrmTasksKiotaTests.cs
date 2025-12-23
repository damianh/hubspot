using DamianH.HubSpot.KiotaClient.CRM.Tasks.V3;
using DamianH.HubSpot.KiotaClient.CRM.Tasks.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmTasksKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMTasksV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMTasksV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_task()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_task_subject", "Follow up with lead" },
                    { "hs_task_body", "Call the prospect to discuss their needs" },
                    { "hs_task_status", "NOT_STARTED" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Tasks.PostAsync(input);
        var task = created!.Entity!;

        task.ShouldNotBeNull();
        task.Id.ShouldNotBeNullOrEmpty();
        task.Properties.ShouldNotBeNull();
        task.Properties.AdditionalData.ShouldContainKeyAndValue("hs_task_subject", "Follow up with lead");
        task.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_task_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_task_subject", "Task to retrieve" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Tasks.PostAsync(input);
        var taskId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Tasks[taskId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(taskId);
    }

    [Fact]
    public async Task Can_update_task()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_task_subject", "Original Task" },
                    { "hs_task_status", "NOT_STARTED" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Tasks.PostAsync(input);
        var taskId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_task_subject", "Updated Task" },
                    { "hs_task_status", "COMPLETED" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Tasks[taskId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_task_subject", "Updated Task");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("hs_task_status", "COMPLETED");
    }

    [Fact]
    public async Task Can_list_tasks()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                        { "hs_task_subject", $"Task {i}" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Tasks.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Tasks.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_tasks()
    {
        var batchInput = new BatchInputSimplePublicObjectBatchInputForCreate
        {
            Inputs =
            [
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                            { "hs_task_subject", "Batch Task 1" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                            { "hs_task_subject", "Batch Task 2" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Tasks.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
