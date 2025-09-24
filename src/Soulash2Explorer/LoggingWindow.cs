/*
  Copyright (C) 2025 Robyn (robyn@mamallama.dev)

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published 
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
*/

using System;
using Godot;

namespace Soulash2Explorer;

[GlobalClass]
public partial class LoggingWindow : Window
{
  public static readonly string MessageColor = "D0D0D0";
  public static readonly string WarningColor = "E89D12";
  public static readonly string ErrorColor = "FF4C4C";
  public static readonly int MaxLogCount = 100;

  public static LoggingWindow Instance;

  [Export]
  public PackedScene LogEntryProto;

  [Export]
  public ScrollContainer LogEntryArea;

  [Export]
  public VBoxContainer LogEntryBox;

  [Export]
  public Button CloseButton;

  private LogEntryItem[] AllEntries;
  private int CurrentEntry = 0;

  public override void _Ready()
  {
    Instance ??= this;

    AllEntries = new LogEntryItem[MaxLogCount];

    for (int i = 0; i < MaxLogCount; i++)
    {
      LogEntryItem thing;
      AllEntries[i] = thing = LogEntryProto.Instantiate<LogEntryItem>();
      LogEntryBox.AddChild(thing);
    }

    CloseButton.Pressed += Closed;
    CloseRequested += Closed;
  }

  private void Closed() =>
    Visible = false;

  private void LogInternal(string message)
  {
    var current = AllEntries[CurrentEntry];
    CurrentEntry = (CurrentEntry + 1) % 100;

    LogEntryBox.MoveChild(current, 100);
    current.LogLabel.Text = message;
    current.Visible = true;

    LogEntryArea.GetVScrollBar().Value = LogEntryArea.GetVScrollBar().MaxValue;
  }

  public void LogMessage(object message) =>
    LogInternal($"[color={MessageColor}]MSG[/color] | {message}");

  public void LogWarning(object message) =>
    LogInternal($"[color={WarningColor}]WRN[/color] | {message}");

  public void LogError(object message) =>
    LogInternal($"[color={ErrorColor}]ERR[/color] | {message}");
}
