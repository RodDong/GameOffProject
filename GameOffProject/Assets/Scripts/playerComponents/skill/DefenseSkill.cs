using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSkill : Skill
{
    private const float k = 1.0f;
    public enum DefenseSkillId {
        TEST_DEFENSE_SKILL_1,
        TEST_DEFENSE_SKILL_HAPPY,
        TEST_DEFENSE_SKILL_SAD,
        TEST_DEFENSE_SKILL_ANGRY
    }
    private DefenseSkillId id;
    private int immuneCounter;
    private int reflectCounter;
    public DefenseSkill(DefenseSkillId id) {
        this.id = id;

        switch(id) {
            case DefenseSkillId.TEST_DEFENSE_SKILL_1:
                type = SkillType.DEFENSE;
                attribute = SkillAttribute.HAPPY;
                displayName = "test defense";
                power = 50;
                immuneCounter = 1;
                break;
        }
    }

    public bool processImmune() {
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
