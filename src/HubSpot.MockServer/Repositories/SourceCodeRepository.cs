namespace DamianH.HubSpot.MockServer.Repositories;

public class SourceCodeRepository
{
    private readonly Dictionary<string, SourceCodeFile> _files = new();
    private int _nextId = 1;

    public SourceCodeFile Create(SourceCodeFile file)
    {
        file.Id = _nextId++.ToString();
        file.CreatedAt = DateTime.UtcNow;
        file.UpdatedAt = DateTime.UtcNow;
        _files[file.Path ?? file.Id] = file;
        return file;
    }

    public SourceCodeFile? GetByPath(string path)
    {
        return _files.GetValueOrDefault(path);
    }

    public List<SourceCodeFile> GetAll()
    {
        return _files.Values.OrderBy(f => f.Path).ToList();
    }

    public SourceCodeFile? Update(string path, SourceCodeFile updatedFile)
    {
        if (!_files.ContainsKey(path))
        {
            return null;
        }

        updatedFile.Path = path;
        updatedFile.UpdatedAt = DateTime.UtcNow;
        _files[path] = updatedFile;
        return updatedFile;
    }

    public bool Delete(string path)
    {
        return _files.Remove(path);
    }

    public int Count()
    {
        return _files.Count;
    }

    public void Clear()
    {
        _files.Clear();
        _nextId = 1;
    }
}

public class SourceCodeFile
{
    public string? Id { get; set; }
    public string? Path { get; set; }
    public string? Content { get; set; }
    public string? Type { get; set; } // "HTML", "CSS", "JS", "MODULE", etc.
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
