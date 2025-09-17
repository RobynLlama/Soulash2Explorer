namespace SoulashExplorer.Types;

public class HistoryEntryJoinedFamily(int eventID, int year, int day, EventType what, int who, int familyID) : HistoryEntry(eventID, year, day, what, who)
{
  /// <summary>
  /// Which family the actor joined
  /// </summary>
  public int FamilyID = familyID;

  protected override string DescribeEvent(SaveCollection save)
  {
    string fam;

    if (save.AllFactions.TryGetValue(FamilyID, out var family))
      fam = family.Name;
    else
      fam = $"Unknown ({FamilyID})";

    return $"Joined the {fam} Family";
  }
}
