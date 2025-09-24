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

public static class FactionListing
{
  public static bool Create(FileInfo factionFile, Dictionary<int, SaveFaction> table)
  {
    if (!factionFile.Exists)
    {
      LoggingWindow.Instance.LogError("Faction file doesn't real");
      return false;
    }

    using TextReader factions = new StreamReader(factionFile.FullName);
    string[] items = factions.ReadToEnd().Split('|');
    int reading = int.Parse(items[3]);
    int groupID = 1;

    for (int i = 4; i < items.Length; i++)
    {
      if (reading > 0)
      {
        var data = items[i].Split('*');

        int id = int.Parse(data[0]);
        SaveFaction temp;
        table[id] = temp = new(id, data[1]);

        reading--;

        continue;
      }
      groupID++;
      reading = int.Parse(items[i]);

      if (groupID == 2 || groupID == 4)
      {
        i += reading * 2;
        groupID++;

        i++;
        reading = int.Parse(items[i]);
      }

      if (groupID == 6)
        break;
    }

    return true;
  }
}
