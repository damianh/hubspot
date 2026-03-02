namespace DamianH.HubSpot.MockServer.Repositories.Blog;

internal class BlogPostRepository
{
    private readonly TimeProvider _timeProvider;
    private readonly Dictionary<string, BlogPost> _posts = new();
    private readonly Dictionary<string, List<BlogPostRevision>> _revisions = new();
    private readonly Dictionary<string, List<string>> _languageGroups = new();
    private int _nextId = 1;

    public BlogPostRepository(TimeProvider timeProvider) => _timeProvider = timeProvider;

    public BlogPost Create(BlogPost post)
    {
        post.Id = _nextId++.ToString();
        post.Created = _timeProvider.GetUtcNow().UtcDateTime;
        post.Updated = _timeProvider.GetUtcNow().UtcDateTime;
        _posts[post.Id] = post;

        AddRevision(post.Id, post);

        return post;
    }

    public BlogPost? GetById(string id) => _posts.GetValueOrDefault(id);

    public List<BlogPost> GetAll(int offset = 0, int limit = 100) => _posts.Values
            .OrderByDescending(p => p.Created)
            .Skip(offset)
            .Take(limit)
            .ToList();

    public BlogPost? Update(string id, BlogPost updatedPost)
    {
        if (!_posts.ContainsKey(id))
        {
            return null;
        }

        updatedPost.Id = id;
        updatedPost.Updated = _timeProvider.GetUtcNow().UtcDateTime;
        _posts[id] = updatedPost;

        AddRevision(id, updatedPost);

        return updatedPost;
    }

    public bool Delete(string id)
    {
        var removed = _posts.Remove(id);
        if (removed)
        {
            _revisions.Remove(id);
        }
        return removed;
    }

    public List<BlogPost> BatchCreate(List<BlogPost> posts) => posts.Select(Create).ToList();

    public List<BlogPost> BatchRead(List<string> ids) => ids.Select(id => _posts.GetValueOrDefault(id))
            .Where(p => p != null)
            .Cast<BlogPost>()
            .ToList();

    public List<BlogPost> BatchUpdate(List<BlogPost> posts)
    {
        var results = new List<BlogPost>();
        foreach (var post in posts)
        {
            if (post.Id != null)
            {
                var updated = Update(post.Id, post);
                if (updated != null)
                {
                    results.Add(updated);
                }
            }
        }
        return results;
    }

    public int BatchDelete(List<string> ids) => ids.Count(Delete);

    private void AddRevision(string postId, BlogPost post)
    {
        if (!_revisions.TryGetValue(postId, out var postRevisions))
        {
            postRevisions = [];
            _revisions[postId] = postRevisions;
        }

        var revision = new BlogPostRevision
        {
            Id = (postRevisions.Count + 1).ToString(),
            PostId = postId,
            CreatedAt = _timeProvider.GetUtcNow().UtcDateTime,
            Content = post
        };

        postRevisions.Add(revision);
    }

    public List<BlogPostRevision> GetRevisions(string postId) => _revisions.GetValueOrDefault(postId) ?? [];

    public BlogPostRevision? GetRevisionById(string postId, string revisionId) => _revisions.GetValueOrDefault(postId)
            ?.FirstOrDefault(r => r.Id == revisionId);

    public BlogPost? RestoreRevision(string postId, string revisionId)
    {
        var revision = GetRevisionById(postId, revisionId);
        if (revision?.Content == null)
        {
            return null;
        }

        return Update(postId, revision.Content);
    }

    public void AttachToLanguageGroup(string postId, string languageGroupId)
    {
        if (!_languageGroups.TryGetValue(languageGroupId, out var group))
        {
            group = [];
            _languageGroups[languageGroupId] = group;
        }

        if (!group.Contains(postId))
        {
            group.Add(postId);
        }
    }

    public void DetachFromLanguageGroup(string postId)
    {
        foreach (var group in _languageGroups.Values)
        {
            group.Remove(postId);
        }
    }

    public List<BlogPost> GetLanguageVariants(string postId)
    {
        var post = GetById(postId);
        if (post?.Language == null)
        {
            return [];
        }

        var groupId = _languageGroups
            .FirstOrDefault(g => g.Value.Contains(postId))
            .Key;

        if (groupId == null)
        {
            return [post];
        }

        return _languageGroups[groupId]
            .Select(GetById)
            .Where(p => p != null)
            .Cast<BlogPost>()
            .ToList();
    }

    public int Count() => _posts.Count;

    public void Clear()
    {
        _posts.Clear();
        _revisions.Clear();
        _languageGroups.Clear();
        _nextId = 1;
    }
}
