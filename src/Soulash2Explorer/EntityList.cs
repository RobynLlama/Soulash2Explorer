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
