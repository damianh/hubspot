namespace DamianH.HubSpot.MockServer.Repositories.Import;

internal enum ImportState
{
    STARTED,
    PROCESSING,
    DONE,
    FAILED,
    CANCELED
}
