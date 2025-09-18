using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SoulashSaveUtils.Types;

public class FramesData
{
  [JsonPropertyName("frames")]
  [JsonInclude]
  public Dictionary<string, Portrait> Import { get; protected set; } = [];
}
