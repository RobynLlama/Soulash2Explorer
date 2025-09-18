using System;
using Godot;

public partial class EntityListScrollWidth : ScrollContainer
{
  [Export]
  public int MinScrollSize = 6;

  public override void _Ready()
  {
    GetVScrollBar().CustomMinimumSize = new(MinScrollSize, 12);
  }
}
