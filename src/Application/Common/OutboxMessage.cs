using System.ComponentModel.DataAnnotations;
using Destructurama.Attributed;
using Domain.Event;
using Newtonsoft.Json;

namespace Application.Common;

public sealed class OutboxMessage
{
    public static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy(),
        },
    };

    public Ulid Id { get; init; } = Ulid.NewUlid();

    [StringLength(128)]
    public string Type { get; init; } = default!;

    [LogMasked]
    [StringLength(1024)]
    public string Content { get; init; } = string.Empty;

    public DateTime OccuredOnUtc { get; init; }

    public DateTime? ProcessedOnUtc { get; set; }

    [StringLength(1024)]
    public string? Error { get; set; }

    public static OutboxMessage FromDomainEvent(IDomainEvent domainEvent)
    {
        return new OutboxMessage
        {
            Id = Ulid.NewUlid(),
            Type = domainEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings),
            OccuredOnUtc = DateTime.UtcNow,
            ProcessedOnUtc = null,
            Error = null,
        };
    }
}