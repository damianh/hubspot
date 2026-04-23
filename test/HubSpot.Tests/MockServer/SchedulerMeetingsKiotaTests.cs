using DamianH.HubSpot.KiotaClient.Scheduler.Meetings.V3;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class SchedulerMeetingsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotSchedulerMeetingsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotSchedulerMeetingsV3Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task Can_list_meeting_links()
    {
        var result = await _client.Scheduler.V3.Meetings.MeetingLinks.GetAsync();

        result.ShouldNotBeNull();
        result.Results.ShouldNotBeNull();
    }
}
