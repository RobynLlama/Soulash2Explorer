using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SoulashExplorer.Helpers;

namespace SoulashExplorer.Types;

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

    Console.WriteLine($"""
    Total {source} Buildings:
      {AllDataBuildings.Keys.Count}
    """);

    return true;
  }

  public bool LoadPortraitsFromSource(string source)
  {
    if (Paths.GameBasePath == string.Empty)
    {
      Console.WriteLine("Unable to read GameBasePath");
      return false;
    }

    FileInfo ports = new(Path.Combine(Paths.DataPath, source, "portraits", "portrait_parts.json"));

    if (!ports.Exists)
    {
      Console.WriteLine($"Unable to read portraits_parts.json from: {ports.FullName}");
      return false;
    }

    Console.WriteLine("Reading Portraits..");
    StreamReader reader = new(ports.OpenRead());

    if (JsonSerializer.Deserialize<FramesData>(reader.ReadToEnd()) is FramesData frames)
    {
      foreach (var entry in frames.Import)
      {
        var key = entry.Key[21..].Replace(").aseprite", string.Empty);

        Console.WriteLine($"""
          Reading Portrait: {key}
          """);
        PortraitFrames[key] = entry.Value;
      }
      return true;
    }

    return false;
  }
}
