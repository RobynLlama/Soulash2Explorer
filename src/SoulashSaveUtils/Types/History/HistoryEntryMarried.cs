/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

public class HistoryEntryMarried(int eventID, int year, int day, EventType what, int who, int target) : HistoryEntry(eventID, year, day, what, who)
{

  public int ToWho = target;

  public override string DescribeEvent(SaveCollection save)
  {
    string spouseName = $"Unknown ({ToWho})";

    if (save.AllEntities.TryGetValue(ToWho, out var spouse))
      spouseName = spouse.GetFullName;

    return $"was wed to {spouseName}";
  }
}
