using System.Text.Json.Serialization;

namespace SoulashExplorer.Types;

public class DataBuilding
{
  public int ID { get; protected set; }

  [JsonPropertyName("id")]
  [JsonInclude]
  protected string StrID
  {
    get { return ID.ToString(); }
    set
    {
      if (int.TryParse(value, out var realID))
        ID = realID;
    }
  }

  [JsonPropertyName("name")]
  [JsonInclude]
  public string Name { get; protected set; } = string.Empty;
}
