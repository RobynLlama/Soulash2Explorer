using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoulashSaveUtils.Helpers;

namespace SoulashSaveUtils.Types;

public class SaveCollection
{
  public readonly DirectoryInfo SaveDir;
  public readonly Dictionary<int, SaveEntity> AllEntities = [];
  public readonly Dictionary<int, SaveFaction> AllFactions = [];
  public readonly Dictionary<int, SaveBuilding> AllBuildings = [];
  public HistorySave? WorldHistory = null;

  private const string ActorsSaveFile = "actors.sav";
  private const string HistorySaveFile = "history_events.sav";
  private const string FactionsSaveFile = "factions.sav";
  private const string BuildingsSaveFile = "buildings.sav";

  public SaveCollection(string dir)
  {
    SaveDir = new(dir);
  }

  public SaveCollection(DirectoryInfo dir)
  {
    SaveDir = dir;
  }

  public bool LoadActorsSave()
  {
    Console.WriteLine("Reading Actors");
    FileInfo actorsFile = new(Path.Combine(SaveDir.FullName, ActorsSaveFile));

    if (!ActorListing.Create(actorsFile, AllEntities))
      return false;

    var total = AllEntities.Keys.Count;
    var humanoid = AllEntities.Values.Where(x => x.IsHumanoid).Count();

    Console.WriteLine($"""
    Total Entities: {total}
      Humanoid:     {humanoid}
      Other:        {total - humanoid}
    """);

    return true;
  }

  public bool LoadHistorySave()
  {
    var historyFile = new FileInfo(Path.Combine(SaveDir.FullName, HistorySaveFile));
    var history = HistorySave.FromFile(historyFile);

    if (history is null)
    {
      Console.WriteLine("Unable to load history");
      return false;
    }

    WorldHistory = history;
    return true;
  }

  public bool LoadFactionSave()
  {
    var factionFile = new FileInfo(Path.Combine(SaveDir.FullName, FactionsSaveFile));

    if (!FactionListing.Create(factionFile, AllFactions))
      return false;

    Console.WriteLine($"""
    Total Factions:
      {AllFactions.Keys.Count}
    """);

    return true;
  }

  public bool LoadBuildingSave()
  {
    var buildingFile = new FileInfo(Path.Combine(SaveDir.FullName, BuildingsSaveFile));

    if (!BuildingListing.Create(buildingFile, AllBuildings))
      return false;

    Console.WriteLine($"""
    Total Buildings:
      {AllBuildings.Keys.Count}
    """);

    return true;
  }
}
