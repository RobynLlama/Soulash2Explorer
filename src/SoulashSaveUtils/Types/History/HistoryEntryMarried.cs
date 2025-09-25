/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

public class HistoryEntryMarried(int eventID, int year, int day, EventType what, int who, int target, int maidenFamily) : HistoryEntry(eventID, year, day, what, who)
{

  public int ToWho = target;
  public int MaidenFamily = maidenFamily;

  public override string DescribeEvent(SaveCollection save)
  {
    string spouseName = $"Unknown ({ToWho})";
    string spouseMaidenName = $"??? ({MaidenFamily})";

    if (save.AllEntities.TryGetValue(ToWho, out var spouse))
      spouseName = spouse.GetFullName;

    if (save.AllFamilies.TryGetValue(MaidenFamily, out var maiden))
      spouseMaidenName = maiden.Name;

    return $"was wed to {spouseName} (former family name: {spouseMaidenName})";
  }
}
