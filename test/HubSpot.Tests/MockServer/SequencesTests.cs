using DamianH.HubSpot.KiotaClient.Automation.Sequences.V4;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using SequenceModels = DamianH.HubSpot.KiotaClient.Automation.Sequences.V4.Models;

namespace DamianH.HubSpot.MockServer;

public class SequencesTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotAutomationSequencesV4Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };

        _client = new HubSpotAutomationSequencesV4Client(requestAdapter);
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();

    [Fact]
    public async Task EnrollContactInSequence_ReturnsEnrollmentDetails()
    {
        var enrollmentRequest = new SequenceModels.PublicSequenceEnrollmentRequest
        {
            ContactId = "12345",
            SequenceId = "seq-test-1"
        };

        var enrollment = await _client.Automation.V4.Sequences.Enrollments.PostAsync(enrollmentRequest);

        enrollment.ShouldNotBeNull();
        enrollment.Id.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetSequence_AfterEnrollment_ReturnsSequenceDetails()
    {
        await _client.Automation.V4.Sequences.Enrollments.PostAsync(
            new SequenceModels.PublicSequenceEnrollmentRequest
            {
                ContactId = "12345",
                SequenceId = "seq-test-1"
            });

        var sequence = await _client.Automation.V4.Sequences["seq-test-1"].GetAsync();

        sequence.ShouldNotBeNull();
        sequence.Id.ShouldBe("seq-test-1");
        sequence.Name.ShouldNotBeNullOrEmpty();
    }
}
