using System.Collections.Concurrent;
using System.Text.Json;

namespace DamianH.HubSpot;

public sealed class GeneratorTests
{
    private const string SpecUrl = "https://api.hubspot.com/public/api/spec/v2/specs/release/75232/version/2026-03";

    [Fact]
    public void GetEntries_IncludesAllVersionsAndGroups()
    {
        var catalog = JsonSerializer.Deserialize<HubSpotApiCatalog>("""
            {"results":[
              {"group":"CRM","name":"Contacts","versions":[
                {"version":"3","openApi":"https://api.hubspot.com/public/api/spec/v2/specs/release/1/version/3"},
                {"version":"2026-09","openApi":"https://api.hubspot.com/public/api/spec/v2/specs/release/2/version/2026-09"}]},
              {"group":"Test","name":"At Debug","versions":[
                {"version":"2027-03-beta","stage":"DEVELOPER_PREVIEW","introduction":null,
                 "openApi":"https://api.hubspot.com/public/api/spec/v2/specs/release/3/version/2027-03-beta"}]}
            ]}
            """)!;

        var entries = Generator.GetEntries(catalog);

        entries.Select(entry => entry.RelativePath).ShouldBe(new[]
        {
            Path.Join("CRM", "Contacts", "V202609"),
            Path.Join("CRM", "Contacts", "V3"),
            Path.Join("Test", "AtDebug", "V202703beta")
        });
        entries[2].ClassName.ShouldBe("HubSpotTestAtDebugV202703betaClient");
        entries[2].NamespaceName.ShouldBe("DamianH.HubSpot.KiotaClient.Test.AtDebug.V202703beta");
        entries[2].OpenApi.ShouldBe("https://api.hubspot.com/public/api/spec/v2/specs/release/3/version/2027-03-beta");
    }

    [Fact]
    public void GetEntries_RejectsEmptyCatalogAndMissingVersions()
    {
        Should.Throw<InvalidDataException>(() => Generator.GetEntries(new()));
        Should.Throw<InvalidDataException>(() => Generator.GetEntries(new() { Results = [] }));
        Should.Throw<InvalidDataException>(() => Generator.GetEntries(new()
        {
            Results = [new() { Group = "CRM", Name = "Contacts", Versions = [] }]
        }));
    }

    [Theory]
    [InlineData("Traversal", "../CRM", "Contacts", "3", SpecUrl)]
    [InlineData("InvalidName", "CRM", "Contacts/Other", "3", SpecUrl)]
    [InlineData("InvalidVersion", "CRM", "Contacts", "../3", SpecUrl)]
    [InlineData("InsecureUrl", "CRM", "Contacts", "3", "http://api.hubspot.com/public/api/spec/1")]
    [InlineData("ExternalUrl", "CRM", "Contacts", "3", "https://example.com/public/api/spec/1")]
    [InlineData("WrongEndpoint", "CRM", "Contacts", "3", "https://api.hubspot.com/crm/v3/objects")]
    public void GetEntries_RejectsInvalidInputs(string caseName, string group, string name, string version, string url)
    {
        Should.Throw<InvalidDataException>(() => Generator.GetEntries(Catalog(group, name, version, url)));
    }

    [Fact]
    public void GetEntries_RejectsSanitizedIdentityCollisions()
    {
        var catalog = Catalog("CRM", "Some-Api", "3", SpecUrl);
        catalog.Results!.Add(new() { Group = "CRM", Name = "SomeApi", Versions = [new() { Version = "3", OpenApi = SpecUrl }] });

        Should.Throw<InvalidDataException>(() => Generator.GetEntries(catalog))
            .Message.ShouldContain("Duplicate generated client identity");
    }

    [Fact]
    public void FindRepositoryRoot_ResolvesFromExecutableNotWorkingDirectory()
    {
        using var workspace = new Workspace();
        var project = Path.Join(workspace.Root, "tool", "Generate");
        Directory.CreateDirectory(project);
        File.WriteAllText(Path.Join(project, "Generate.csproj"), "<Project />");
        File.WriteAllText(Path.Join(workspace.Project, "HubSpot.KiotaClient.csproj"), "<Project />");
        var executable = Path.Join(project, "bin", "Debug", "net10.0");
        Directory.CreateDirectory(executable);

        Generator.FindRepositoryRoot(executable).ShouldBe(workspace.Root);
        Should.Throw<DirectoryNotFoundException>(() => Generator.FindRepositoryRoot(Path.GetPathRoot(workspace.Root)!));
    }

    [Fact]
    public async Task GenerateAsync_SuccessReplacesUnlistedClientsAndWritesExactInventory()
    {
        using var workspace = new Workspace();
        var entries = Generator.GetEntries(Catalog("CRM", "Contacts", "2026-03", SpecUrl));
        File.WriteAllText(Path.Join(workspace.Project, "generated-report-failed.txt"), "previous failure");
        var exitCode = await Generator.GenerateAsync(workspace.Root, entries, WriteLock);

        exitCode.ShouldBe(0);
        File.Exists(workspace.OldClient).ShouldBeFalse();
        Directory.GetFiles(workspace.Output, "kiota-lock.json", SearchOption.AllDirectories)
            .Select(path => Path.GetRelativePath(workspace.Output, path))
            .ShouldBe([Path.Join("CRM", "Contacts", "V202603", "kiota-lock.json")]);
        var report = await File.ReadAllTextAsync(Path.Join(workspace.Output, "generated-report.txt"));
        report.ShouldContain("Expected clients: 1");
        report.ShouldContain($"CRM{Path.DirectorySeparatorChar}Contacts{Path.DirectorySeparatorChar}V202603: {SpecUrl}");
        report.ShouldEndWith($"Failed:{Environment.NewLine}");
        File.Exists(Path.Join(workspace.Project, "generated-report-failed.txt")).ShouldBeFalse();
        workspace.TemporaryDirectories.ShouldBeEmpty();
    }

    [Fact]
    public async Task GenerateAsync_PartialFailureDoesNotPromoteSuccessfulClients()
    {
        using var workspace = new Workspace();
        var catalog = Catalog("CRM", "Contacts", "2026-03", SpecUrl);
        catalog.Results!.Add(new() { Group = "CRM", Name = "Deals", Versions = [new() { Version = "3", OpenApi = SpecUrl }] });
        var entries = Generator.GetEntries(catalog);

        var exitCode = await Generator.GenerateAsync(workspace.Root, entries, async (entry, output) =>
        {
            if (entry.RelativePath.Contains("Deals", StringComparison.Ordinal))
            {
                throw new IOException("Deals generation failed");
            }
            await WriteLock(entry, output);
        });

        exitCode.ShouldBe(1);
        Directory.GetFiles(workspace.Output, "*", SearchOption.AllDirectories).ShouldBe([workspace.OldClient]);
        (await File.ReadAllTextAsync(workspace.OldClient)).ShouldBe("existing client");
        var report = await File.ReadAllTextAsync(Path.Join(workspace.Project, "generated-report-failed.txt"));
        report.ShouldContain($"Succeeded:{Environment.NewLine}  {entries[0].RelativePath}: {SpecUrl}");
        report.ShouldContain($"Failed:{Environment.NewLine}  {entries[1].RelativePath}: {SpecUrl}");
        workspace.TemporaryDirectories.ShouldBeEmpty();
    }

    [Fact]
    public async Task GenerateAsync_EmptyInventoryIsRejectedBeforeChangingOutput()
    {
        using var workspace = new Workspace();
        await Should.ThrowAsync<InvalidDataException>(() => Generator.GenerateAsync(workspace.Root, [], WriteLock));

        (await File.ReadAllTextAsync(workspace.OldClient)).ShouldBe("existing client");
        workspace.TemporaryDirectories.ShouldBeEmpty();
    }

    [Fact]
    public async Task GenerateAsync_FailurePreservesExistingOutputAndReportsEachFailure()
    {
        using var workspace = new Workspace();
        var catalog = Catalog("CRM", "Contacts", "2026-03", SpecUrl);
        catalog.Results!.Add(new() { Group = "CRM", Name = "Deals", Versions = [new() { Version = "3", OpenApi = SpecUrl }] });
        var entries = Generator.GetEntries(catalog);
        var attempted = new ConcurrentBag<string>();

        var exitCode = await Generator.GenerateAsync(workspace.Root, entries, (entry, _) =>
        {
            attempted.Add(entry.RelativePath);
            throw new IOException("simulated generation failure");
        });

        exitCode.ShouldBe(1);
        attempted.Order(StringComparer.Ordinal).ShouldBe(entries.Select(entry => entry.RelativePath));
        (await File.ReadAllTextAsync(workspace.OldClient)).ShouldBe("existing client");
        var report = await File.ReadAllTextAsync(Path.Join(workspace.Project, "generated-report-failed.txt"));
        foreach (var entry in entries)
        {
            report.ShouldContain(entry.RelativePath);
        }
        report.ShouldContain("simulated generation failure");
        workspace.TemporaryDirectories.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("MissingLock")]
    [InlineData("WrongUrl")]
    [InlineData("MalformedLock")]
    [InlineData("WrongKiotaVersion")]
    public async Task GenerateAsync_InvalidLockFailsWithoutReplacingOutput(string caseName)
    {
        using var workspace = new Workspace();
        var entries = Generator.GetEntries(Catalog("CRM", "Contacts", "2026-03", SpecUrl));
        var exitCode = await Generator.GenerateAsync(workspace.Root, entries, async (entry, output) =>
        {
            Directory.CreateDirectory(output);
            if (caseName != "MissingLock")
            {
                await File.WriteAllTextAsync(Path.Join(output, "kiota-lock.json"), caseName == "MalformedLock" ? "{" :
                    JsonSerializer.Serialize(new
                    {
                        descriptionLocation = caseName == "WrongUrl" ? "https://example.com/wrong" : entry.OpenApi,
                        kiotaVersion = caseName == "WrongKiotaVersion" ? "1.29.0" : "1.38.0",
                        clientClassName = entry.ClassName,
                        clientNamespaceName = entry.NamespaceName
                    }));
            }
        });

        exitCode.ShouldBe(1);
        (await File.ReadAllTextAsync(workspace.OldClient)).ShouldBe("existing client");
        (await File.ReadAllTextAsync(Path.Join(workspace.Project, "generated-report-failed.txt")))
            .ShouldContain(entries[0].RelativePath);
    }

    [Fact]
    public async Task GenerateAsync_ExtraClientFailsInventoryVerification()
    {
        using var workspace = new Workspace();
        var entries = Generator.GetEntries(Catalog("CRM", "Contacts", "2026-03", SpecUrl));

        await Should.ThrowAsync<InvalidDataException>(() => Generator.GenerateAsync(workspace.Root, entries, async (entry, output) =>
        {
            await WriteLock(entry, output);
            await WriteLock(entry, Path.Join(output, "Extra"));
        }));

        (await File.ReadAllTextAsync(workspace.OldClient)).ShouldBe("existing client");
        workspace.TemporaryDirectories.ShouldBeEmpty();
    }

    private static HubSpotApiCatalog Catalog(string group, string name, string version, string url) => new()
    {
        Results = [new() { Group = group, Name = name, Versions = [new() { Version = version, OpenApi = url }] }]
    };

    private static async Task WriteLock(GenerationEntry entry, string output)
    {
        Directory.CreateDirectory(output);
        await File.WriteAllTextAsync(Path.Join(output, "kiota-lock.json"),
            JsonSerializer.Serialize(new
            {
                descriptionLocation = entry.OpenApi,
                kiotaVersion = "1.38.0",
                clientClassName = entry.ClassName,
                clientNamespaceName = entry.NamespaceName
            }));
    }

    private sealed class Workspace : IDisposable
    {
        internal string Root { get; } = Path.Join(Path.GetTempPath(), $"hubspot-generator-test-{Guid.NewGuid():N}");
        internal string Project => Path.Join(Root, "src", "HubSpot.KiotaClient");
        internal string Output => Path.Join(Project, "Generated");
        internal string OldClient => Path.Join(Output, "old-client.txt");
        internal IEnumerable<string> TemporaryDirectories => Directory.GetDirectories(Project)
            .Where(path => Path.GetFileName(path).StartsWith("Generated.", StringComparison.Ordinal));

        internal Workspace()
        {
            Directory.CreateDirectory(Output);
            File.WriteAllText(OldClient, "existing client");
            Directory.CreateDirectory(Path.Join(Root, ".config"));
            File.WriteAllText(Path.Join(Root, ".config", "dotnet-tools.json"),
                """{"tools":{"microsoft.openapi.kiota":{"version":"1.38.0"}}}""");
        }

        public void Dispose() => Directory.Delete(Root, true);
    }
}
