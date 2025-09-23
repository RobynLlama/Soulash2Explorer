/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.IO;
using Godot;

namespace Soulash2Explorer;

public static class PortraitStorage
{
  public static ImageTexture Texture { get; private set; } = new();
  public static void LoadTexture()
  {
    string texPath = Path.Combine(SoulashSaveUtils.Helpers.Paths.DataPath, "core_2", "assets", "gfx", "portraits", "portrait_parts.png");
    Image image = new();

    GD.Print($"Loading {texPath}");
    if (image.Load(texPath) != Error.Ok)
    {
      GD.PushError("Unable to load portrait file");
      return;
    }

    Texture.SetImage(image);
  }
}
