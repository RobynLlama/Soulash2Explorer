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
using Godot;
using SoulashSaveUtils.Helpers;

namespace Soulash2Explorer;


public partial class PathAndSaveSelector : PanelContainer
{
  [Export(PropertyHint.File, "*.tscn")]
  public string ViewerScenePath = "";

  [Export]
  [ExportCategory("Inputs")]
  public LineEdit GamePathDir;

  [Export]
  [ExportCategory("Inputs")]
  public LineEdit SavePathDir;

  [Export]
  [ExportCategory("Inputs")]
  public ItemList SaveDisplay;

  [Export]
  [ExportCategory("Buttons")]
  public Button SaveButton;

  [Export]
  [ExportCategory("Buttons")]
  public Button LoadButton;

  public override void _Ready()
  {
    //Set default paths
    GamePathDir.Text = Paths.ConfiguredPaths.GameBasePath;
    SavePathDir.Text = Paths.ConfiguredPaths.GameSavesPath;

    if (!string.IsNullOrEmpty(SavePathDir.Text))
      UpdateSelectable();

    //hook up buttons
    SaveButton.Pressed += OnPressedSave;
    LoadButton.Pressed += OnPressedLoad;
  }

  private void OnPressedLoad()
  {
    int sel = SaveDisplay.GetSelectedItems().FirstOrDefault();
    string item = SaveDisplay.GetItemText(sel);

    if (!Directory.Exists(Path.Combine(Paths.ConfiguredPaths.GameSavesPath, item)))
    {
      GD.PushError("Path does not exist! Refusing to load");
      return;
    }

    Paths.SelectedSave = item;
    GetTree().ChangeSceneToFile(ViewerScenePath);
  }

  private void OnPressedSave()
  {
    Paths.ConfiguredPaths.GameBasePath = GamePathDir.Text;
    Paths.ConfiguredPaths.GameSavesPath = SavePathDir.Text;
    Paths.SaveConfig();

    SaveDisplay.Clear();
    UpdateSelectable();
  }

  private void UpdateSelectable()
  {
    DirectoryInfo saveDir = new(Paths.ConfiguredPaths.GameSavesPath);
    foreach (var dir in saveDir.GetDirectories())
      SaveDisplay.AddItem(dir.Name);
  }
}
