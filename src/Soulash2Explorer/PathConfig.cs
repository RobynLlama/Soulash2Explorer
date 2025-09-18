using System;
using Godot;

namespace Soulash2Explorer;

[GlobalClass]
public partial class PathConfig : Resource
{

  [Export]
  public string GameBasePath = string.Empty;

  [Export]
  public string GameSavesPath = string.Empty;

}
