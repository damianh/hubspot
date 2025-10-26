using DamianH.HubSpot.KiotaClient.CRM.Calls.V3;
using DamianH.HubSpot.KiotaClient.CRM.Emails.V3;
using DamianH.HubSpot.KiotaClient.CRM.Meetings.V3;
using DamianH.HubSpot.KiotaClient.CRM.Notes.V3;
using DamianH.HubSpot.KiotaClient.CRM.Tasks.V3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using CallsModels = DamianH.HubSpot.KiotaClient.CRM.Calls.V3.Models;
using EmailsModels = DamianH.HubSpot.KiotaClient.CRM.Emails.V3.Models;
using MeetingsModels = DamianH.HubSpot.KiotaClient.CRM.Meetings.V3.Models;
using NotesModels = DamianH.HubSpot.KiotaClient.CRM.Notes.V3.Models;
using TasksModels = DamianH.HubSpot.KiotaClient.CRM.Tasks.V3.Models;

namespace DamianH.HubSpot.MockServer;

public class CrmStandardObjectsTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;
    private HubSpotCRMCallsV3Client _callsClient = null!;
    private HubSpotCRMEmailsV3Client _emailsClient = null!;
    private HubSpotCRMMeetingsV3Client _meetingsClient = null!;
    private HubSpotCRMNotesV3Client _notesClient = null!;
    private HubSpotCRMTasksV3Client _tasksClient = null!;

    public async ValueTask InitializeAsync()
    {
        var services = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        _server = await HubSpotMockServer.StartNew(loggerFactory);
        var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider())
        {
            BaseUrl = _server.Uri.ToString()
        };

        _callsClient = new HubSpotCRMCallsV3Client(requestAdapter);
        _emailsClient = new HubSpotCRMEmailsV3Client(requestAdapter);
        _meetingsClient = new HubSpotCRMMeetingsV3Client(requestAdapter);
        _notesClient = new HubSpotCRMNotesV3Client(requestAdapter);
        _tasksClient = new HubSpotCRMTasksV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_and_get_call()
    {
        var createRequest = new CallsModels.SimplePublicObjectInputForCreate
        {
            Properties = new CallsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_call_title"] = "Test Call",
                    ["hs_call_body"] = "Discussion about project"
                }
            }
        };

        var created = await _callsClient.Crm.V3.Objects.Calls.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var callId = created!.Entity!.Id;
        callId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _callsClient.Crm.V3.Objects.Calls[callId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["hs_call_title", "hs_call_body"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(callId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_call_title", "Test Call");
        retrieved.Properties.AdditionalData.ShouldContainKeyAndValue("hs_call_body", "Discussion about project");
    }

    [Fact]
    public async Task Can_create_and_get_email()
    {
        var createRequest = new EmailsModels.SimplePublicObjectInputForCreate
        {
            Properties = new EmailsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_email_subject"] = "Test Email",
                    ["hs_email_text"] = "Email body content"
                }
            }
        };

        var created = await _emailsClient.Crm.V3.Objects.Emails.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var emailId = created!.Entity!.Id;
        emailId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _emailsClient.Crm.V3.Objects.Emails[emailId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["hs_email_subject"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(emailId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_email_subject", "Test Email");
    }

    [Fact]
    public async Task Can_create_and_get_meeting()
    {
        var createRequest = new MeetingsModels.SimplePublicObjectInputForCreate
        {
            Properties = new MeetingsModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_meeting_title"] = "Test Meeting",
                    ["hs_meeting_body"] = "Meeting notes"
                }
            }
        };

        var created = await _meetingsClient.Crm.V3.Objects.Meetings.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var meetingId = created!.Entity!.Id;
        meetingId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _meetingsClient.Crm.V3.Objects.Meetings[meetingId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["hs_meeting_title"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(meetingId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_meeting_title", "Test Meeting");
    }

    [Fact]
    public async Task Can_create_and_get_note()
    {
        var createRequest = new NotesModels.SimplePublicObjectInputForCreate
        {
            Properties = new NotesModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_note_body"] = "This is a test note"
                }
            }
        };

        var created = await _notesClient.Crm.V3.Objects.Notes.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var noteId = created!.Entity!.Id;
        noteId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _notesClient.Crm.V3.Objects.Notes[noteId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["hs_note_body"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(noteId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_note_body", "This is a test note");
    }

    [Fact]
    public async Task Can_create_and_get_task()
    {
        var createRequest = new TasksModels.SimplePublicObjectInputForCreate
        {
            Properties = new TasksModels.SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    ["hs_task_subject"] = "Test Task",
                    ["hs_task_body"] = "Task description"
                }
            }
        };

        var created = await _tasksClient.Crm.V3.Objects.Tasks.PostAsync(createRequest);
        created.ShouldNotBeNull();
        var taskId = created!.Entity!.Id;
        taskId.ShouldNotBeNullOrWhiteSpace();

        var retrieved = await _tasksClient.Crm.V3.Objects.Tasks[taskId].GetAsync(rc =>
        {
            rc.QueryParameters.Properties = ["hs_task_subject"];
        });
        retrieved.ShouldNotBeNull();
        retrieved!.Id.ShouldBe(taskId);
        retrieved.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_task_subject", "Test Task");
    }

    public async ValueTask DisposeAsync() => await _server.DisposeAsync();
}
