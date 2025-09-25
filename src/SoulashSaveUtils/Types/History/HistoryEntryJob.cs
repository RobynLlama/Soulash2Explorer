/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using SoulashSaveUtils.Helpers;

namespace SoulashSaveUtils.Types;

public class HistoryEntryJob(int eventID, int year, int day, EventType what, int who, int familyID, int buildingID) : HistoryEntry(eventID, year, day, what, who)
{

  /// <summary>
  /// I'm not sure why the FamilyID is stored in this event but it is
  /// </summary>
  public int FamilyID = familyID;

  /// <summary>
  /// Which core building the job is in
  /// </summary>
  public int SourceBuildingID = buildingID;

  public override string DescribeEvent(SaveCollection save)
  {
    string where;
    string fam;

    if (save.AllFamilies.TryGetValue(FamilyID, out var family))
      fam = family.Name;
    else
      fam = $"Unknown ({FamilyID})";

    if (DataBase.LoadedData.AllDataBuildings.TryGetValue(SourceBuildingID, out var building))
      where = building.Name;
    else
      where = $"Unknown ({SourceBuildingID})";

    return $"began working a new job at {fam}'s {where}";
  }
}
