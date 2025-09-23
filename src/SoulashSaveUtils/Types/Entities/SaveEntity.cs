/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

public class SaveEntity(int ID, SaveEntityName name, bool isHumanoid, Glyph[] glyphs, IEntityComponent[] components)
{
  public int EntityID = ID;
  public SaveEntityName Name = name;
  public bool IsHumanoid = isHumanoid;
  public Glyph[] Glyphs = glyphs;
  public IEntityComponent[] Components = components;
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
