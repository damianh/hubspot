namespace DamianH.HubSpot.MockServer.Repositories.HubDb;

internal class HubDbRepository
{
    private readonly Dictionary<string, HubDbTable> _tables = new();
    private readonly Dictionary<string, Dictionary<string, HubDbRow>> _rows = new();
    private int _nextTableId = 1;
    private readonly Dictionary<string, int> _nextRowIds = new();

    public HubDbTable CreateTable(HubDbTable table)
    {
        table.Id = _nextTableId++.ToString();
        table.CreatedAt = DateTime.UtcNow;
        table.UpdatedAt = DateTime.UtcNow;
        _tables[table.Id] = table;
        _rows[table.Id] = new Dictionary<string, HubDbRow>();
        _nextRowIds[table.Id] = 1;
        return table;
    }

    public HubDbTable? GetTableById(string id) => _tables.GetValueOrDefault(id);

    public List<HubDbTable> GetAllTables(int offset = 0, int limit = 100) => _tables.Values
            .OrderBy(t => t.Name)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public HubDbTable? UpdateTable(string id, HubDbTable updatedTable)
    {
        if (!_tables.ContainsKey(id))
        {
            return null;
        }

        updatedTable.Id = id;
        updatedTable.UpdatedAt = DateTime.UtcNow;
        _tables[id] = updatedTable;
        return updatedTable;
    }

    public bool DeleteTable(string id)
    {
        var removed = _tables.Remove(id);
        if (removed)
        {
            _rows.Remove(id);
            _nextRowIds.Remove(id);
        }
        return removed;
    }

    public HubDbRow CreateRow(string tableId, HubDbRow row)
    {
        if (!_tables.ContainsKey(tableId))
        {
            throw new InvalidOperationException($"Table {tableId} not found");
        }

        if (!_nextRowIds.ContainsKey(tableId))
        {
            _nextRowIds[tableId] = 1;
        }

        row.Id = _nextRowIds[tableId]++.ToString();
        row.CreatedAt = DateTime.UtcNow;
        row.UpdatedAt = DateTime.UtcNow;
        _rows[tableId][row.Id] = row;
        return row;
    }

    public HubDbRow? GetRowById(string tableId, string rowId) => _rows.GetValueOrDefault(tableId)?.GetValueOrDefault(rowId);

    public List<HubDbRow> GetAllRows(string tableId, int offset = 0, int limit = 100)
    {
        if (!_rows.ContainsKey(tableId))
        {
            return [];
        }

        return _rows[tableId].Values
            .OrderBy(r => r.Id)
            .Skip(offset)
            .Take(limit)
            .ToList();
    }

    public HubDbRow? UpdateRow(string tableId, string rowId, HubDbRow updatedRow)
    {
        if (!_rows.ContainsKey(tableId) || !_rows[tableId].ContainsKey(rowId))
        {
            return null;
        }

        updatedRow.Id = rowId;
        updatedRow.UpdatedAt = DateTime.UtcNow;
        _rows[tableId][rowId] = updatedRow;
        return updatedRow;
    }

    public bool DeleteRow(string tableId, string rowId) => _rows.GetValueOrDefault(tableId)?.Remove(rowId) ?? false;

    public int GetTableCount() => _tables.Count;

    public int GetRowCount(string tableId) => _rows.GetValueOrDefault(tableId)?.Count ?? 0;

    public void Clear()
    {
        _tables.Clear();
        _rows.Clear();
        _nextRowIds.Clear();
        _nextTableId = 1;
    }
}
