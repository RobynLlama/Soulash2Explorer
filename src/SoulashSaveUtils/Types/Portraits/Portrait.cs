using System.Text.Json.Serialization;

namespace SoulashSaveUtils.Types;

[method: JsonConstructor]
public class Portrait(Frame frame)
{
  [JsonInclude]
  [JsonPropertyName("frame")]
  public Frame Frame { get; protected set; } = frame;
}
