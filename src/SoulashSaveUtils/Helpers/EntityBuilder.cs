/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.Collections.Generic;
using SoulashSaveUtils.Types;
#nullable enable

namespace SoulashSaveUtils.Helpers;

public class EntityBuilder
{
  private int? ID;
  private Dictionary<string, IEntityComponent> Components = [];
  public EntityBuilder WithID(int entID)
  {
    ID = entID;
    return this;
  }

  public EntityBuilder WithComponent(IEntityComponent component)
  {
    Components.Add(component.ComponentID, component);
    return this;
  }

  public EntityBuilder Reset()
  {
    ID = null;
    Components = [];
    return this;
  }

  public bool IsReady => ID is not null;

  public SaveEntity Build()
  {
    //Pretty sure these are top level components all entities MUST have
    if (ID is not int goodID)
      throw new ArgumentNullException(nameof(ID));

    return new(goodID, Components);
  }
}
