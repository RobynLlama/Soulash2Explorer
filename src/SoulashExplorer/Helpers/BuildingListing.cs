using System;
using System.Collections.Generic;
using System.IO;
using SoulashExplorer.Types;

namespace SoulashExplorer.Helpers;

public static class BuildingListing
{
  public static bool Create(FileInfo buildingFile, Dictionary<int, SaveBuilding> table)
  {
    if (!buildingFile.Exists)
    {
      Console.WriteLine("Building file doesn't real");
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

      Console.WriteLine($"Building: {id}/{name}");
      table[id] = new(id, name);
    }

    return true;
  }
}
