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

[GlobalClass]
public partial class EntityList : VBoxContainer
{

  [Export]
  protected PackedScene ListItemProto;

  protected int ItemsPerPage = 20;
  private EntityListItem[] ListItems;

  public override void _Ready()
  {
    ListItems = new EntityListItem[ItemsPerPage];
    //Populate the list pool
    for (int i = 0; i < ItemsPerPage; i++)
    {
      var ent = ListItemProto.Instantiate<EntityListItem>();
      ListItems[i] = ent;
      AddChild(ent);
    }
  }

  public void ClearList()
  {
    foreach (var item in ListItems)
      item.Clear();
  }

  public void UpdateListFromPosition(SaveCollection save, int pos)
  {
    if (pos < 0 || pos > save.AllEntitiesList.Length)
    {
      GD.PushWarning($"Ignoring a bad pos index while paging through entities: {pos}");
      return;
    }

    var remaining = save.AllEntitiesList.Length - pos;
    var listingCount = Math.Min(ItemsPerPage, remaining);

    Span<SaveEntity> updatingEntities = new(save.AllEntitiesList, pos, listingCount);

    ClearList();

    for (int i = 0; i < listingCount; i++)
      ListItems[i].ContainEntity(updatingEntities[i]);
  }
}
