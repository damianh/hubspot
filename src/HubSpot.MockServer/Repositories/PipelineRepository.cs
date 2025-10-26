using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories;

/// <summary>
/// Repository for managing pipelines and their stages
/// </summary>
internal class PipelineRepository
{
    private readonly ConcurrentDictionary<string, Pipeline> _pipelines = new();
    private readonly ConcurrentDictionary<string, PipelineStage> _stages = new();
    private int _nextPipelineId = 1;
    private int _nextStageId = 1;

    public record Pipeline(
        string Id,
        string Label,
        string ObjectType,
        int DisplayOrder = 0,
        DateTime CreatedAt = default,
        DateTime UpdatedAt = default);

    public record PipelineStage(
        string Id,
        string PipelineId,
        string Label,
        int DisplayOrder = 0,
        Dictionary<string, string>? Metadata = null,
        DateTime CreatedAt = default,
        DateTime UpdatedAt = default);

    public PipelineRepository() => SeedDefaultPipelines();

    private void SeedDefaultPipelines()
    {
        var now = DateTime.UtcNow;

        // Default Sales Pipeline for Deals
        var dealsPipeline = new Pipeline("default", "Sales Pipeline", "deals", 0, now, now);
        _pipelines[GetPipelineKey("deals", "default")] = dealsPipeline;

        // Deal stages
        AddStage(new PipelineStage("appointmentscheduled", "default", "Appointment Scheduled", 0, null, now, now));
        AddStage(new PipelineStage("qualifiedtobuy", "default", "Qualified to Buy", 1, null, now, now));
        AddStage(new PipelineStage("presentationscheduled", "default", "Presentation Scheduled", 2, null, now, now));
        AddStage(new PipelineStage("decisionmakerboughtin", "default", "Decision Maker Bought-In", 3, null, now, now));
        AddStage(new PipelineStage("contractsent", "default", "Contract Sent", 4, null, now, now));
        AddStage(new PipelineStage("closedwon", "default", "Closed Won", 5, new Dictionary<string, string> { ["isClosed"] = "true" }, now, now));
        AddStage(new PipelineStage("closedlost", "default", "Closed Lost", 6, new Dictionary<string, string> { ["isClosed"] = "true" }, now, now));

        // Default Support Pipeline for Tickets
        var ticketsPipeline = new Pipeline("0", "Support Pipeline", "tickets", 0, now, now);
        _pipelines[GetPipelineKey("tickets", "0")] = ticketsPipeline;

        // Ticket stages
        AddStage(new PipelineStage("1", "0", "New", 0, null, now, now));
        AddStage(new PipelineStage("2", "0", "Waiting on contact", 1, null, now, now));
        AddStage(new PipelineStage("3", "0", "Waiting on us", 2, null, now, now));
        AddStage(new PipelineStage("4", "0", "Closed", 3, new Dictionary<string, string> { ["isClosed"] = "true" }, now, now));
    }

    public Pipeline? GetPipeline(string objectType, string pipelineId)
    {
        var key = GetPipelineKey(objectType, pipelineId);
        return _pipelines.GetValueOrDefault(key);
    }

    public IReadOnlyList<Pipeline> GetPipelines(string objectType) => _pipelines.Values
            .Where(p => p.ObjectType == objectType)
            .OrderBy(p => p.DisplayOrder)
            .ThenBy(p => p.Label)
            .ToList();

    public Pipeline CreatePipeline(
        string objectType,
        string label,
        int displayOrder = 0)
    {
        var now = DateTime.UtcNow;
        var id = Interlocked.Increment(ref _nextPipelineId).ToString();
        var pipeline = new Pipeline(id, label, objectType, displayOrder, now, now);

        var key = GetPipelineKey(objectType, id);
        _pipelines[key] = pipeline;
        return pipeline;
    }

    public Pipeline? UpdatePipeline(
        string objectType,
        string pipelineId,
        string? label = null,
        int? displayOrder = null)
    {
        var existing = GetPipeline(objectType, pipelineId);
        if (existing == null)
        {
            return null;
        }

        var updated = existing with
        {
            Label = label ?? existing.Label,
            DisplayOrder = displayOrder ?? existing.DisplayOrder,
            UpdatedAt = DateTime.UtcNow
        };

        var key = GetPipelineKey(objectType, pipelineId);
        _pipelines[key] = updated;
        return updated;
    }

    public bool DeletePipeline(string objectType, string pipelineId)
    {
        // Delete all stages in this pipeline
        var stagesToDelete = _stages.Values
            .Where(s => s.PipelineId == pipelineId)
            .ToList();

        foreach (var stage in stagesToDelete)
        {
            var stageKey = GetStageKey(pipelineId, stage.Id);
            _stages.TryRemove(stageKey, out _);
        }

        var key = GetPipelineKey(objectType, pipelineId);
        return _pipelines.TryRemove(key, out _);
    }

    public PipelineStage? GetStage(string pipelineId, string stageId)
    {
        var key = GetStageKey(pipelineId, stageId);
        return _stages.GetValueOrDefault(key);
    }

    public IReadOnlyList<PipelineStage> GetStages(string pipelineId) => _stages.Values
            .Where(s => s.PipelineId == pipelineId)
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.Label)
            .ToList();

    public PipelineStage CreateStage(
        string pipelineId,
        string label,
        int displayOrder = 0,
        Dictionary<string, string>? metadata = null)
    {
        var now = DateTime.UtcNow;
        var id = Interlocked.Increment(ref _nextStageId).ToString();
        var stage = new PipelineStage(id, pipelineId, label, displayOrder, metadata, now, now);

        AddStage(stage);
        return stage;
    }

    public PipelineStage? UpdateStage(
        string pipelineId,
        string stageId,
        string? label = null,
        int? displayOrder = null,
        Dictionary<string, string>? metadata = null)
    {
        var existing = GetStage(pipelineId, stageId);
        if (existing == null)
        {
            return null;
        }

        var updated = existing with
        {
            Label = label ?? existing.Label,
            DisplayOrder = displayOrder ?? existing.DisplayOrder,
            Metadata = metadata ?? existing.Metadata,
            UpdatedAt = DateTime.UtcNow
        };

        AddStage(updated);
        return updated;
    }

    public bool DeleteStage(string pipelineId, string stageId)
    {
        var key = GetStageKey(pipelineId, stageId);
        return _stages.TryRemove(key, out _);
    }

    public void Clear()
    {
        _pipelines.Clear();
        _stages.Clear();
        _nextPipelineId = 1;
        _nextStageId = 1;
        SeedDefaultPipelines();
    }

    private void AddStage(PipelineStage stage)
    {
        var key = GetStageKey(stage.PipelineId, stage.Id);
        _stages[key] = stage;
    }

    private static string GetPipelineKey(string objectType, string pipelineId) => $"{objectType}:{pipelineId}";

    private static string GetStageKey(string pipelineId, string stageId) => $"{pipelineId}:{stageId}";
}
