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
  public Button PageBackButton;

  [Export]
  public Button PageForwardButton;

  [Export]
  public Label PageInfoLabel;

  [Export]
  public TextEdit HistoryView;

  [Export]
  [ExportGroup("Combined History View")]
  public Label WorldHistoryLabel;

  [Export]
  public TextEdit WorldHistoryBox;

  [Export]
  public MarginContainer HistoryTab;

  [Export]
  [ExportGroup("Meta View")]
  public Label WorldNameLabel;

  [Export]
  public Label WorldMetaLabel;

  [Export]
  public Label NoticeLabel;

  [Export]
  public LineEdit InfoVersionField;

  protected SaveCollection save;
  protected bool HistoryLoaded = false;
  protected int PageNumber = 0;
  protected int MaxPages = 99;

  public override void _Ready()
  {
    var saveName = Paths.SelectedSave;
    var host = typeof(HistoryViewer).Assembly;
    var version = host.GetName().Version;
    var infVer = host.GetCustomAttributes<AssemblyInformationalVersionAttribute>()
    .FirstOrDefault()?.InformationalVersion ?? "Unknown Version";

    if (string.IsNullOrWhiteSpace(Paths.SelectedSave))
    {
      GD.PushError("Paths are not configured");
      return;
    }

    GD.Print($"Loading: {saveName}");

    DataBase.LoadedData = new();
    DataBase.LoadedData.LoadAllDataFromSource("core_2");
    PortraitStorage.LoadTexture();

    save = new(Path.Combine(Paths.ConfiguredPaths.GameSavesPath, saveName));

    if (!save.LoadCompleteSave())
      return;

    foreach (var item in Listing.GetChildren())
    {
      if (item is EntityListItem child)
        child.EntityHistoryRequested += UpdateRequested;
    }

    Menu.IdPressed += MenuPressed;
    HistoryTab.VisibilityChanged += ReloadWorldHistory;

    WorldHistoryLabel.Text = $"""
    {saveName}
      {save.WorldHistory.HistoricalEvents.Values.Count} Total Events
    """;

    WorldNameLabel.Text = saveName;

    WorldMetaLabel.Text = $"""
    Year {save.CycleYear}, Day {save.CycleDay}

    Total Entities: {save.AllEntities.Keys.Count}
    Total Events  : {save.WorldHistory.HistoricalEvents.Keys.Count}
    """;

    NoticeLabel.Text = $"""
    Soulash 2 Explorer {version} written by RobynLlama
    """;

    InfoVersionField.Text = $"Build: {infVer}";
    MaxPages = save.AllEntitiesList.Length / Listing.ItemsPerPage;

    UpdateEntityList();

    PageBackButton.Pressed += () => { ChangePage(--PageNumber); };
    PageForwardButton.Pressed += () => { ChangePage(++PageNumber); };
  }

  private void UpdateEntityList()
  {
    Listing.UpdateListFromPosition(save, PageNumber * 20);
    PageInfoLabel.Text = $"Page {PageNumber + 1} / {MaxPages + 1}";
  }

  private void ChangePage(int newPage)
  {
    PageNumber = Math.Clamp(newPage, 0, MaxPages);
    UpdateEntityList();
  }

  private void ReloadWorldHistory()
  {
    if (HistoryLoaded)
      return;

    StringBuilder hs = new($"""
    World History for {Paths.SelectedSave}

    
    """);

    foreach (var item in save.WorldHistory.HistoricalEvents.Values)
    {
      hs.AppendLine(item.ToString(save));
      hs.AppendLine();
    }

    WorldHistoryBox.Text = hs.ToString();

    HistoryLoaded = true;
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
      default:
        GD.Print($"Unhandled ID pressed: {id}");
        break;
    }
  }

  private void UpdateRequested(int entID)
  {
    GD.Print($"Update requested by ent: {entID}");

    if (save.AllEntities.Values.FirstOrDefault(x => x.EntityID == entID) is not SaveEntity entity)
      return;

    var events = save.WorldHistory.HistoricalEvents.Values.Where(x => x.Who == entID);
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
