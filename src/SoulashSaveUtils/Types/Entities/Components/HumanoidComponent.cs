/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;

namespace SoulashSaveUtils.Types;

//Humanoid;10|panther|0|1|0
//Guessing: RaceID, Subrace ID, ??, Gender?, ??

public class HumanoidComponent(int race, string subrace, string unk1, int gender, string unk2) : IEntityComponent
{
  public string ComponentID => "Humanoid";

  /// <summary>
  /// The index into the Humanoid race table for this humanoid
  /// </summary>
  public int RaceID = race;

  /// <summary>
  /// Generally only used for Rasimi for "Panther" "Lion" etc
  /// </summary>
  public string SubraceID = subrace;
  public string Unknown1 = unk1;
  public HumanoidGender Gender = (HumanoidGender)gender;
  public string Unknown2 = unk2;
  public static HumanoidComponent BuildComponent(string[] args)
  {
    if (args.Length < 5)
      throw new ArgumentException("Too few arguments to parse into humanoid", nameof(args));

    if (!int.TryParse(args[0], out var race) || !int.TryParse(args[3], out var gender))
      throw new ArgumentException("Cannot parse given data into humanoid", nameof(args));

    return new HumanoidComponent(race, args[1], args[2], gender, args[4]);
  }
}

public enum HumanoidGender
{
  NA,
  Male,
  Female
}
