/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using System.Collections.Generic;
using Godot;
using SoulashSaveUtils.Types;

namespace Soulash2Explorer;

[GlobalClass]
public partial class PortraitCache : Node
{
  public static PortraitCache Instance { get; protected set; }
  private Dictionary<int, PackedScene> Cache;
  private static readonly PackedScene PortraitContainer;

  [Export]
  private PackedScene LayerProto;

  [Export]
  private Sprite2D EmptyPortrait;

  private PackedScene EmptyPortProto;

  static PortraitCache()
  {
    var d = new Control()
    {
      Name = "Portrait Container",
      AnchorRight = 1.0f,
      AnchorBottom = 1.0f,
      GrowHorizontal = Control.GrowDirection.Both,
      GrowVertical = Control.GrowDirection.Both,
      SizeFlagsHorizontal = (Control.SizeFlags)3,
      SizeFlagsVertical = (Control.SizeFlags)3,
    };

    PortraitContainer = new PackedScene();
    var err = PortraitContainer.Pack(d);

    if (err != Error.Ok)
      GD.PushError($"Error packing prefab: {err}");
  }

  public PortraitCache()
  {
    Instance ??= this;
    Cache = [];
  }

  public override void _Ready()
  {
    EmptyPortProto = new();
    var empty = PortraitContainer.Instantiate();
    RemoveChild(EmptyPortrait);
    empty.AddChild(EmptyPortrait);
    EmptyPortrait.Owner = empty;

    var err = EmptyPortProto.Pack(empty);

    if (err != Error.Ok)
      GD.PushError($"Unable to pack Empty Portrait {err}");
  }

  public PackedScene GetPortrait(SaveEntity entity)
  {
    if (!entity.IsHumanoid)
      return EmptyPortProto;

    var id = entity.EntityID;

    if (Cache.TryGetValue(id, out var cachePort))
      return cachePort;

    var cachedScene = PortraitContainer.Instantiate();

    foreach (var glyph in entity.Glyphs)
    {
      var layer = LayerProto.Instantiate<Sprite2D>();

      layer.Texture = PortraitStorage.Texture;
      layer.Modulate = new(glyph.Color.X, glyph.Color.Y, glyph.Color.Z);
      layer.RegionRect = new(glyph.Frame.XOffset, glyph.Frame.YOffset, glyph.Frame.Width, glyph.Frame.Height);

      cachedScene.AddChild(layer);
      layer.Owner = cachedScene;
    }

    //repack the scene
    var portrait = new PackedScene();
    var err = portrait.Pack(cachedScene);

    if (err != Error.Ok)
      GD.PushError($"Failed to pack portrait {err}");

    Cache[id] = portrait;

    return portrait;
  }

  public PackedScene GetPortrait(int entityID, SaveCollection save)
  {
    if (!save.AllEntities.TryGetValue(entityID, out var ent))
      return EmptyPortProto;

    return GetPortrait(ent);
  }

  public void InvalidateCache()
  {
    Cache = [];

    //Hint to the GC that now is a good time
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();
  }
}
