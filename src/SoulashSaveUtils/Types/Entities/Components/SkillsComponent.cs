using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using SoulashSaveUtils.Helpers;

namespace SoulashSaveUtils.Types;

// I don't parse list of abilities for now as I don't know if that would be helpful
// Skills;1|core_2_necromancy*50*50*0|core_2_control_undead*core_2_corpse_explosion*core_2_death_ray*core_2_necrotic_touch*core_2_raise_undead|0|
// Skills;{skillCount}|{id}*{currentLevel}*{potentialLevel}*{progress}|list of abilities|?|
public partial class SkillsComponent(
    FrozenDictionary<string, Skill> skills) : IEntityComponent
{
    public string ComponentID => "Skills";

    public FrozenDictionary<string, Skill> Skills { get; set; } = skills;

    public static SkillsComponent BuildComponent(string[] args)
    {
        if (!int.TryParse(args[0], out var skillCount))
            throw new InvalidOperationException("Unable to parse skill count");

        if (skillCount == 0)
        {
            // Entity has no skills
            return new SkillsComponent(new Dictionary<string, Skill>().ToFrozenDictionary());
        }

        var skillsDictionary = new Dictionary<string, Skill>();
        var rawSkills = args.Skip(1).Take(skillCount);

        foreach (var rawSkill in rawSkills)
        {
            var skillData = rawSkill.Split("*").ToArray();

            var id = skillData[0];

            if (!int.TryParse(skillData[1], out var currentLevel))
                throw new InvalidOperationException($"Unable to parse current skill level for skill {id}, invalid value {skillData[1]}");

            if (!int.TryParse(skillData[2], out var potential))
                throw new InvalidOperationException($"Unable to parse current skill level for skill {id}, invalid value {skillData[2]}");

            if (!int.TryParse(skillData[3], out var currentProgress))
                throw new InvalidOperationException($"Unable to parse current xp progress for skill {id}, invalid value {skillData[3]}");

            if (!_skillLevelProgressionLookup.TryGetValue(currentLevel, out var progressToNextLevel))
                throw new InvalidOperationException($"Unable to parse current xp progress to next for skill {id}, invalid value {skillData[3]}");

            string name = "Unknown";
            if (DataBase.LoadedData.AllDataSkills.TryGetValue(id, out var dataSkill))
            {
                name = dataSkill.Name;
            }

            skillsDictionary.Add(id, new Skill(id, name, currentLevel, potential, currentProgress, progressToNextLevel));
        }

        return new SkillsComponent(skillsDictionary.ToFrozenDictionary());
    }

    /// <summary>
    /// Lookup dictionary for xp values required to achieve next level
    /// </summary>
    private static readonly FrozenDictionary<int, int> _skillLevelProgressionLookup = new Dictionary<int, int>()
    {
        {0, 21},
        {1, 22},
        {2, 23},
        {3, 24},
        {4, 25},
        {5, 26},
        {6, 27},
        {7, 28},
        {8, 29},
        {9, 30},
        {10, 31},
        {11, 32},
        {12, 33},
        {13, 34},
        {14, 35},
        {15, 36},
        {16, 37},
        {17, 38},
        {18, 39},
        {19, 40},
        {20, 42},
        {21, 44},
        {22, 46},
        {23, 48},
        {24, 50},
        {25, 52},
        {26, 54},
        {27, 56},
        {28, 58},
        {29, 60},
        {30, 63},
        {31, 66},
        {32, 69},
        {33, 72},
        {34, 75},
        {35, 78},
        {36, 81},
        {37, 85},
        {38, 89},
        {39, 93},
        {40, 97},
        {41, 101},
        {42, 106},
        {43, 111},
        {44, 116},
        {45, 121},
        {46, 127},
        {47, 133},
        {48, 139},
        {49, 145},
        {50, 152}
    }.ToFrozenDictionary();
}

public record Skill(
    string Id,
    string Name,
    int CurrentLevel,
    int PotentialLevel,
    int CurrentProgress,
    int ProgressToNextLevel
);
