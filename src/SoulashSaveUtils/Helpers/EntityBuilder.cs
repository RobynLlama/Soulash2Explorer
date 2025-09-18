using System;
using System.Collections.Generic;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public class EntityBuilder
{
  private int? ID;
  private SaveEntityName? Name;
  private bool IsHumanoid = false;
  private List<Glyph> Glyphs = [];
  public EntityBuilder WithID(int entID)
  {
    ID = entID;
    return this;
  }

  public EntityBuilder WithName(SaveEntityName name)
  {
    Name = name;
    return this;
  }

  public EntityBuilder WithName(string nameString)
  {
    Name = new(nameString);
    return this;
  }

  public EntityBuilder SetHumanoid()
  {
    IsHumanoid = true;
    return this;
  }

  public EntityBuilder WithGlyph(Glyph glyph)
  {
    Glyphs.Add(glyph);
    return this;
  }

  public EntityBuilder Reset()
  {
    ID = null;
    Name = null;
    IsHumanoid = false;
    Glyphs.Clear();
    return this;
  }

  public bool IsReady => ID is not null && Name is not null;

  public SaveEntity Build()
  {
    if (ID is not int goodID)
      throw new ArgumentNullException(nameof(ID));
    ArgumentNullException.ThrowIfNull(Name);

    return new(goodID, Name, IsHumanoid, [.. Glyphs]);
  }
}
