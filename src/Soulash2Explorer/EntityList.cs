/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using Godot;
using Soulash2Explorer;

namespace Soulash2Explorer;

[GlobalClass]
public partial class EntityList : VBoxContainer
{
  [Export]
  protected PackedScene EntityChild;

  public EntityListItem AddListItem(string desc, int id)
  {
    var child = EntityChild.Instantiate<EntityListItem>();

    child.Desc.Text = desc;
    child.EntityID = id;

    AddChild(child);

    return child;
  }

  public void ClearList()
  {
    foreach (var child in GetChildren())
      child.QueueFree();
  }
}
