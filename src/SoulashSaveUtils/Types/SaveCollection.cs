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
using System.Linq;
using System.Text.Json;
using Godot;
using Soulash2Explorer;
using SoulashSaveUtils.Helpers;

namespace SoulashSaveUtils.Types;

public class SaveCollection
{
  public readonly DirectoryInfo SaveDir;

  public readonly Dictionary<int, SaveEntity> AllEntities = [];
  public SaveEntity[] AllEntitiesList = [];
  public readonly Dictionary<int, SaveFaction> AllLocations = [];
  public readonly Dictionary<int, SaveFaction> AllFamilies = [];
  public readonly Dictionary<int, SaveFaction> AllStates = [];
  public readonly Dictionary<int, SaveBuilding> AllBuildings = [];
  public readonly Dictionary<int, Company> AllCompanies = [];
  public HistorySave WorldHistory = new();
  public GeneralSave GeneralSaveData = new();

#nullable enable
  public SaveEntity? PlayerEntity;
#nullable restore

  private const string ActorsSaveFile = "actors.sav";
  private const string GeneralSaveFile = "general.json";
  private const string CompanySaveFile = "companies.sav";
  private const string HistorySaveFile = "history_events.sav";
  private const string FactionsSaveFile = "factions.sav";
  private const string BuildingsSaveFile = "buildings.sav";
  private const string PlayerSaveFile = "player.sav";

  public SaveCollection(string dir)
  {
    SaveDir = new(dir);
  }

  public SaveCollection(DirectoryInfo dir)
  {
    SaveDir = dir;
  }

  public bool LoadCompleteSave()
  {
    if (!LoadGeneralSave())
      return false;

    if (!LoadActorsSave())
      return false;

    LoadPlayerSave();

    if (!LoadFactionSave())
      return false;

    if (!LoadCompanySave())
      return false;

    if (!LoadHistorySave())
      return false;

    return true;
  }

  public bool LoadCompanySave()
  {
    FileInfo companyFile = new(Path.Combine(SaveDir.FullName, CompanySaveFile));

    if (!CompanyListing.Create(companyFile, AllCompanies))
      return false;

    //Leaving room here for any processing we may need later

    return true;
  }

  public bool LoadActorsSave()
  {
    FileInfo actorsFile = new(Path.Combine(SaveDir.FullName, ActorsSaveFile));

    if (!ActorListing.Create(actorsFile, AllEntities))
      return false;

    AllEntitiesList = [.. AllEntities.Values.Where(x => x.IsHumanoid)];

    return true;
  }

  public bool LoadPlayerSave()
  {
    var playerFile = new FileInfo(Path.Combine(SaveDir.FullName, PlayerSaveFile));

    if (!playerFile.Exists)
    {
      LoggingWindow.Instance.LogMessage("Notice: No player entity exists in save");
      return false;
    }

    Dictionary<int, SaveEntity> playerData = [];
    ActorListing.Create(playerFile, playerData);
    PlayerEntity = playerData.Values.Where(x => x.IsHumanoid).FirstOrDefault();

    foreach (var item in playerData.Values)
      AllEntities[item.EntityID] = item;

    if (PlayerEntity is not null)
      AllEntitiesList = [PlayerEntity, .. AllEntitiesList];

    return true;
  }

  public bool LoadGeneralSave()
  {
    var generalFile = new FileInfo(Path.Combine(SaveDir.FullName, GeneralSaveFile));

    if (!generalFile.Exists)
    {
      LoggingWindow.Instance.LogError("General Save doesn't real");
      return false;
    }


    using StreamReader generalText = new(generalFile.OpenRead());
    var data = generalText.ReadToEnd();

    if (JsonSerializer.Deserialize<GeneralSave>(data) is not GeneralSave gs)
    {
      LoggingWindow.Instance.LogError("General Save did not deserialize");
      return false;
    }


    GeneralSaveData = gs;

    return true;
  }

  public bool LoadHistorySave()
  {
    var historyFile = new FileInfo(Path.Combine(SaveDir.FullName, HistorySaveFile));
    var history = HistorySave.FromFile(historyFile);

    if (history is null)
    {
      LoggingWindow.Instance.LogError("Unable to load history");
      return false;
    }

    WorldHistory = history;
    return true;
  }

  public bool LoadFactionSave()
  {
    var factionFile = new FileInfo(Path.Combine(SaveDir.FullName, FactionsSaveFile));

    if (!FactionListing.Create(factionFile, AllLocations, AllFamilies, AllStates))
      return false;

    LoggingWindow.Instance.LogMessage($"""
    Total Factions:
      {AllLocations.Keys.Count + AllFamilies.Keys.Count + AllStates.Keys.Count}
    """);

    return true;
  }

  public bool LoadBuildingSave()
  {
    var buildingFile = new FileInfo(Path.Combine(SaveDir.FullName, BuildingsSaveFile));

    if (!BuildingListing.Create(buildingFile, AllBuildings))
      return false;

    LoggingWindow.Instance.LogMessage($"""
    Total Buildings:
      {AllBuildings.Keys.Count}
    """);

    return true;
  }
}
