function GenerateClient($url, $area, $feature) {
    $namespaceName = "DamianH.HubSpot.KiotaClient.$area.$feature"
    $output = "./Generated/$area/$feature"
    $className = "HubSpot" + $area + $feature + "Client"
    Write-Host $output $namespace $className
    kiota generate --openapi $url --language csharp --output $output --class-name $className --namespace-name $namespaceName
}
if (Test-Path -LiteralPath "./Generated") {
    Remove-Item -LiteralPath ./Generated -Recurse
} 
GenerateClient "https://api.hubspot.com/api-catalog-public/v1/apis/webhooks/v3" "Webhooks" "Webhooks"
GenerateClient "https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/companies" "Crm" "Companies"
GenerateClient "https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/contacts" "Crm" "Contacts"
GenerateClient "https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/deals" "Crm" "Deals"
GenerateClient "https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects/line_items" "Crm" "LineItems"
GenerateClient "https://api.hubspot.com/api-catalog-public/v1/apis/crm/v3/objects" "Crm" "Objects"
GenerateClient "https://api.hubspot.com/api-catalog-public/v1/apis/marketing/v3/transactional" "Marketing" "Transactional"