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
      GD.Print("Unable to load file");
      return;
    }

    Texture.SetImage(image);
  }
}
