/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

public class HistoryEntryJoinedFamily(int eventID, int year, int day, EventType what, int who, int familyID) : HistoryEntry(eventID, year, day, what, who)
{
  /// <summary>
  /// Which family the actor joined
  /// </summary>
  public int FamilyID = familyID;

  public override string DescribeEvent(SaveCollection save)
  {
    string fam;

    if (save.AllFamilies.TryGetValue(FamilyID, out var family))
      fam = family.Name;
    else
      fam = $"Unknown ({FamilyID})";

    return $"Joined the {fam} Family";
  }
}
