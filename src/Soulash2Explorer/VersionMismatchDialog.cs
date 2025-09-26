/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using Godot;

[GlobalClass]
public partial class VersionMismatchDialog : Window
{
  [Export]
  public Button ButtonOK;

  [Export]
  public Label WarningText;

  public static VersionMismatchDialog Instance;

  public override void _Ready()
  {
    Instance ??= this;
    ButtonOK.Pressed += Hide;
  }

  public void Show(string version)
  {
    WarningText.Text = $"""
    The loaded save file is from game version {version} and has not been tested with the S2E editor.
    
    Check for updates on the Github and report any issues with save parsing
    """;

    Show();
  }
}
