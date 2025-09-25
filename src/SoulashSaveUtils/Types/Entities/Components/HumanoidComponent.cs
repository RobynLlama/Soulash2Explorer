/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

namespace SoulashSaveUtils.Types;

public class HumanoidComponent : IEntityComponent
{
  public string ComponentID => "Humanoid";
  public static IEntityComponent BuildComponent(string[] _) => new GenericComponent("Humanoid");
}
