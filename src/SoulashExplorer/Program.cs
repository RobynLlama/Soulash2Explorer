using System;
using System.IO;
using System.Text;
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

    var outdir = new DirectoryInfo("./output");
    if (!outdir.Exists)
      outdir.Create();

    outdir.CreateSubdirectory("./entities");

    if (saveData.WorldHistory is HistorySave history)
    {

      StringBuilder entLinks = new();

      foreach (var entity in saveData.AllEntities.Values)
      {
        if (!entity.IsHumanoid)
          continue;

        string entFileName = $"./entities/{entity.EntityID}.html";
        var link = $"""
        <li><a href="{entFileName}">{entity.GetFullName}</a></li>

        """;
        entLinks.Append(link);

        StringBuilder histData = new();

        foreach (var thing in history.HistoricalEvents.Values)
        {
          if (thing.Who == entity.EntityID)
          {
            string histOutput = $"""
            <p>
              <h3>Year {thing.Year}, Day {thing.Day}</h3>
              <p>
                {entity.Name.GivenName} {thing.DescribeEvent(saveData)}
              </p>
            </p>
            """;

            histData.AppendLine(histOutput);
          }
        }

        File.WriteAllText(Path.Combine(outdir.FullName, entFileName), $"""
        <html>
          <head>
            <title>{entity.GetFullName} History</title>
          <head>
          <body>
            <h1> {entity.GetFullName} </h1>
            <h2> Historical Events </h2>
            {histData}
          </body>
        </html>
        """);
      }

      string html = $"""
      <html>
        <head>
          <title>Save Info</title>
        </head>
        <body>
          <h2>Historic Entity List</h2>
          <ul>
            {entLinks}
          </ul>
        </body>
      </html>
      """;

      File.WriteAllText(Path.Combine(outdir.FullName, "Index.html"), html);
    }
  }
}
