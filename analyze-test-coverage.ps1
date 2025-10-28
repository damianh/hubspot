# Test Coverage Analysis Script
# Analyzes which API endpoints have test coverage

$generatedPath = "src\HubSpot.KiotaClient\Generated"
$testPath = "test\HubSpot.Tests\MockServer"

# Find all generated client files
$clientFiles = Get-ChildItem -Path $generatedPath -Recurse -Filter "*Client.cs" | 
    Where-Object { $_.Directory.Name -match "^V\d+" }

Write-Host "=== HubSpot API Test Coverage Analysis ===" -ForegroundColor Cyan
Write-Host ""

# Extract API names from client files
$apis = @{}
foreach ($file in $clientFiles) {
    $relativePath = $file.Directory.Parent.Parent.Name + "\" + $file.Directory.Parent.Name
    if (-not $apis.ContainsKey($relativePath)) {
        $apis[$relativePath] = @{
            ClientFile = $file.FullName
            Tested = $false
            TestFile = $null
        }
    }
}

# Check which APIs have tests
$testFiles = Get-ChildItem -Path $testPath -Filter "*Tests.cs"
foreach ($testFile in $testFiles) {
    $content = Get-Content $testFile.FullName -Raw
    
    # Look for using statements that reference the generated clients
    if ($content -match 'using DamianH\.HubSpot\.KiotaClient\.([^;]+);') {
        foreach ($api in $apis.Keys) {
            if ($content -match [regex]::Escape($api)) {
                $apis[$api].Tested = $true
                if ($null -eq $apis[$api].TestFile) {
                    $apis[$api].TestFile = @()
                }
                $apis[$api].TestFile += $testFile.Name
            }
        }
    }
}

# Report findings
$testedCount = ($apis.Values | Where-Object { $_.Tested }).Count
$totalCount = $apis.Count
$coveragePercent = [math]::Round(($testedCount / $totalCount) * 100, 1)

Write-Host "Total APIs: $totalCount" -ForegroundColor White
Write-Host "Tested APIs: $testedCount" -ForegroundColor Green
Write-Host "Untested APIs: $($totalCount - $testedCount)" -ForegroundColor Red
Write-Host "Coverage: $coveragePercent%" -ForegroundColor Yellow
Write-Host ""

# Show tested APIs
Write-Host "=== Tested APIs ===" -ForegroundColor Green
$apis.Keys | Sort-Object | Where-Object { $apis[$_].Tested } | ForEach-Object {
    Write-Host "✓ $_ " -ForegroundColor Green -NoNewline
    Write-Host "($($apis[$_].TestFile -join ', '))" -ForegroundColor Gray
}
Write-Host ""

# Show untested APIs (high priority ones first)
Write-Host "=== Untested APIs ===" -ForegroundColor Red
$untested = $apis.Keys | Sort-Object | Where-Object { -not $apis[$_].Tested }

# Categorize by priority
$highPriority = @("CRM", "CMS")
$mediumPriority = @("Marketing", "Conversations", "Automation")

Write-Host ""
Write-Host "HIGH PRIORITY (CRM, CMS):" -ForegroundColor Yellow
$untested | Where-Object { $_ -match "^(CRM|CMS)" } | ForEach-Object {
    Write-Host "  ✗ $_" -ForegroundColor Red
}

Write-Host ""
Write-Host "MEDIUM PRIORITY (Marketing, Conversations, Automation):" -ForegroundColor Yellow
$untested | Where-Object { $_ -match "^(Marketing|Conversations|Automation)" } | ForEach-Object {
    Write-Host "  ✗ $_" -ForegroundColor Red
}

Write-Host ""
Write-Host "LOWER PRIORITY (Other):" -ForegroundColor Yellow
$untested | Where-Object { $_ -notmatch "^(CRM|CMS|Marketing|Conversations|Automation)" } | ForEach-Object {
    Write-Host "  ✗ $_" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== Recommendations ===" -ForegroundColor Cyan
Write-Host "1. Add tests for high-priority untested APIs (CRM, CMS)"
Write-Host "2. Use CmsPagesTests.cs as a template for new test files"
Write-Host "3. Run coverage with: dotnet test --settings coverlet.runsettings --collect:`"XPlat Code Coverage`""
Write-Host ""
