using System;
using System.Collections.Generic;
using System.IO;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class FactionListing
{
  public static bool Create(FileInfo factionFile, Dictionary<int, SaveFaction> table)
  {
    if (!factionFile.Exists)
    {
      Console.WriteLine("Faction file doesn't real");
      return false;
    }

    using TextReader factions = new StreamReader(factionFile.FullName);
    string[] items = factions.ReadToEnd().Split('|');
    int reading = int.Parse(items[3]);
    int groupID = 1;

    Console.WriteLine($"First span length: {reading}");

    for (int i = 4; i < items.Length; i++)
    {
      if (reading > 0)
      {
        //Console.WriteLine($"Reading {items[i]}");
        var data = items[i].Split('*');

        int id = int.Parse(data[0]);
        SaveFaction temp;
        table[id] = temp = new(id, data[1]);

        Console.WriteLine($"Faction: {temp.ID}/{temp.Name}");

        reading--;

        continue;
      }

      //Console.WriteLine($"Next span length: {items[i]}");
      groupID++;
      reading = int.Parse(items[i]);

      if (groupID == 2 || groupID == 4)
      {
        i += reading * 2;
        groupID++;
        //Console.WriteLine($"Skipping a group of ({reading * 2}) items");

        i++;
        reading = int.Parse(items[i]);
        //Console.WriteLine($"Next span length: {items[i]}");
      }

      if (groupID == 6)
        break;
    }

    return true;
  }
}
