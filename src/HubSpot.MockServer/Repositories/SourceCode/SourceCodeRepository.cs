namespace DamianH.HubSpot.MockServer.Repositories.SourceCode;

internal class SourceCodeRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly Dictionary<string, SourceCodeFile> _files = new();
    private int _nextId = 1;



    public SourceCodeRepository(TimeProvider timeProvider) => _timeProvider = timeProvider;

    public SourceCodeFile Create(SourceCodeFile file)
    {
        file.Id = _nextId++.ToString();
        file.CreatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        file.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        _files[file.Path ?? file.Id] = file;
        return file;
    }

    public SourceCodeFile? GetByPath(string path) => _files.GetValueOrDefault(path);

    public List<SourceCodeFile> GetAll() => _files.Values.OrderBy(f => f.Path).ToList();

    public SourceCodeFile? Update(string path, SourceCodeFile updatedFile)
    {
        if (!_files.ContainsKey(path))
        {
            return null;
        }

        updatedFile.Path = path;
        updatedFile.UpdatedAt = _timeProvider.GetUtcNow().UtcDateTime;
        _files[path] = updatedFile;
        return updatedFile;
    }

    public bool Delete(string path) => _files.Remove(path);

    public int Count() => _files.Count;

    public void Clear()
    {
        _files.Clear();
        _nextId = 1;
    }
}
