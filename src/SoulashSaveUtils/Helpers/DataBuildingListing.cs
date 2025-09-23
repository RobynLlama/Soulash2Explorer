/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class DataBuildingListing
{
  public static bool Create(string source, Dictionary<int, DataBuilding> table)
  {
    if (Paths.ConfiguredPaths.GameBasePath == string.Empty)
    {
      GD.PushError("Unable to read GameBasePath");
      return false;
    }

    DirectoryInfo sourceBuildings = new(Path.Combine(Paths.DataPath, source, "buildings"));

    if (!sourceBuildings.Exists)
    {
      GD.PushError($"Unable to read buildings @ {sourceBuildings.FullName}");
      return false;
    }

    foreach (var file in sourceBuildings.EnumerateFiles("*.json"))
    {
      GD.Print($"Loading File: {file.Name}");
      StreamReader reader = new(file.OpenRead());


      if (JsonSerializer.Deserialize<DataBuilding>(reader.ReadToEnd()) is DataBuilding building)
        table[building.ID] = building;
      else
        GD.PushWarning("  File does not contain DataBuilding!");
    }

    return true;
  }
}
