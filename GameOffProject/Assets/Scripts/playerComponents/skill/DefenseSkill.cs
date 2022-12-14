using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSkill : Skill
{
    private const float k = 0.5f;
    private int immuneCounter;
    private int reflectCounter;
    public DefenseSkill(SkillAttribute attribute) {
        this.attribute = attribute;
        
        type = SkillType.DEFENSE;

        switch(attribute) {
            case SkillAttribute.NONE:
                displayName = "Fortify";
                skillImage = imgRoot + "EmptyButton/" + "def_default";
                power = 50;
                immuneCounter = 1;
                break;
            case SkillAttribute.ANGRY:
                displayName = "Reflect";
                skillImage = imgRoot + "EmptyButton/" + "def_angry";
                power = 1;
                immuneCounter = 1;
                break;
            case SkillAttribute.SAD:
                displayName = "Immune";
                skillImage = imgRoot + "EmptyButton/" + "def_sad";
                power = 1;
                immuneCounter = 1;
                break;
            case SkillAttribute.HAPPY:
                displayName = "Heal";
                skillImage = imgRoot + "EmptyButton/" + "heal_happy";
                power = 1;
                immuneCounter = 1;
                break;
        }
    }

    public bool processImmune() {
        if (attribute != SkillAttribute.HAPPY) {
            Debug.LogWarning("This Skill Does Support this Function");
        }

        if (immuneCounter > 0) {
            immuneCounter -= 1;
            return true;
        } else {
            return false;
        }
    }

    public bool processReflect() {
        if (reflectCounter > 0) {
            reflectCounter -= 1;
            return true;
        } else {
            return false;
        }
    }

    // heal = random(0.95, 1.05) * (targetATK/100) * power * k
    public float getHealAmount(PlayerStatus playerStatus) {
        return getSkillRandom() * playerStatus.getDEFbyAttribute(SkillAttribute.HAPPY) * power * k;
    }
}
