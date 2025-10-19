using System.Text.Json.Serialization;

namespace SoulashSaveUtils.Types;

public class DataSkill
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
