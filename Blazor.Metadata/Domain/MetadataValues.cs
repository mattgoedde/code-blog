using System.Text.Json;

namespace Blazor.Metadata.Domain
{
    [Serializable]
    public record MetadataValues
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public string Author { get; init; } = string.Empty;
        public IEnumerable<string> Keywords { get; init; } = Enumerable.Empty<string>();

        public string SocialMediaCardTitle => $"Goedde's Code Blog - {Title}";

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
