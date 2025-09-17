using System;
using System.Collections.Generic;
using System.IO;

namespace SoulashExplorer.Types;

public class HistorySave
{
  /// <summary>
  /// The Last ID the history save used
  /// </summary>
  public int LastHistoryID;

  /// <summary>
  /// The total number of entries in this history
  /// </summary>
  public int TotalHistoryEntries => HistoricalEvents.Keys.Count;

  /// <summary>
  /// The total number of entries in this history including the
  /// header entries. Specifically used for writing the history
  /// file
  /// </summary>
  public int TotalHistoryEntriesInFile => TotalHistoryEntries + 2;

  public readonly Dictionary<int, HistoryEntry> HistoricalEvents = [];

  public bool SafeAddHistory(HistoryEntry? entry)
  {
    if (entry is null)
      return false;

    if (HistoricalEvents.ContainsKey(entry.EventID))
      return false;

    if (entry.EventID > LastHistoryID)
      LastHistoryID = entry.EventID;

    HistoricalEvents.Add(entry.EventID, entry);
    return true;
  }

  public static HistorySave? FromFile(FileInfo historyFile)
  {
    if (!historyFile.Exists)
    {
      Console.WriteLine("Unable to read history file");
      return null;
    }

    using StreamReader reader = new(historyFile.FullName);
    string[] allHistory = reader.ReadToEnd().Split('|');

    var LastHistoryID = allHistory[0];
    var TotalEntries = allHistory[1];

    var intCount = int.Parse(TotalEntries);
    var intLastID = int.Parse(LastHistoryID);

    HistorySave hs = new();

    if (intCount == 0 || intLastID == 0)
    {
      Console.WriteLine("Unable to parse entry count or last ID");
      return hs;
    }

    hs.LastHistoryID = intLastID;

    for (int i = 2; i < intCount + 2; i++)
      hs.SafeAddHistory(HistoryEntry.FromString(allHistory[i]));

    return hs;
  }
}
