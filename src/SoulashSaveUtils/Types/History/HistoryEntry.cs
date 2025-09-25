/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

#nullable enable
namespace SoulashSaveUtils.Types;

//206575*800*78*1*607509*0*-1*1344*24*-1*-1**-1*-1*0*-1
//472*35*50*2*1144*0*-1*-1*-1*-1*-1**-1*-1*0*-1
//182996*715*60*9*238851*0*0*881*-1*-1*-1**-1*-1*0*-1

//124*27*19*3*9*31*3*1*-1*-1*-1**-1*-1*0*-1
//Event 124, on 27/19 marriage, Who (9), Who (31)?, ? (3), ? (1) => blank
//Guessing: EventID, int Year, int Day, int EventType, EntityID who, 6 event args, always null, 4 event args

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

    //Used in Marriage for other entity, maybe?
    if (!int.TryParse(entries[5], out var eventArgs1))
      return null;

    if (!int.TryParse(entries[6], out var eventArgs2))
      return null;

    //Used in Born for faction ID
    //Used in Joined Family and Lead Family event as a faction ID
    //Used in Got a Job for some reason
    if (!int.TryParse(entries[7], out var whichFaction))
      return null;

    //Used in Born for faction ID of permanent location (city/town)
    if (!int.TryParse(entries[8], out var whatLocation))
      return null;

    if (!int.TryParse(entries[9], out var eventArgs5))
      return null;

    if (!int.TryParse(entries[10], out var eventArgs6))
      return null;

    //Used in Got a Job, probably Job ID
    //Null everywhere else, all other args can be negative tho ??
    if (!int.TryParse(entries[11], out var JobID))
      JobID = 0;

    if (!int.TryParse(entries[12], out var eventArgs8))
      return null;

    if (!int.TryParse(entries[13], out var eventArgs9))
      return null;

    if (!int.TryParse(entries[14], out var eventArgs10))
      return null;

    if (!int.TryParse(entries[15], out var eventArgs11))
      return null;

    var eventParsed = (EventType)eType;

    return eventParsed switch
    {
      EventType.Born => new HistoryEntryBorn(ID, year, day, eventParsed, ent, whichFaction, whatLocation),
      EventType.Died => new HistoryEntryDied(ID, year, day, eventParsed, ent),
      EventType.Married => new HistoryEntryMarried(ID, year, day, eventParsed, ent, eventArgs1),
      EventType.GotJob => new HistoryEntryJob(ID, year, day, eventParsed, ent, whichFaction, JobID),
      EventType.BecameFamilyLeader => new HistoryEntryFamilyLeader(ID, year, day, eventParsed, ent, whichFaction),
      EventType.JoinedFamily => new HistoryEntryJoinedFamily(ID, year, day, eventParsed, ent, whichFaction),
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
