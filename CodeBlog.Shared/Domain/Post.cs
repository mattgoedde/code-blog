using System.Text.Json;

namespace CodeBlog.Data.Domain
{
    [Serializable]
    public record Post : EntityBase
    {
        public string Content { get; init; } = string.Empty;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
