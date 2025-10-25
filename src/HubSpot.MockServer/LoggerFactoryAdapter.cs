using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DamianH.HubSpot.MockServer;

internal sealed class LoggerFactoryAdapter : ILoggerFactory
{
    public void AddProvider(ILoggerProvider provider) { }
    
    public ILogger CreateLogger(string categoryName) => NullLogger.Instance;
    
    public void Dispose() { }
}
