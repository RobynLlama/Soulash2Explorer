/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

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
      LoggingWindow.Instance.LogMessage("Loading path settings from config");
      ConfiguredPaths = ResourceLoader.Load<PathConfig>(path);
      return;
    }

    LoggingWindow.Instance.LogMessage("Using default path settings");
    ConfiguredPaths = new();
  }

  public static void SaveConfig()
  {
    var path = Path.Combine(OS.GetUserDataDir(), "paths.tres");
    ResourceSaver.Save(ConfiguredPaths, path, ResourceSaver.SaverFlags.None);
  }
}
