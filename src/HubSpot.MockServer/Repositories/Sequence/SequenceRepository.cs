using System.Collections.Concurrent;

namespace DamianH.HubSpot.MockServer.Repositories.Sequence;

internal class SequenceRepository
{
    private readonly ConcurrentDictionary<string, Sequence> _sequences = new();
    private readonly ConcurrentDictionary<string, SequenceEnrollment> _enrollments = new();
    private readonly ConcurrentDictionary<string, List<string>> _enrollmentsByContact = new();
    private readonly ConcurrentDictionary<string, List<string>> _enrollmentsBySequence = new();
    private int _nextEnrollmentId = 1;

    public Sequence? GetSequence(string sequenceId)
    {
        _sequences.TryGetValue(sequenceId, out var sequence);
        return sequence;
    }

    public IReadOnlyList<Sequence> GetAllSequences() => _sequences.Values.ToList();

    public void AddSequence(Sequence sequence) => _sequences[sequence.Id] = sequence;

    public SequenceEnrollment CreateEnrollment(string contactId, string sequenceId, int userId)
    {
        var enrollmentId = _nextEnrollmentId++.ToString();
        var enrollment = new SequenceEnrollment
        {
            EnrollmentId = enrollmentId,
            ContactId = contactId,
            SequenceId = sequenceId,
            UserId = userId,
            State = "ENROLLED",
            EnrolledAt = DateTimeOffset.UtcNow
        };

        _enrollments[enrollmentId] = enrollment;

        _enrollmentsByContact.AddOrUpdate(
            contactId,
            [enrollmentId],
            (_, list) =>
            {
                list.Add(enrollmentId);
                return list;
            });

        _enrollmentsBySequence.AddOrUpdate(
            sequenceId,
            [enrollmentId],
            (_, list) =>
            {
                list.Add(enrollmentId);
                return list;
            });

        return enrollment;
    }

    public SequenceEnrollment? GetEnrollment(string enrollmentId)
    {
        _enrollments.TryGetValue(enrollmentId, out var enrollment);
        return enrollment;
    }

    public IReadOnlyList<SequenceEnrollment> GetEnrollmentsByContact(string contactId)
    {
        if (!_enrollmentsByContact.TryGetValue(contactId, out var enrollmentIds))
        {
            return Array.Empty<SequenceEnrollment>();
        }

        return enrollmentIds
            .Select(id => _enrollments.GetValueOrDefault(id))
            .Where(e => e != null)
            .ToList()!;
    }

    public IReadOnlyList<SequenceEnrollment> GetEnrollmentsBySequence(string sequenceId)
    {
        if (!_enrollmentsBySequence.TryGetValue(sequenceId, out var enrollmentIds))
        {
            return Array.Empty<SequenceEnrollment>();
        }

        return enrollmentIds
            .Select(id => _enrollments.GetValueOrDefault(id))
            .Where(e => e != null)
            .ToList()!;
    }

    public void UpdateEnrollmentState(string enrollmentId, string newState)
    {
        if (_enrollments.TryGetValue(enrollmentId, out var enrollment))
        {
            _enrollments[enrollmentId] = enrollment with { State = newState };
        }
    }

    public void Clear()
    {
        _sequences.Clear();
        _enrollments.Clear();
        _enrollmentsByContact.Clear();
        _enrollmentsBySequence.Clear();
        _nextEnrollmentId = 1;
    }
}
