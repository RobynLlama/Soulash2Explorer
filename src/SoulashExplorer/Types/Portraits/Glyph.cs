using System;

namespace SoulashExplorer.Types;

public class Glyph
{
  public Frame Frame;

  public Glyph(string itemName)
  {
    //Todo: Store color info
    if (Program.LoadedData.PortraitFrames.TryGetValue(itemName, out var port))
      Frame = port.Frame;
    else
      throw new InvalidOperationException($"Missing portrait {itemName}");
  }
}
