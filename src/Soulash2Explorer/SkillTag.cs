using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using Godot;
using SoulashSaveUtils.Types;

public partial class SkillTag : PanelContainer
{
    private const string UNKNOWN_COLOR_KEY = "UNKNOWN_COLOR";

    [Export]
    public Label SkillNameLabel;

    public void UpdateSkill(Skill skill)
    {
        SkillNameLabel.Text = $"{skill.Name} {skill.CurrentLevel}/{skill.PotentialLevel}";

        if (!_skillColorLookup.TryGetValue(skill.Id, out var baseColorHex))
        {
            baseColorHex = _skillColorLookup[UNKNOWN_COLOR_KEY];
        }

        var baseColor = new Color(baseColorHex);
        var progressColor = GetProgressColor(baseColor);

        if (Material is not ShaderMaterial material)
        {
            return;
        }

        float progress = skill.CurrentProgress == 0 || skill.ProgressToNextLevel == 0
            ? 0
            : skill.CurrentProgress / (float)skill.ProgressToNextLevel * 100;

        material.SetShaderParameter("background_color", baseColor);
        material.SetShaderParameter("progress_color", progressColor);
        material.SetShaderParameter("progress", progress);
    }

    // Get progress color by making base color slightly darker
    private static Color GetProgressColor(Color baseColor)
    {
        return new Color(
            baseColor with
            {
                R = baseColor.R * 0.7f,
                G = baseColor.G * 0.7f,
                B = baseColor.B * 0.7f,
            }
        );
    }

    private static readonly FrozenDictionary<string, string> _skillColorLookup
        = new Dictionary<string, string>
        {
            { UNKNOWN_COLOR_KEY, "#464645ff"},
            { "core_2_adventuring", "#c69a6b" },
            { "core_2_agriculture", "#8bb356" },
            { "core_2_alchemy", "#b36ca3" },
            { "core_2_armorsmith", "#9c8c7a" },
            { "core_2_athletics", "#5b8da9" },
            { "core_2_axe_fighting", "#b25e4a" },
            { "core_2_divine_fists", "#e0b15a" },
            { "core_2_carpentry", "#c49c6f" },
            { "core_2_construction", "#a48f77" },
            { "core_2_cryomancy", "#6cb3d9" },
            { "core_2_floromancy", "#8ec86c" },
            { "core_2_hunting", "#6e9b5e" },
            { "core_2_imbuing", "#7a7cd4" },
            { "core_2_leadership", "#d7a45f" },
            { "core_2_leatherworking", "#a47856" },
            { "core_2_mace_fighting", "#8b5b49" },
            { "core_2_necromancy", "#7c6aa8" },
            { "core_2_pole_fighting", "#4a8a70" },
            { "core_2_protection", "#5fa9c1" },
            { "core_2_pyromancy", "#e05c3c" },
            { "core_2_stormthrowing", "#4d9cd6" },
            { "core_2_sword_fighting", "#b46a5f" },
            { "core_2_tailoring", "#d89c63" },
            { "core_2_thievery", "#b0a342" },
            { "core_2_weaponsmith", "#c47e55" },
        }
        .ToFrozenDictionary();

}
