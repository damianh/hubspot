$ErrorActionPreference = "Stop"
dotnet run --project (Join-Path $PSScriptRoot "Generate")
if ($LASTEXITCODE -ne 0) {
    throw "HubSpot client generation failed with exit code $LASTEXITCODE."
}
