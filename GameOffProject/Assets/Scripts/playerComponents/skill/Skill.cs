using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackSkill;
public class Skill
{
    public enum SkillType {
        ATTACK,
        DEFENSE,
        EFFECT
    }
   
    protected SkillType type;
    protected SkillAttribute attribute;
    protected float power;
    protected string displayName;

    // skill utilities
    protected float getSkillRandom() {
        return Random.Range(0.95f, 1.05f);
    }

    public SkillType getSkillType() {
        return type;
    }

    public string getDisplayName() {
        return displayName;
    }

    public SkillAttribute GetSkillAttribute() {
        return attribute;
    }
}