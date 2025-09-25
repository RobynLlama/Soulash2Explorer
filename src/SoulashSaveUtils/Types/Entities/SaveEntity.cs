/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.Collections.Generic;

namespace SoulashSaveUtils.Types;

public class SaveEntity
{
  public int EntityID;
  public NameComponent Name;
  public bool IsHumanoid { get; protected set; }
  public Glyph[] Glyphs;
  public Dictionary<string, IEntityComponent> Components;

  public SaveEntity(int ID, Dictionary<string, IEntityComponent> components)
  {
    EntityID = ID;
    Components = components;

    IsHumanoid = Components.ContainsKey("Humanoid");

    if (!Components.TryGetValue("Glyph", out var glyph) || glyph is not GlyphComponent glyphC)
      throw new ArgumentException("Components do not contain mandatory glyph component", nameof(components));

    if (!Components.TryGetValue("Name", out var name) || name is not NameComponent nameC)
      throw new ArgumentException("Components do not contain mandatory name component", nameof(components));

    Glyphs = glyphC.Glyphs;
    Name = nameC;
  }

  public string GetFullName
  {
    get
    {
      if (IsHumanoid)
        return Name.FullActorName;

      return Name.FullEntityName;
    }
  }
}
