using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSkill : Skill
{
    public enum DebuffType {
        None,
        TEST_DEBUFF_1
    }
    public enum DebuffSkillId {
        TEST_DEBUFF_SKILL_1,
        TEST_DEBUFF_SKILL_HAPPY,
        TEST_DEBUFF_SKILL_SAD,
        TEST_DEBUFF_SKILL_ANGRY
    }
    private DebuffSkillId id;
    private int duration;
    public DebuffSkill(DebuffSkillId id) {
        this.id = id;

        switch(id) {
            case DebuffSkillId.TEST_DEBUFF_SKILL_1:
                type = SkillType.DEBUFF;
                attribute = SkillAttribute.SAD;
                displayName = "test debuff";
                power = 50;
                duration = 3;
                break;
        }
    }

    // move to debuff class
    // public DebuffType processDebuff() {
    //     if (duration > 0) {
    //         switch(id) {
    //             case DebuffSkillId.TEST_DEBUFF_SKILL_1:
    //                 duration -= 1;
    //                 return DebuffType.TEST_DEBUFF_1;
    //             default:
    //                 return DebuffType.None;
    //         }
    //     } else {
    //         return DebuffType.None;
    //     }
    // }
}
