using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;
using Soulash2Explorer;
using SoulashSaveUtils.Types;

namespace SoulashSaveUtils.Helpers;

public static class DataSkillListing
{
    public static bool Create(string source, Dictionary<string, DataSkill> table)
    {
        if (string.IsNullOrWhiteSpace(Paths.ConfiguredPaths.GameBasePath))
        {
            LoggingWindow.Instance.LogError("Unable to read GameBasePath");
            return false;
        }

        FileInfo sourceDataSkills = new(Path.Combine(Paths.DataPath, source, "skills.json"));

        if (!sourceDataSkills.Exists)
        {
            LoggingWindow.Instance.LogError($"Unable to read skills.json in source \"{source}\"");
            return false;
        }

        var dataSkillsString = string.Empty;
        using (var streamReader = new StreamReader(File.OpenRead(sourceDataSkills.FullName)))
        {
            dataSkillsString = streamReader.ReadToEnd();
        }

        if (string.IsNullOrWhiteSpace(dataSkillsString))
        {
            LogInvalidJsonFormatError(sourceDataSkills);
            return false;
        }

        try
        {
            var dataSkillsList = JsonSerializer.Deserialize<List<DataSkill>>(dataSkillsString);

            foreach (var dataSkill in dataSkillsList)
            {
                table.Add(dataSkill.Id, dataSkill);
            }
        }
        catch (JsonException ex)
        {
            LogInvalidJsonFormatError(sourceDataSkills, ex.ToString());
            return false;
        }
        catch (Exception ex)
        {
            LoggingWindow.Instance.LogError($"Unexpected error occurred while reading {sourceDataSkills.FullName}. Reason: {ex}");
        }

        return true;
    }

    private static void LogInvalidJsonFormatError(FileInfo sourceDataSkills, string reason = null)
    {
        reason ??= string.Empty;

        LoggingWindow.Instance.LogError(
            $"Invalid format in {sourceDataSkills.FullName}. {(!string.IsNullOrWhiteSpace(reason) ? $"Reason: {reason}" : string.Empty)}"
        );
    }
}
