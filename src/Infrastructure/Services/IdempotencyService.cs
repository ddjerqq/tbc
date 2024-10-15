using Application.Services;

#pragma warning disable CS1591
#pragma warning disable VSTHRD110
namespace Infrastructure.Services;

public sealed class IdempotencyService : IIdempotencyService
{
    private static readonly TimeSpan InvalidationTime = TimeSpan.FromMinutes(10);
    private readonly HashSet<Guid> _keys = [];

    public bool ContainsKey(Guid key) => _keys.Contains(key);

    public void AddKey(Guid key)
    {
        _keys.Add(key);

        Task.Delay(InvalidationTime)
            .ContinueWith(_ => _keys.Remove(key), TaskScheduler.Default);
    }
}