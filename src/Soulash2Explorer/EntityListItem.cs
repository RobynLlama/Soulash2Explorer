/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using Godot;

namespace Soulash2Explorer;

public partial class EntityListItem : PanelContainer
{
  [Export]
  public PanelContainer PortraitContainer;

  [Export]
  public Label Desc;

  [Export]
  public Button ViewButton;

  public event Action<int> EntityHistoryRequested;
  public int EntityID;

  public override void _Ready()
  {
    ViewButton.Pressed += OnPressedView;
  }

  private void OnPressedView() =>
    EntityHistoryRequested?.Invoke(EntityID);
}
