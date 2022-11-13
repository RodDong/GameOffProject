using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSkill : Skill
{
    public enum DebuffSkillId {
        TEST_BUFF_SKILL_1,
    }
    private DebuffSkillId id;
    public DebuffSkill(DebuffSkillId id) {
        this.id = id;

        switch(id) {
            case DebuffSkillId.TEST_BUFF_SKILL_1:
                type = SkillType.DEBUFF;
                attribute = SkillAttribute.SAD;
                power = 50;
                break;
        }
    }
}
