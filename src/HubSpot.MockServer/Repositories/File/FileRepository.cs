using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.File;

internal class FileRepository
{
    private readonly ConcurrentDictionary<string, FileMetadata> _files = new();
    private long _nextFileId = 1;

    public FileMetadata UploadFile(string fileName, string contentType, byte[] content)
    {
        var file = new FileMetadata
        {
            Id = _nextFileId++.ToString(),
            Name = fileName,
            Type = contentType,
            Extension = Path.GetExtension(fileName).TrimStart('.'),
            Size = content.Length,
            Content = content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Url = $"https://mock.hubspot.com/files/{_nextFileId - 1}/{fileName}"
        };

        _files[file.Id] = file;
        return file;
    }

    public FileMetadata? GetFile(string fileId) => _files.GetValueOrDefault(fileId);

    public IEnumerable<FileMetadata> GetAllFiles() => _files.Values.ToList();

    public FileMetadata? UpdateFile(string fileId, string? newName)
    {
        if (!_files.TryGetValue(fileId, out var file))
        {
            return null;
        }

        if (newName != null)
        {
            file.Name = newName;
            file.Extension = Path.GetExtension(newName).TrimStart('.');
            file.UpdatedAt = DateTime.UtcNow;
        }

        return file;
    }

    public bool DeleteFile(string fileId) => _files.TryRemove(fileId, out _);

    public byte[]? GetFileContent(string fileId) => _files.TryGetValue(fileId, out var file) ? file.Content : null;
}

public class FileMetadata
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public long Size { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Url { get; set; } = null!;
    public byte[] Content { get; set; } = null!;
}
