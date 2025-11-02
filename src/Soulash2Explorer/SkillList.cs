using Godot;
using SoulashSaveUtils.Types;
using System;
using System.Collections.Frozen;
using System.Linq;

public partial class SkillList : GridContainer
{
    [Export]
    public PackedScene SkillTagPackedScene;

    private InstancePool<SkillTag> _instancePool;

    public override void _Ready()
    {
        _instancePool = new InstancePool<SkillTag>(
            // I generate 5 instances in advance because most actors I saw didn't have more than 5 skills
            // Usually only player has a lot of skills, so potentially we could end-up with ~20 instances
            Enumerable.Range(0, 5)
                .Select((i) => SkillTagPackedScene.Instantiate<SkillTag>())
                .ToArray(),
            () => SkillTagPackedScene.Instantiate<SkillTag>());
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

        if (skills == null || skills.Count == 0)
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
