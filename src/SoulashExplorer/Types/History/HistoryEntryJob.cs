namespace SoulashExplorer.Types;

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

  protected override string DescribeEvent(SaveCollection save)
  {
    string where;
    string fam;

    if (save.AllFactions.TryGetValue(FamilyID, out var family))
      fam = family.Name;
    else
      fam = $"Unknown ({FamilyID})";

    if (Program.LoadedData.AllDataBuildings.TryGetValue(SourceBuildingID, out var building))
      where = building.Name;
    else
      where = $"Unknown ({SourceBuildingID})";

    return $"began working a new job at {fam}'s {where}";
  }
}
