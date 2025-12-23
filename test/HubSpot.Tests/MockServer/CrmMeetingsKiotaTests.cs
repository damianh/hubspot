using DamianH.HubSpot.KiotaClient.CRM.Meetings.V3;
using DamianH.HubSpot.KiotaClient.CRM.Meetings.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmMeetingsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMMeetingsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMMeetingsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_meeting()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_meeting_title", "Sales Meeting" },
                    { "hs_meeting_body", "Discuss Q4 targets" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Meetings.PostAsync(input);
        var meeting = created!.Entity!;

        meeting.ShouldNotBeNull();
        meeting.Id.ShouldNotBeNullOrEmpty();
        meeting.Properties.ShouldNotBeNull();
        meeting.Properties.AdditionalData.ShouldContainKeyAndValue("hs_meeting_title", "Sales Meeting");
        meeting.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_meeting_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_meeting_title", "Get Test Meeting" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Meetings.PostAsync(input);
        var meetingId = created!.Entity!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Meetings[meetingId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(meetingId);
    }

    [Fact]
    public async Task Can_update_meeting()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_meeting_title", "Original Title" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Meetings.PostAsync(input);
        var meetingId = created!.Entity!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_meeting_title", "Updated Title" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Meetings[meetingId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_meeting_title", "Updated Title");
    }

    [Fact]
    public async Task Can_delete_meeting()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                    { "hs_meeting_title", "Meeting to Delete" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Meetings.PostAsync(input);
        var meetingId = created!.Entity!.Id!;

        await _client.Crm.V3.Objects.Meetings[meetingId].DeleteAsync();

        var exception = await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Meetings[meetingId].GetAsync();
        });

        _outputHelper.WriteLine($"Expected exception: {exception.Message}");
    }

    [Fact]
    public async Task Can_list_meetings()
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
                        { "hs_meeting_title", $"Meeting {i}" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Meetings.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Meetings.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
