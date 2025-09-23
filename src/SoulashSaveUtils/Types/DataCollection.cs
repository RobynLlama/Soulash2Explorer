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
using SoulashSaveUtils.Helpers;

namespace SoulashSaveUtils.Types;

public class DataCollection
{
  public readonly Dictionary<int, DataBuilding> AllDataBuildings = [];
  public Dictionary<string, Portrait> PortraitFrames { get; protected set; } = [];

  public bool LoadAllDataFromSource(string sourceMod)
  {
    if (!LoadDataBuildingData(sourceMod))
      return false;

    if (!LoadPortraitsFromSource(sourceMod))
      return false;

    return true;
  }

  public bool LoadDataBuildingData(string source)
  {
    if (!DataBuildingListing.Create(source, AllDataBuildings))
      return false;

    GD.Print($"""
    Total {source} Buildings:
      {AllDataBuildings.Keys.Count}
    """);

    return true;
  }

  public bool LoadPortraitsFromSource(string source)
  {
    if (Paths.ConfiguredPaths.GameBasePath == string.Empty)
    {
      GD.PushError("Unable to read GameBasePath");
      return false;
    }

    FileInfo ports = new(Path.Combine(Paths.DataPath, source, "portraits", "portrait_parts.json"));

    if (!ports.Exists)
    {
      GD.PushError($"Unable to read portraits_parts.json from: {ports.FullName}");
      return false;
    }

    StreamReader reader = new(ports.OpenRead());

    if (JsonSerializer.Deserialize<FramesData>(reader.ReadToEnd()) is FramesData frames)
    {
      foreach (var entry in frames.Import)
      {
        var key = entry.Key[21..].Replace(").aseprite", string.Empty);

        GD.Print($"""
          Reading Portrait: {key}
          """);
        PortraitFrames[key] = entry.Value;
      }
      return true;
    }

    return false;
  }
}
