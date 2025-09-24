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
using Godot;
using Soulash2Explorer;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class BuildingListing
{
  public static bool Create(FileInfo buildingFile, Dictionary<int, SaveBuilding> table)
  {
    if (!buildingFile.Exists)
    {
      LoggingWindow.Instance.LogError("Building file doesn't real");
      return false;
    }

    using TextReader buildings = new StreamReader(buildingFile.FullName);
    string[] items = buildings.ReadToEnd().Split('|');
    int reading = int.Parse(items[1]);

    for (int i = 2; i < reading + 2; i++)
    {
      var data = items[i].Split('*');

      var id = int.Parse(data[0]);
      var name = data[2];

      if (string.IsNullOrEmpty(name))
        name = "[NO NAME]";

      table[id] = new(id, name);
    }

    return true;
  }
}
