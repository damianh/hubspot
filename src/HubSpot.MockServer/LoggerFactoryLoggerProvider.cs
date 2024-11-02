using Microsoft.Extensions.Logging;

namespace DamianH.HubSpot.MockServer;

internal sealed class LoggerFactoryLoggerProvider(ILoggerFactory loggerFactory) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => loggerFactory.CreateLogger(categoryName);

    public void Dispose()
    { }
}