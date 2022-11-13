using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSkill : Skill
{
    public enum DefenseSkillId {
        TEST_DEFENSE_SKILL_1,
    }
    private DefenseSkillId id;
    public DefenseSkill(DefenseSkillId id) {
        this.id = id;

        switch(id) {
            case DefenseSkillId.TEST_DEFENSE_SKILL_1:
                type = SkillType.DEFENSE;
                attribute = SkillAttribute.HAPPY;
                power = 50;
                break;
        }
    }
}
