namespace SoulashExplorer.Types;

public class HistoryEntryMarried(int eventID, int year, int day, EventType what, int who) : HistoryEntry(eventID, year, day, what, who)
{
  public override string DescribeEvent(SaveCollection save)
  {
    //Todo: load factions into save collection and parse here
    return $"was married";
  }
}
