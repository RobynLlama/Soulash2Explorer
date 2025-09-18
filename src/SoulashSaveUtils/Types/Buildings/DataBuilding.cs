/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System.Text.Json.Serialization;

namespace SoulashSaveUtils.Types;

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
