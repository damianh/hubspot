using System.Threading.Channels;

namespace DamianH.HubSpot.MockServer.Webhooks;

internal sealed class WebhookEventChannel
{
    private readonly Channel<WebhookEvent> _channel = Channel.CreateUnbounded<WebhookEvent>(
        new UnboundedChannelOptions { SingleReader = true });

    public ChannelWriter<WebhookEvent> Writer => _channel.Writer;

    public ChannelReader<WebhookEvent> Reader => _channel.Reader;
}
