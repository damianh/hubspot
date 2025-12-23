using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.File;

internal class FileRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly ConcurrentDictionary<string, FileMetadata> _files = new();
    private long _nextFileId = 1;



    public FileRepository(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

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
            CreatedAt = _timeProvider.GetUtcNow().UtcDateTime,
            UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime,
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
            file.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        }

        return file;
    }

    public bool DeleteFile(string fileId) => _files.TryRemove(fileId, out _);

    public byte[]? GetFileContent(string fileId) => _files.TryGetValue(fileId, out var file) ? file.Content : null;
}
