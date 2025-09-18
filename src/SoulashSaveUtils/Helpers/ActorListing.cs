using System;
using System.Collections.Generic;
using System.IO;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class ActorListing
{
  public static bool Create(FileInfo actorsFile, Dictionary<int, SaveEntity> table)
  {
    if (!actorsFile.Exists)
    {
      Console.WriteLine("Actors file doesn't real");
      return false;
    }

    using TextReader actors = new StreamReader(actorsFile.FullName);
    bool set = false;
    EntityBuilder ent = new();

    void buildEnt()
    {
      if (!ent.IsReady)
        return;

      var entity = ent.Build();
      table[entity.EntityID] = entity;
      if (entity.IsHumanoid)
      {
        Console.WriteLine($"""
            Entity: {entity.EntityID}, {entity.GetFullName}
            """);
      }
    }

    while (actors.ReadLine() is string next && !string.IsNullOrEmpty(next))
    {
      if (next == "ENTITY:")
      {
        set = false;
        buildEnt();
        ent.Reset();
        continue;
      }

      if (next == "SET:")
      {
        set = true;
        continue;
      }

      if (next.StartsWith("Name;"))
      {
        ent.WithName(next[5..]);
        continue;
      }

      if (next.StartsWith("Humanoid;"))
      {
        ent.SetHumanoid();
        continue;
      }

      if (next.StartsWith("Glyph;"))
      {
        var data = next[6..].Split('|');

        //clip the count because I don't need it
        data = data[1..];

        foreach (var item in data)
        {
          var gInfo = item.Split('*');
          ent.WithGlyph(new(gInfo[0]));
        }
      }

      if (!set && next.Contains(';'))
      {
        //parse ID
        var lineParsed = next.Split(';');
        if (int.TryParse(lineParsed[0], out var id))
          ent.WithID(id);
        else
          Console.WriteLine($"Skipping bad ID: {lineParsed[0]}");
      }
    }

    buildEnt();
    return true;
  }
}
