using Godot;
using SoulashSaveUtils.Types;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;

public partial class SkillList : GridContainer
{
    [Export]
    public PackedScene SkillTagPackedScene;

    private InstancePool<SkillTag> _instancePool;

    public override void _Ready()
    {
        _instancePool = new InstancePool<SkillTag>(() => SkillTagPackedScene.Instantiate<SkillTag>());
    }

    public void UpdateSkills(FrozenDictionary<string, Skill> skills)
    {
        _instancePool.FreeAllInstances(instance =>
        {
            if (IsAncestorOf(instance))
            {
                RemoveChild(instance);
            }
        });

        if (skills == null)
        {
            return;
        }

        foreach (var skill in skills)
        {
            var instance = _instancePool.GetInstance();

            instance.UpdateSkill(skill.Value);

            AddChild(instance);
        }
    }
}
