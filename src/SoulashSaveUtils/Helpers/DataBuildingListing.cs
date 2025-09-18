using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class DataBuildingListing
{
  public static bool Create(string source, Dictionary<int, DataBuilding> table)
  {
    if (Paths.GameBasePath == string.Empty)
    {
      Console.WriteLine("Unable to read GameBasePath");
      return false;
    }

    DirectoryInfo sourceBuildings = new(Path.Combine(Paths.DataPath, source, "buildings"));

    if (!sourceBuildings.Exists)
    {
      Console.WriteLine($"Unable to read buildings @ {sourceBuildings.FullName}");
      return false;
    }

    foreach (var file in sourceBuildings.EnumerateFiles("*.json"))
    {
      Console.WriteLine($"Loading File: {file.Name}");
      StreamReader reader = new(file.OpenRead());


      if (JsonSerializer.Deserialize<DataBuilding>(reader.ReadToEnd()) is DataBuilding building)
      {
        table[building.ID] = building;
        Console.WriteLine($"  Read DataBuilding: {building.ID}/{building.Name}");
      }
      else
        Console.WriteLine("Skipping");
    }

    return true;
  }
}
