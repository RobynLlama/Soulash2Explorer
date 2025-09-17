using System;
using System.IO;
using SoulashExplorer.Types;

namespace SoulashExplorer;

internal class Program
{

  public static readonly DataCollection LoadedData = new();

  static void Main(string[] args)
  {
    //string saveDir = @"/home/qtmims/.steam/debian-installation/steamapps/common/Soulash 2/AppData/saves/TestWorld";
    string saveDir = @"/home/qtmims/.steam/debian-installation/steamapps/common/Soulash 2/AppData/saves/Lil World";

    Helpers.Paths.GameBasePath = @"/home/qtmims/.steam/debian-installation/steamapps/common/Soulash 2";
    string coreMod = "core_2";

    DirectoryInfo save = new(saveDir);

    if (!LoadedData.LoadAllDataFromSource(coreMod))
      return;

    if (!save.Exists)
    {
      Console.WriteLine($"""
      Save doesn't seem to exist @
        {save.FullName}
      """);
      return;

    }

    SaveCollection saveData = new(save);

    if (!saveData.LoadFactionSave())
      return;
    if (!saveData.LoadBuildingSave())
      return;
    if (!saveData.LoadActorsSave())
      return;
    if (!saveData.LoadHistorySave())
      return;

    if (saveData.WorldHistory is HistorySave history)
      foreach (var item in history.HistoricalEvents.Values)
      {
        Console.WriteLine(item.ToString(saveData));
      }
  }
}
