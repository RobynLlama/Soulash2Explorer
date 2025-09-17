namespace SoulashExplorer.Types;

public class HistoryEntryFamilyLeader(int eventID, int year, int day, EventType what, int who, int familyID) : HistoryEntry(eventID, year, day, what, who)
{
  /// <summary>
  /// Which family the actor became leader of
  /// </summary>
  public int FamilyID = familyID;

  public override string DescribeEvent(SaveCollection save)
  {
    string fam;

    if (save.AllFactions.TryGetValue(FamilyID, out var family))
      fam = family.Name;
    else
      fam = $"Unknown ({FamilyID})";

    return $"Began leading the {fam} Family";
  }
}
