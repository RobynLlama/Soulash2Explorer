using System.IO;

namespace SoulashExplorer.Helpers;

public static class Paths
{
  public static string GameBasePath = string.Empty;
  public static string DataPath => Path.Combine(GameBasePath, "data", "mods");
}
