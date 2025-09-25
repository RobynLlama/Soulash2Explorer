/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;

namespace SoulashSaveUtils.Types;

//Persona;739|28|773|1|793|99|2|0|1|30|8|0
public class PersonaComponent(int birthYear, int birthDay, string unk1, string unk2, int deathYear, int deathDay, int deathType, int deathEntity, string unk3, string unk4, string unk5, string unk6) : IEntityComponent
{
  public string ComponentID => "Persona";

  public int BirthYear = birthYear;
  public int BirthDay = birthDay;

  //Looks like a date but for what?
  public string Unk1 = unk1;
  public string Unk2 = unk2;


  public int DeathYear = deathYear;
  public int DeathDay = deathDay;
  public DeathType DeathType = (DeathType)deathType;

  //Pretty sure this is the ID of who killed this entity
  public int DeathEntity = deathEntity;

  public string Unk3 = unk3;
  public string Unk4 = unk4;
  public string Unk5 = unk5;
  public string Unk6 = unk6;

  public static PersonaComponent BuildComponent(string[] args)
  {
    if (args.Length < 12)
      throw new ArgumentException("Not enough args to build Persona", nameof(args));

    if (!int.TryParse(args[0], out var birthYear))
      throw new InvalidOperationException("Unable to parse birth year");
    if (!int.TryParse(args[1], out var birthDay))
      throw new InvalidOperationException("Unable to parse birth year");

    if (!int.TryParse(args[4], out var deathYear))
      throw new InvalidOperationException("Unable to parse death year");
    if (!int.TryParse(args[5], out var deathDay))
      throw new InvalidOperationException("Unable to parse death day");

    if (!int.TryParse(args[6], out var deathType))
      throw new InvalidOperationException("Unable to parse death year");
    if (!int.TryParse(args[7], out var deathEntity))
      throw new InvalidOperationException("Unable to parse death day");

    return new PersonaComponent(birthYear, birthDay, args[2], args[3], deathYear, deathDay, deathType, deathEntity, args[8], args[9], args[10], args[11]);
  }
}

public enum DeathType
{
  NaturalCauses = 2,
  Slain = 3,
}
