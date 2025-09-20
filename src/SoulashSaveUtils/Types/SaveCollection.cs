/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.Collections.Generic;
using System.IO;
using SoulashSaveUtils.Helpers;

namespace SoulashSaveUtils.Types;

public class SaveCollection
{
  public readonly DirectoryInfo SaveDir;

  /// <summary>
  /// Game year as defined in Cycle.sav
  /// </summary>
  public int CycleYear;

  /// <summary>
  /// Game day as defined in Cycle.sav
  /// </summary>
  public int CycleDay;

  public readonly Dictionary<int, SaveEntity> AllEntities = [];
  public readonly Dictionary<int, SaveFaction> AllFactions = [];
  public readonly Dictionary<int, SaveBuilding> AllBuildings = [];
  public HistorySave? WorldHistory = null;

  private const string ActorsSaveFile = "actors.sav";
  private const string CycleSaveFile = "cycle.sav";
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

    /*
    var total = AllEntities.Keys.Count;
    var humanoid = AllEntities.Values.Where(x => x.IsHumanoid).Count();

    Console.WriteLine($"""
    Total Entities: {total}
      Humanoid:     {humanoid}
      Other:        {total - humanoid}
    """);
    */

    return true;
  }

  public bool LoadCompleteSave()
  {
    if (!LoadCycleSave())
      return false;

    if (!LoadActorsSave())
      return false;

    if (!LoadFactionSave())
      return false;

    if (!LoadHistorySave())
      return false;

    return true;
  }

  public bool LoadCycleSave()
  {
    var cycleFile = new FileInfo(Path.Combine(SaveDir.FullName, CycleSaveFile));

    if (!cycleFile.Exists)
      return false;

    using StreamReader cycleText = new StreamReader(cycleFile.OpenRead());
    string[] cycleItems = cycleText.ReadToEnd().Split('|');
    //646.60004|8|0|0|0|35|50|35|50

    if (cycleItems.Length < 7)
      return false;

    if (!int.TryParse(cycleItems[5], out var years))
      return false;

    if (!int.TryParse(cycleItems[6], out var days))
      return false;

    CycleYear = years;
    CycleDay = days;
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
