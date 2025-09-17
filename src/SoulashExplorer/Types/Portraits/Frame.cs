using System.Text.Json.Serialization;

namespace SoulashExplorer.Types;

public class Frame
{
  [JsonPropertyName("x")]
  [JsonInclude]
  public int XOffset { get; protected set; }

  [JsonPropertyName("y")]
  [JsonInclude]
  public int YOffset { get; protected set; }

  [JsonPropertyName("w")]
  [JsonInclude]
  public int Width { get; protected set; }

  [JsonPropertyName("h")]
  [JsonInclude]
  public int Height { get; protected set; }

  public override string ToString() =>
    $"Offset: {XOffset},{YOffset} | Dimensions: {Width}, {Height}";
}
