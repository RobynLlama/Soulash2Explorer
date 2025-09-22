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

public partial class HistoricalEventItem : PanelContainer
{
  [Export]
  public PanelContainer PortraitContainer;

  [Export]
  public Label EventActorDesc;

  [Export]
  public Label EventChronology;

  [Export]
  public Label EventDesc;

  public override void _Ready()
  {
    Visible = false;
  }

  public void Clear()
  {
    foreach (var layer in PortraitContainer.GetChildren())
      layer.QueueFree();

    Visible = false;
  }

  public void ContainEvent(HistoryEntry item, SaveCollection save)
  {
    Clear();

    var entID = item.Who;
    string actorName = $"Unknown ({entID})";
    if (save.AllEntities.TryGetValue(entID, out var entity))
      actorName = entity.GetFullName;

    EventChronology.Text = $"Year {item.Year}, Day {item.Day} (# {item.EventID})";
    EventActorDesc.Text = actorName;
    EventDesc.Text = $"{item.DescribeEvent(save)}";

    PortraitContainer.AddChild(PortraitCache.Instance.GetPortrait(entID, save).Instantiate());

    Visible = true;
  }
}
