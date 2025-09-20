/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
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

  public event Action<int> EntityHistoryRequested;
  protected int EntityID;

  public override void _Ready()
  {
    Visible = false;
    ViewButton.Pressed += OnPressedView;
  }

  public void Clear()
  {
    foreach (var layer in PortraitContainer.GetChildren())
      layer.QueueFree();

    Desc.Text = string.Empty;
    EntityID = -1;

    Visible = false;
  }

  public void ContainEntity(SaveEntity ent)
  {
    Clear();

    foreach (var glyph in ent.Glyphs)
    {
      var layer = PortraitLayer.Instantiate<Sprite2D>();

      layer.Texture = PortraitStorage.Texture;
      layer.RegionRect = new(glyph.Frame.XOffset, glyph.Frame.YOffset, glyph.Frame.Width, glyph.Frame.Height);

      PortraitContainer.AddChild(layer);
    }

    Desc.Text = ent.GetFullName;
    EntityID = ent.EntityID;

    Visible = true;
  }

  private void OnPressedView() =>
    EntityHistoryRequested?.Invoke(EntityID);
}
