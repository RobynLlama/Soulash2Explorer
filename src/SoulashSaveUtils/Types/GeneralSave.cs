using System.Text.Json.Serialization;

namespace SoulashSaveUtils;

public class GeneralSave
{
  [JsonInclude]
  [JsonPropertyName("day")]
  public int Day { get; set; }

  [JsonInclude]
  [JsonPropertyName("year")]
  public int Year { get; set; }

  [JsonInclude]
  [JsonPropertyName("game_version")]
  public string GameVersion { get; set; }

  [JsonInclude]
  [JsonPropertyName("required_mods")]
  public string[] RequiredMods { get; set; }

  [JsonInclude]
  [JsonPropertyName("seed")]
  public string WorldSeed { get; set; }

  [JsonInclude]
  [JsonPropertyName("world_name")]
  public string WorldName { get; set; }
}
