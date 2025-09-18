/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using SoulashSaveUtils.Helpers;

namespace SoulashSaveUtils.Types;

public class Glyph
{
  public Frame Frame;

  public Glyph(string itemName)
  {
    //Todo: Store color info
    if (DataBase.LoadedData.PortraitFrames.TryGetValue(itemName, out var port))
      Frame = port.Frame;
    else
      throw new InvalidOperationException($"Missing portrait {itemName}");
  }
}
