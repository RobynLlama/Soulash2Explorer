/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System.Collections.Generic;

namespace SoulashSaveUtils.Types;

public class GlyphComponent(Glyph[] glyphs) : IEntityComponent
{
  public string ComponentID => "Glyph";
  public Glyph[] Glyphs = glyphs;

  static float GetColorFloat(string cdata)
  {
    if (!int.TryParse(cdata, out var number))
      return 1f;

    return number / 255f;
  }

  public static GlyphComponent BuildComponent(string[] args)
  {
    var parsedGlyphs = new List<Glyph>();

    //ignore the count, not needed
    foreach (var item in args[1..])
    {
      var gInfo = item.Split('*');

      parsedGlyphs.Add(
        new(gInfo[0],
        new(
          GetColorFloat(gInfo[1]),
          GetColorFloat(gInfo[2]),
          GetColorFloat(gInfo[3])
        )));
    }

    return new([.. parsedGlyphs]);
  }
}
