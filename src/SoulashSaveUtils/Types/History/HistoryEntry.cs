/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

#nullable enable
namespace SoulashSaveUtils.Types;

public class HistoryEntry(int eventID, int year, int day, EventType what, int who)
{
  /// <summary>
  /// The ID of the event
  /// </summary>
  public int EventID = eventID;

  /// <summary>
  /// What year the event took place in
  /// </summary>
  public int Year = year;

  /// <summary>
  /// What day the event took place on
  /// </summary>
  public int Day = day;

  /// <summary>
  /// What happened
  /// </summary>
  public EventType What = what;

  /// <summary>
  /// Entity ID of who did this thing, or who was acted on.
  /// eg: This Entity died, got married, or got a new job
  /// </summary>
  public int Who = who;

  public static HistoryEntry? FromString(string data)
  {
    string[] entries = data.Split('*');

    if (entries.Length != 16)
      return null;

    if (!int.TryParse(entries[0], out var ID))
      return null;

    if (!int.TryParse(entries[1], out var year))
      return null;

    if (!int.TryParse(entries[2], out var day))
      return null;

    if (!int.TryParse(entries[3], out var eType))
      return null;

    if (!int.TryParse(entries[4], out var ent))
      return null;

    //Used in Marriage for other entity
    if (!int.TryParse(entries[5], out var whatTarget))
      return null;

    //Used in Marriage for spouse's maiden family name
    if (!int.TryParse(entries[6], out var whichPrevFamily))
      return null;

    //Used in Born
    //Used in Joined Family and Lead Family event
    //Used in Got a Job
    if (!int.TryParse(entries[7], out var whichFamily))
      return null;

    //Used in Born as location
    if (!int.TryParse(entries[8], out var whatLocation))
      return null;

    if (!int.TryParse(entries[9], out var eventArgs5))
      return null;

    if (!int.TryParse(entries[10], out var eventArgs6))
      return null;

    //Used in Got a Job, probably Job ID
    //Null everywhere else, all other args can be negative tho ??
    if (!int.TryParse(entries[11], out var jobID))
      jobID = 0;

    if (!int.TryParse(entries[12], out var eventArgs8))
      return null;

    if (!int.TryParse(entries[13], out var eventArgs9))
      return null;

    //Used in JoinedCompany
    if (!int.TryParse(entries[14], out var whichCompany))
      return null;

    if (!int.TryParse(entries[15], out var eventArgs11))
      return null;

    var eventParsed = (EventType)eType;

    return eventParsed switch
    {
      EventType.Born => new HistoryEntryBorn(ID, year, day, eventParsed, ent, whichFamily, whatLocation),
      EventType.Died => new HistoryEntryDied(ID, year, day, eventParsed, ent),
      EventType.Married => new HistoryEntryMarried(ID, year, day, eventParsed, ent, whatTarget, whichPrevFamily),
      EventType.GotJob => new HistoryEntryJob(ID, year, day, eventParsed, ent, whichFamily, jobID),
      EventType.BecameFamilyLeader => new HistoryEntryFamilyLeader(ID, year, day, eventParsed, ent, whichFamily),
      EventType.JoinedCompany => new HistoryEntryJoinedCompany(ID, year, day, eventParsed, ent, whichCompany),
      EventType.JoinedFamily => new HistoryEntryJoinedFamily(ID, year, day, eventParsed, ent, whichFamily),
      EventType.NecrotyrantReborn => new HistoryEntryRebornTyrant(ID, year, day, eventParsed, ent),
      _ => new HistoryEntry(ID, year, day, eventParsed, ent),
    };
  }

  public virtual string ToString(SaveCollection save)
  {
    string actorName;

    if (save.AllEntities.TryGetValue(Who, out var actor))
      actorName = actor.GetFullName;
    else
      actorName = $"Unknown Actor ({Who})";

    return $"""
      Event #{EventID}:
        Year {Year}, Day {Day}
        {actorName} {DescribeEvent(save)}
      """;
  }

  public virtual string DescribeEvent(SaveCollection save) =>
    $"({What})";
}
