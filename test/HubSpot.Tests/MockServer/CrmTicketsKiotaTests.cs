using DamianH.HubSpot.KiotaClient.CRM.Tickets.V3;
using DamianH.HubSpot.KiotaClient.CRM.Tickets.V3.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace DamianH.HubSpot.MockServer;

public class CrmTicketsKiotaTests : IAsyncLifetime
{
    private readonly ITestOutputHelper _outputHelper = TestContext.Current.TestOutputHelper!;
    private HubSpotMockServer _server = null!;
    private HubSpotCRMTicketsV3Client _client = null!;

    public async ValueTask InitializeAsync()
    {
        _server = await HubSpotMockServer.StartNew();
        _outputHelper.WriteLine(_server.Uri.ToString());

        var authenticationProvider = new AnonymousAuthenticationProvider();
        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider)
        {
            BaseUrl = _server.Uri.ToString()
        };
        _client = new HubSpotCRMTicketsV3Client(requestAdapter);
    }

    [Fact]
    public async Task Can_create_ticket()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "subject", "Website login issue" },
                    { "content", "User cannot log into the customer portal" },
                    { "hs_pipeline_stage", "1" },
                    { "hs_ticket_priority", "HIGH" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Tickets.PostAsync(input);
        var ticket = created!;

        ticket.ShouldNotBeNull();
        ticket.Id.ShouldNotBeNullOrEmpty();
        ticket.Properties.ShouldNotBeNull();
        ticket.Properties.AdditionalData.ShouldContainKeyAndValue("subject", "Website login issue");
        ticket.CreatedAt.ShouldNotBeNull();
    }

    [Fact]
    public async Task Can_get_ticket_by_id()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "subject", "Payment processing error" },
                    { "content", "Transaction failed during checkout" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Tickets.PostAsync(input);
        var ticketId = created!.Id!;

        var retrieved = await _client.Crm.V3.Objects.Tickets[ticketId].GetAsync();

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(ticketId);
    }

    [Fact]
    public async Task Can_update_ticket()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "subject", "Initial Support Request" },
                    { "hs_pipeline_stage", "1" },
                    { "hs_ticket_priority", "MEDIUM" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Tickets.PostAsync(input);
        var ticketId = created!.Id!;

        var updateInput = new SimplePublicObjectInput
        {
            Properties = new SimplePublicObjectInput_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "hs_pipeline_stage", "2" },
                    { "hs_ticket_priority", "LOW" }
                }
            }
        };

        var updated = await _client.Crm.V3.Objects.Tickets[ticketId].PatchAsync(updateInput);

        updated.ShouldNotBeNull();
        updated.Properties!.AdditionalData.ShouldContainKeyAndValue("hs_pipeline_stage", "2");
        updated.Properties.AdditionalData.ShouldContainKeyAndValue("hs_ticket_priority", "LOW");
    }

    [Fact]
    public async Task Can_delete_ticket()
    {
        var input = new SimplePublicObjectInputForCreate
        {
            Properties = new SimplePublicObjectInputForCreate_properties
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { "subject", "Ticket to Delete" },
                    { "content", "This ticket will be removed" }
                }
            }
        };

        var created = await _client.Crm.V3.Objects.Tickets.PostAsync(input);
        var ticketId = created!.Id!;

        await _client.Crm.V3.Objects.Tickets[ticketId].DeleteAsync();

        var exception = await Should.ThrowAsync<Exception>(async () =>
        {
            await _client.Crm.V3.Objects.Tickets[ticketId].GetAsync();
        });

        _outputHelper.WriteLine($"Expected exception: {exception.Message}");
    }

    [Fact]
    public async Task Can_list_tickets()
    {
        for (int i = 1; i <= 3; i++)
        {
            var input = new SimplePublicObjectInputForCreate
            {
                Properties = new SimplePublicObjectInputForCreate_properties
                {
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "subject", $"Support Ticket {i}" },
                        { "content", $"Issue description {i}" }
                    }
                }
            };
            await _client.Crm.V3.Objects.Tickets.PostAsync(input);
        }

        var response = await _client.Crm.V3.Objects.Tickets.GetAsync(config =>
        {
            config.QueryParameters.Limit = 10;
        });

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task Can_batch_create_tickets()
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
                            { "subject", "Batch Ticket 1" },
                            { "content", "First batch issue" }
                        }
                    }
                },
                new SimplePublicObjectBatchInputForCreate
                {
                    Properties = new SimplePublicObjectBatchInputForCreate_properties
                    {
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "subject", "Batch Ticket 2" },
                            { "content", "Second batch issue" }
                        }
                    }
                }
            ]
        };

        var response = await _client.Crm.V3.Objects.Tickets.Batch.Create.PostAsync(batchInput);

        response.ShouldNotBeNull();
        response.Results.ShouldNotBeNull();
        response.Results.Count.ShouldBe(2);
    }

    public async ValueTask DisposeAsync()
    {
        await _server.DisposeAsync();
    }
}
