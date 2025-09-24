/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using Soulash2Explorer;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class ActorListing
{
  private static readonly Dictionary<string, Func<string[], IEntityComponent>> ComponentLibrary = new(StringComparer.OrdinalIgnoreCase);
  public static void RegisterComponentType(string name, Func<string[], IEntityComponent> builder) =>
    ComponentLibrary[name] = builder;

  static ActorListing()
  {
    RegisterComponentType("Actor", ActorComponent.BuildComponent);
    RegisterComponentType("Collidable", CollidableComponent.BuildComponent);
    RegisterComponentType("Player", PlayerComponent.BuildComponent);
    RegisterComponentType("Name", NameComponent.BuildComponent);
  }
  public static bool Create(FileInfo actorsFile, Dictionary<int, SaveEntity> table)
  {
    if (!actorsFile.Exists)
    {
      LoggingWindow.Instance.LogError("Actors file doesn't real");
      return false;
    }

    using StreamReader actors = new(actorsFile.FullName);
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
        LoggingWindow.Instance.LogMessage($"""
            Entity: {entity.EntityID}, {entity.GetFullName}
            """);
      }
    }

    while (actors.ReadLine() is string next && !string.IsNullOrEmpty(next))
    {
      //Starting a new entity / completing the old one
      if (next == "ENTITY:")
      {
        set = false;
        buildEnt();
        ent.Reset();
        continue;
      }

      //Nothing but the EntityID shows up before a SET directive
      if (next == "SET:")
      {
        set = true;
        continue;
      }

      if (!set && next.Contains(';'))
      {
        //parse ID
        var lineParsed = next.Split(';');
        if (int.TryParse(lineParsed[0], out var id))
          ent.WithID(id);
        else
          LoggingWindow.Instance.LogWarning($"Skipping bad ID: {lineParsed[0]}");

        continue;
      }

      if (!set)
      {
        LoggingWindow.Instance.LogWarning($"Components detected before SET directive while parsing an entity");
        continue;
      }

      var componentData = next.Split(';');
      string componentName;
      string[] componentArgs = [];

      if (componentData.Length > 0)
      {
        componentName = componentData[0];
        if (componentData.Length > 1)
          componentArgs = componentData[1].Split('|');
      }
      else
      {
        LoggingWindow.Instance.LogWarning("Bad component in entity");
        continue;
      }

      if (ComponentLibrary.TryGetValue(componentName, out var builder))
      {
        var nextComp = builder(componentArgs);

        if (nextComp is NameComponent name)
          ent.WithName(name);

        ent.WithComponent(nextComp);
        continue;
      }

      //Todo: get rid of these and use components or something
      switch (componentName)
      {
        case "Humanoid":
          ent.SetHumanoid();
          continue;
        case "Glyph":
          var data = componentArgs;

          //clip the count because I don't need it
          data = data[1..];

          float GetColorFloat(string cdata)
          {
            if (!int.TryParse(cdata, out var number))
              return 1f;

            return number / 255f;
          }

          foreach (var item in data)
          {
            var gInfo = item.Split('*');
            ent.WithGlyph(
              new(gInfo[0],
              new(
                GetColorFloat(gInfo[1]),
                GetColorFloat(gInfo[2]),
                GetColorFloat(gInfo[3])
              )));
          }
          continue;
        default:
          continue;
      }
    }

    buildEnt();
    return true;
  }
}
