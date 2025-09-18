using System;
using System.IO;
using System.Linq;
using Godot;
using SoulashSaveUtils.Helpers;
using SoulashSaveUtils.Types;

namespace Soulash2Explorer;

public partial class SceneRoot : Node2D
{
  public override void _Ready()
  {
    //setup paths
    Paths.GameBasePath = "/home/qtmims/.steam/debian-installation/steamapps/common/Soulash 2";
    Paths.GameSavesPath = "/home/qtmims/.steam/debian-installation/steamapps/common/Soulash 2/AppData/saves/Lil World";

    //preload the portrait atlas
    PortraitStorage.LoadTexture();

    DataBase.LoadedData.LoadAllDataFromSource("core_2");
    SaveCollection saveData = new(new DirectoryInfo(Paths.GameSavesPath));
    saveData.LoadActorsSave();

    SaveEntity ent = saveData.AllEntities.Values.Where(x => x.Glyphs.Length > 3).First();

    GD.Print($"Drawing glyphs for: {ent.GetFullName}");

    foreach (var glyph in ent.Glyphs)
      AddChild(new Sprite2D
      {
        Texture = PortraitStorage.Texture,
        Centered = false,
        RegionEnabled = true,
        RegionRect = new(glyph.Frame.XOffset, glyph.Frame.YOffset, glyph.Frame.Width, glyph.Frame.Height)
      });
  }
}
