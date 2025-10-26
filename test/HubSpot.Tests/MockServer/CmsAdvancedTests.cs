namespace DamianH.HubSpot.MockServer;

public class CmsAdvancedTests : IAsyncLifetime
{
    private HubSpotMockServer _server = null!;

    public async ValueTask InitializeAsync() 
        => _server = await HubSpotMockServer.StartNew();

    public async ValueTask DisposeAsync() 
        => await _server.DisposeAsync();

    #region HubDB Tests

    [Fact]
    public async Task HubDb_CreateTable_ReturnsTable()
    {
        // Arrange - Create a table
        var createTableRequest = new
        {
            name = "test_table",
            label = "Test Table",
            columns = new[]
            {
                new { name = "name", label = "Name", type = "TEXT" },
                new { name = "email", label = "Email", type = "TEXT" }
            }
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createTableRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        
        // Act
        var response = await httpClient.PostAsync("/cms/v3/hubdb/tables", content);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("test_table");
        responseJson.ShouldContain("Test Table");
    }

    [Fact]
    public async Task HubDb_CreateRow_ReturnsRow()
    {
        // Arrange - Create a table first
        var createTableRequest = new
        {
            name = "contacts_table",
            label = "Contacts Table",
            columns = new[]
            {
                new { name = "name", label = "Name", type = "TEXT" },
                new { name = "email", label = "Email", type = "TEXT" }
            }
        };

        var tableJson = System.Text.Json.JsonSerializer.Serialize(createTableRequest);
        var tableContent = new StringContent(tableJson, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        var tableResponse = await httpClient.PostAsync("/cms/v3/hubdb/tables", tableContent);
        var tableResponseJson = await tableResponse.Content.ReadAsStringAsync();
        var tableDoc = System.Text.Json.JsonDocument.Parse(tableResponseJson);
        var tableId = tableDoc.RootElement.GetProperty("id").GetString();

        // Create a row
        var createRowRequest = new
        {
            values = new
            {
                name = "John Doe",
                email = "john@example.com"
            }
        };

        var rowJson = System.Text.Json.JsonSerializer.Serialize(createRowRequest);
        var rowContent = new StringContent(rowJson, System.Text.Encoding.UTF8, "application/json");
        
        // Act
        var response = await httpClient.PostAsync($"/cms/v3/hubdb/tables/{tableId}/rows", rowContent);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("John Doe");
        responseJson.ShouldContain("john@example.com");
    }

    [Fact]
    public async Task HubDb_GetTables_ReturnsTablesList()
    {
        // Arrange - Create a table
        var createTableRequest = new
        {
            name = "test_table",
            label = "Test Table"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createTableRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        await httpClient.PostAsync("/cms/v3/hubdb/tables", content);
        
        // Act
        var response = await httpClient.GetAsync("/cms/v3/hubdb/tables");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("test_table");
        responseJson.ShouldContain("results");
    }

    #endregion

    #region Source Code Tests

    [Fact]
    public async Task SourceCode_CreateFile_ReturnsFile()
    {
        // Arrange
        var createFileRequest = new
        {
            path = "/templates/page.html",
            content = "<html><body>Hello World</body></html>",
            type = "HTML"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createFileRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        
        // Act
        var response = await httpClient.PostAsync("/cms/v3/source-code/draft/content/templates/page.html", content);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("templates/page.html");
        responseJson.ShouldContain("Hello World");
    }

    [Fact]
    public async Task SourceCode_GetFile_ReturnsFile()
    {
        // Arrange - Create a file first
        var createFileRequest = new
        {
            path = "/templates/test.html",
            content = "<html><body>Test</body></html>",
            type = "HTML"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createFileRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        await httpClient.PostAsync("/cms/v3/source-code/draft/content/templates/test.html", content);
        
        // Act
        var response = await httpClient.GetAsync("/cms/v3/source-code/draft/content/templates/test.html");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("templates/test.html");
        responseJson.ShouldContain("Test");
    }

    [Fact]
    public async Task SourceCode_ListFiles_ReturnsFilesList()
    {
        // Arrange - Create a file
        var createFileRequest = new
        {
            path = "/templates/list.html",
            content = "<html><body>List</body></html>",
            type = "HTML"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createFileRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        await httpClient.PostAsync("/cms/v3/source-code/draft/content/templates/list.html", content);
        
        // Act
        var response = await httpClient.GetAsync("/cms/v3/source-code/draft/content");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("templates/list.html");
        responseJson.ShouldContain("results");
    }

    #endregion

    #region Site Search Tests

    [Fact]
    public async Task SiteSearch_IndexContent_ReturnsIndexedContent()
    {
        // Arrange
        var indexRequest = new
        {
            title = "Test Page",
            description = "This is a test page",
            content = "Some content here",
            url = "https://example.com/test",
            type = "PAGE"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(indexRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        
        // Act
        var response = await httpClient.PostAsync("/cms/v3/site-search/index", content);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("Test Page");
        responseJson.ShouldContain("test page");
    }

    [Fact]
    public async Task SiteSearch_Search_ReturnsResults()
    {
        // Arrange - Index some content first
        var indexRequest = new
        {
            title = "Searchable Page",
            description = "This is searchable",
            content = "Content with keyword foobar",
            url = "https://example.com/search",
            type = "PAGE"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(indexRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        await httpClient.PostAsync("/cms/v3/site-search/index", content);
        
        // Act
        var response = await httpClient.GetAsync("/cms/v3/site-search/search?q=foobar");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("foobar");
        responseJson.ShouldContain("Searchable Page");
    }

    #endregion

    #region Content Audit Tests

    [Fact]
    public async Task ContentAudit_GetLogs_ReturnsLogs()
    {
        // Arrange
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        
        // Act
        var response = await httpClient.GetAsync("/cms/v3/audit-logs");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("results");
    }

    #endregion

    #region Media Bridge Tests

    [Fact]
    public async Task MediaBridge_CreateAsset_ReturnsAsset()
    {
        // Arrange
        var createAssetRequest = new
        {
            name = "test-image.jpg",
            url = "https://cdn.example.com/test-image.jpg",
            type = "IMAGE",
            mimeType = "image/jpeg",
            width = 1920,
            height = 1080
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createAssetRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        
        // Act
        var response = await httpClient.PostAsync("/cms/v3/media-bridge", content);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("test-image.jpg");
        responseJson.ShouldContain("IMAGE");
    }

    [Fact]
    public async Task MediaBridge_GetAssets_ReturnsAssetsList()
    {
        // Arrange - Create an asset
        var createAssetRequest = new
        {
            name = "video.mp4",
            url = "https://cdn.example.com/video.mp4",
            type = "VIDEO",
            mimeType = "video/mp4"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createAssetRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        await httpClient.PostAsync("/cms/v3/media-bridge", content);
        
        // Act
        var response = await httpClient.GetAsync("/cms/v3/media-bridge");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("video.mp4");
        responseJson.ShouldContain("results");
    }

    [Fact]
    public async Task MediaBridge_GetAsset_ReturnsAsset()
    {
        // Arrange - Create an asset first
        var createAssetRequest = new
        {
            name = "document.pdf",
            url = "https://cdn.example.com/document.pdf",
            type = "DOCUMENT",
            mimeType = "application/pdf"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(createAssetRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        using var httpClient = new HttpClient { BaseAddress = _server.BaseUri };
        var createResponse = await httpClient.PostAsync("/cms/v3/media-bridge", content);
        var createResponseJson = await createResponse.Content.ReadAsStringAsync();
        var doc = System.Text.Json.JsonDocument.Parse(createResponseJson);
        var assetId = doc.RootElement.GetProperty("id").GetString();
        
        // Act
        var response = await httpClient.GetAsync($"/cms/v3/media-bridge/{assetId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        responseJson.ShouldContain("document.pdf");
        responseJson.ShouldContain("DOCUMENT");
    }

    #endregion
}




