/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Godot;
using SoulashSaveUtils;
using SoulashSaveUtils.Helpers;
using SoulashSaveUtils.Types;

namespace Soulash2Explorer;

public partial class HistoryViewer : PanelContainer
{
  [ExportCategory("Misc")]

  [Export]
  public PopupMenu Menu;

  [Export(PropertyHint.File, "*.tscn")]
  public string SaveScenePath = string.Empty;

  [ExportCategory("Tab Views")]

  [Export]
  [ExportGroup("Entity View")]
  public EntityList Listing;

  [Export]
  public ScrollContainer EntityScroller;

  [Export]
  public Button ActorBackButton;

  [Export]
  public Button ActorForwardButton;

  [Export]
  public Label PageInfoLabel;

  [Export]
  public TextEdit HistoryView;

  [Export]
  [ExportGroup("Combined History View")]
  public Label WorldHistoryLabel;

  [Export]
  public HistoryList HistoryListing;

  [Export]
  public ScrollContainer HistoryScroller;

  [Export]
  public Button HistoryBackButton;

  [Export]
  public Button HistoryForwardButton;

  [Export]
  public Label HistoryPageLabel;

  [Export]
  [ExportGroup("Meta View")]
  public Label WorldNameLabel;

  [Export]
  public Label WorldMetaLabel;

  [Export]
  public Label NoticeLabel;

  [Export]
  public Tree ModsList;

  [Export]
  public LineEdit InfoVersionField;

  protected SaveCollection save;

  protected int ActorPageNumber = 0;
  protected int MaxActorPages = 99;

  protected int HistoryPageNumber = 0;
  protected int MaxHistoryPages = 99;

  public override void _Ready()
  {
    var saveName = Paths.SelectedSave;
    var host = typeof(HistoryViewer).Assembly;
    var version = host.GetName().Version;
    var infVer = host.GetCustomAttributes<AssemblyInformationalVersionAttribute>()
    .FirstOrDefault()?.InformationalVersion ?? "Unknown Version";

    if (string.IsNullOrWhiteSpace(Paths.SelectedSave))
    {
      LoggingWindow.Instance.LogError("Paths are not configured");
      return;
    }

    LoggingWindow.Instance.LogMessage($"Loading: {saveName}");

    DataBase.LoadedData = new();
    DataBase.LoadedData.LoadAllDataFromSource("core_2");
    PortraitStorage.LoadTexture();

    save = new(Path.Combine(Paths.ConfiguredPaths.GameSavesPath, saveName));

    if (!save.LoadCompleteSave())
      return;

    if (!VersionChecker.WellKnownSaveVersions.Contains(save.GeneralSaveData.GameVersion))
      VersionMismatchDialog.Instance.Show(save.GeneralSaveData.GameVersion);

    foreach (var item in Listing.GetChildren())
    {
      if (item is EntityListItem child)
        child.EntityHistoryRequested += UpdateRequested;
    }

    Menu.IdPressed += MenuPressed;

    WorldHistoryLabel.Text = $"""
    {saveName}
      {save.WorldHistory.HistoricalEvents.Values.Count} Total Events
    """;

    WorldNameLabel.Text = save.GeneralSaveData.WorldName;

    WorldMetaLabel.Text = $"""
    Year {save.GeneralSaveData.Year}, Day {save.GeneralSaveData.Day}

    Total Entities: {save.AllEntities.Keys.Count}
    Total Events: {save.WorldHistory.HistoricalEvents.Keys.Count}
    World Seed: {save.GeneralSaveData.WorldSeed}
    Game Version: {save.GeneralSaveData.GameVersion}

    """;

    //add root item
    var root = ModsList.CreateItem();
    root.SetText(0, "Required Mods");

    foreach (var mod in save.GeneralSaveData.RequiredMods)
    {
      var item = ModsList.CreateItem(root);
      item.SetText(0, mod);
    }


    NoticeLabel.Text = $"""
    Soulash 2 Explorer {version} written by RobynLlama
    """;

    InfoVersionField.Text = $"Build: {infVer}";

    MaxActorPages = save.AllEntitiesList.Length / Listing.ItemsPerPage;
    MaxHistoryPages = save.WorldHistory.ChronologicalHistory.Length / HistoryListing.ItemsPerPage;

    ActorBackButton.Pressed += () => { ChangeActorPage(--ActorPageNumber); };
    ActorForwardButton.Pressed += () => { ChangeActorPage(++ActorPageNumber); };

    HistoryBackButton.Pressed += () => { ChangeHistoryPage(--HistoryPageNumber); };
    HistoryForwardButton.Pressed += () => { ChangeHistoryPage(++HistoryPageNumber); };

    ChangeActorPage(0);
    ChangeHistoryPage(0);
  }

  private static void UpdatePaginationForLabel(Label whichLabel, int page, int max, ScrollContainer container)
  {
    whichLabel.Text = $"Page {page + 1} / {max + 1}";
    container.GetVScrollBar().Value = 0;
  }

  private void ChangeActorPage(int newPage)
  {
    ActorPageNumber = Math.Clamp(newPage, 0, MaxActorPages);
    Listing.UpdateListFromPosition(save, ActorPageNumber * Listing.ItemsPerPage);
    UpdatePaginationForLabel(PageInfoLabel, ActorPageNumber, MaxActorPages, EntityScroller);
  }

  private void ChangeHistoryPage(int newPage)
  {
    HistoryPageNumber = Math.Clamp(newPage, 0, MaxHistoryPages);
    HistoryListing.UpdateListFromPosition(save, HistoryPageNumber * HistoryListing.ItemsPerPage);
    UpdatePaginationForLabel(HistoryPageLabel, HistoryPageNumber, MaxHistoryPages, HistoryScroller);
  }

  private void MenuPressed(long id)
  {
    switch (id)
    {
      case 0:
        GetTree().ChangeSceneToFile(SaveScenePath);
        break;
      case 2:
        GetTree().Quit();
        break;
      case 3:
        LoggingWindow.Instance.Visible = true;
        break;
      default:
        LoggingWindow.Instance.LogWarning($"Unhandled ID pressed: {id}");
        break;
    }
  }

  private void UpdateRequested(int entID)
  {
    if (save.AllEntities.Values.FirstOrDefault(x => x.EntityID == entID) is not SaveEntity entity)
      return;

    var events = save.WorldHistory.ChronologicalHistory.Where(x => x.Who == entID);
    StringBuilder hs = new();

    hs.AppendLine(entity.GetFullName);
    hs.AppendLine();

    foreach (var item in events)
    {
      hs.AppendLine($"""
      Year {item.Year}, Day {item.Day}
        {item.DescribeEvent(save)}

      """);
    }

    HistoryView.Text = hs.ToString();
  }
}
