using System;
using Godot;

namespace Soulash2Explorer;

public partial class TextEditScrollWidth : TextEdit
{
  [Export]
  public int MinScrollSize = 6;
  public override void _Ready()
  {
    GetVScrollBar().CustomMinimumSize = new(MinScrollSize, 12);
  }
}
