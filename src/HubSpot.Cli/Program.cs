using System.CommandLine;
using DamianH.HubSpot.Cli;
using DamianH.HubSpot.Cli.Commands;

var rootCommand = new RootCommand("HubSpot CLI — interact with the HubSpot API from the command line");

foreach (var option in GlobalOptions.All)
{
    option.Recursive = true;
    rootCommand.Add(option);
}

rootCommand.Add(CrmCommand.Create());
rootCommand.Add(MarketingCommand.Create());
rootCommand.Add(CmsCommand.Create());
rootCommand.Add(FilesCommand.Create());
rootCommand.Add(WebhooksCommand.Create());
rootCommand.Add(ConfigCommand.Create());

var parseResult = rootCommand.Parse(args);
return await parseResult.InvokeAsync();
