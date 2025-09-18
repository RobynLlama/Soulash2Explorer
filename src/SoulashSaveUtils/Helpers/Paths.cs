using System.IO;

namespace SoulashSaveUtils.Helpers;

public static class Paths
{
  public static string GameBasePath = string.Empty;
  public static string GameSavesPath = string.Empty;
  public static string DataPath => Path.Combine(GameBasePath, "data", "mods");
}
