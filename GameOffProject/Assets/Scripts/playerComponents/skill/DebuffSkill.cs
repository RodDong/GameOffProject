using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Debuff;

public class DebuffSkill : Skill
{
    public enum DebuffSkillId {
        TEST_DEBUFF_SKILL_1,
        TEST_DEBUFF_SKILL_HAPPY,
        TEST_DEBUFF_SKILL_SAD,
        TEST_DEBUFF_SKILL_ANGRY
    }
    private DebuffSkillId id;
    private Debuff debuff;
    public DebuffSkill(DebuffSkillId id) {
        this.id = id;

        switch(id) {
            case DebuffSkillId.TEST_DEBUFF_SKILL_1:
                type = SkillType.DEBUFF;
                attribute = SkillAttribute.SAD;
                displayName = "test debuff";
                power = 50;
                debuff = new Debuff(DebuffId.TEST_DEBUFF_1);
                break;
        }
    }
}
