namespace SoulashExplorer.Types;

public class HistoryEntryBorn(int eventID, int year, int day, EventType what, int who, int familyID, int locID) : HistoryEntry(eventID, year, day, what, who)
{

  public int FamilyID = familyID;
  public int LocationID = locID;

  protected override string DescribeEvent(SaveCollection save)
  {
    string fam;
    string loc;

    if (save.AllFactions.TryGetValue(FamilyID, out var family))
      fam = family.Name;
    else
      fam = $"Unknown ({FamilyID})";

    if (save.AllFactions.TryGetValue(LocationID, out var location))
      loc = location.Name;
    else
      loc = $"Unknown ({LocationID})";

    //Todo: load factions into save collection and parse here
    return $"was born into the {fam} Family in {loc}";
  }
}
