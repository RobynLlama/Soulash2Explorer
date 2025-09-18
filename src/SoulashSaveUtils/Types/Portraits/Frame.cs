/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System.Text.Json.Serialization;

namespace SoulashSaveUtils.Types;

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
