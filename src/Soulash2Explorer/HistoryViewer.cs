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
using System.Text;
using Godot;
using SoulashSaveUtils.Helpers;
using SoulashSaveUtils.Types;

namespace Soulash2Explorer;

public partial class HistoryViewer : PanelContainer
{
  [Export]
  public EntityList EntList;

  [Export]
  public TextEdit HistoryView;

  [Export]
  public PopupMenu Menu;

  [Export(PropertyHint.File, "*.tscn")]
  public string SaveScenePath = string.Empty;

  [Export]
  public PackedScene PortraitLayer;

  protected SaveCollection save;

  public override void _Ready()
  {
    if (string.IsNullOrWhiteSpace(Paths.SelectedSave))
    {
      GD.PushError("Paths are not configured");
      return;
    }

    GD.Print($"Loading: {Paths.SelectedSave}");

    DataBase.LoadedData = new();
    DataBase.LoadedData.LoadAllDataFromSource("core_2");
    PortraitStorage.LoadTexture();

    save = new(Path.Combine(Paths.ConfiguredPaths.GameSavesPath, Paths.SelectedSave));

    save.LoadActorsSave();
    save.LoadFactionSave();
    save.LoadHistorySave();

    foreach (var ent in save.AllEntities.Values)
    {
      if (!ent.IsHumanoid)
        continue;

      var child = EntList.AddListItem($"({ent.EntityID}) {ent.GetFullName}", ent.EntityID);

      child.EntityHistoryRequested += UpdateRequested;

      foreach (var glyph in ent.Glyphs)
      {
        var layer = PortraitLayer.Instantiate<Sprite2D>();
        layer.Texture = PortraitStorage.Texture;
        layer.RegionRect = new(glyph.Frame.XOffset, glyph.Frame.YOffset, glyph.Frame.Width, glyph.Frame.Height);
        child.PortraitContainer.AddChild(layer);
      }

    }

    Menu.IdPressed += MenuPressed;
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
