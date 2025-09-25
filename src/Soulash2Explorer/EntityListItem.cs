/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.IO;
using Godot;
using SoulashSaveUtils.Types;

namespace Soulash2Explorer;

public partial class EntityListItem : PanelContainer
{
  [Export]
  public PanelContainer PortraitContainer;

  [Export]
  public Label Desc;

  [Export]
  public Button ViewButton;

  [Export]
  public PackedScene PortraitLayer;

  [Export]
  public Sprite2D GenderIcon;

  public event Action<int> EntityHistoryRequested;
  protected int EntityID;
  protected static ImageTexture GenderTexture = new();
  protected static bool LoadedTex = false;

  public override void _Ready()
  {
    Visible = false;
    ViewButton.Pressed += OnPressedView;

    if (!LoadedTex)
    {
      string texPath = Path.Combine(SoulashSaveUtils.Helpers.Paths.DataPath, "core_2", "assets", "gfx", "portraits", "sex.png");
      Image image = new();

      LoggingWindow.Instance.LogMessage($"Loading {texPath}");
      if (image.Load(texPath) != Error.Ok)
      {
        LoggingWindow.Instance.LogError("Unable to load gender icon file");
        return;
      }

      GenderTexture.SetImage(image);
      LoadedTex = true;
    }

    GenderIcon.Texture = GenderTexture;
  }

  public void Clear()
  {
    foreach (var layer in PortraitContainer.GetChildren())
      layer.QueueFree();

    Desc.Text = string.Empty;
    EntityID = -1;

    GenderIcon.Visible = false;
    Visible = false;
  }

  public void ContainEntity(SaveEntity ent)
  {
    Clear();

    PortraitContainer.AddChild(PortraitCache.Instance.GetPortrait(ent).Instantiate());

    Desc.Text = ent.GetFullName;
    EntityID = ent.EntityID;

    if (ent.Components.TryGetValue("Humanoid", out var comp) && comp is HumanoidComponent humanoid)
    {
      switch (humanoid.Gender)
      {
        case HumanoidGender.Male:
          GenderIcon.RegionRect = new(0, 0, 15, 15);
          GenderIcon.Visible = true;
          break;
        case HumanoidGender.Female:
          GenderIcon.RegionRect = new(15, 0, 15, 15);
          GenderIcon.Visible = true;
          break;
      }
    }

    Visible = true;
  }

  private void OnPressedView() =>
    EntityHistoryRequested?.Invoke(EntityID);
}
