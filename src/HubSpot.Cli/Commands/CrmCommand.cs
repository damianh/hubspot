using System.CommandLine;
using DamianH.HubSpot.Cli.Commands.Crm;

namespace DamianH.HubSpot.Cli.Commands;

internal static class CrmCommand
{
    private static readonly string[] ObjectTypes =
    [
        "companies", "contacts", "deals", "tickets", "line_items",
        "products", "quotes", "calls", "emails", "meetings",
        "notes", "tasks", "postal_mail", "communications",
        "leads", "invoices", "orders", "carts", "fees", "discounts",
        "contracts", "appointments", "feedback_submissions", "goal_targets",
    ];

    public static Command Create()
    {
        var command = new Command("crm", "CRM operations");

        foreach (var objectType in ObjectTypes)
        {
            command.Add(CrmObjectCommand.Create(objectType));
        }

        // Phase 3 specialized subcommands
        command.Add(PropertiesCommand.Create());
        command.Add(PipelinesCommand.Create());
        command.Add(OwnersCommand.Create());
        command.Add(AssociationsCommand.Create());
        command.Add(SchemasCommand.Create());

        return command;
    }
}
