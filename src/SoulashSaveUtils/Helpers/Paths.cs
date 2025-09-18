using System.IO;
using Godot;
using Soulash2Explorer;

namespace SoulashSaveUtils.Helpers;

public static class Paths
{
  public static PathConfig ConfiguredPaths { get; private set; }
  public static string SelectedSave = string.Empty;
  public static string DataPath => Path.Combine(ConfiguredPaths.GameBasePath, "data", "mods");

  static Paths()
  {
    var path = Path.Combine(OS.GetUserDataDir(), "paths.tres");

    if (new FileInfo(path).Exists)
    {
      GD.Print("Loading path settings from config");
      ConfiguredPaths = ResourceLoader.Load<PathConfig>(path);
      return;
    }

    GD.Print("Using default path settings");
    ConfiguredPaths = new();
  }

  public static void SaveConfig()
  {
    var path = Path.Combine(OS.GetUserDataDir(), "paths.tres");
    ResourceSaver.Save(ConfiguredPaths, path, ResourceSaver.SaverFlags.None);
  }
}
