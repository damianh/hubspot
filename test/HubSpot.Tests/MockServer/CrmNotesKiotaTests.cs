using DamianH.HubSpot.KiotaClient.CRM.Notes.V3;
using DamianH.HubSpot.KiotaClient.CRM.Notes.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmNotesKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMNotesV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMNotesV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_note()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_note_body", "This is a test note about the customer interaction." }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Notes.PostAsync(input);
        var note = created!;

        note.ShouldNotBeNull();
        note.Id.ShouldNotBeNullOrEmpty();
        note.Properties.ShouldNotBeNull();
        note.Properties.AdditionalData.ShouldContainKeyAndValue("hs_note_body", "This is a test note about the customer interaction.");
        note.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_note_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_note_body", "Note to retrieve" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Notes.PostAsync(input);
        var noteId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Notes[noteId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(noteId);
    }

    [Fact]
    public async Task Can_update_note()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_note_body", "Original note content" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Notes.PostAsync(input);
        var noteId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_note_body", "Updated note content" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Notes[noteId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_note_body", "Updated note content");
    }

    [Fact]
    public async Task Can_delete_note()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_note_body", "Note to delete" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Notes.PostAsync(input);
        var noteId = created!.Id!;

        await _client.Crm.V3.Objects.Notes[noteId].DeleteAsync();

        var exception = await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Notes[noteId].GetAsync();
        });

        _outputHelper.WriteLine($"Expected exception: {exception.Message}");
    }

    [Fact]
    public async Task Can_list_notes()
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
                        { "hs_note_body", $"Note {i}" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Notes.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Notes.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_notes()
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
                            { "hs_note_body", "Batch Note 1" }
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
                            { "hs_note_body", "Batch Note 2" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Notes.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
