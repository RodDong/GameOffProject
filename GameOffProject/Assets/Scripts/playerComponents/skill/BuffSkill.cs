using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : Skill
{
    public enum BuffType {
        None,
        TEST_BUFF_1
    }
    public enum BuffSkillId {
        TEST_BUFF_SKILL_1,
        TEST_BUFF_SKILL_HAPPY,
        TEST_BUFF_SKILL_SAD,
        TEST_BUFF_SKILL_ANGRY
    }
    private BuffSkillId id;
    private int duration;
    public BuffSkill(BuffSkillId id) {
        this.id = id;

        switch(id) {
            case BuffSkillId.TEST_BUFF_SKILL_1:
                type = SkillType.BUFF;
                attribute = SkillAttribute.SAD;
                displayName = "test buff";
                power = 50;
                duration = 3;
                break;
        }
    }

    // move to buff class
    // public BuffType processBuff() {
    //     if (duration > 0) {
    //         switch(id) {
    //             case BuffSkillId.TEST_BUFF_SKILL_1:
    //                 duration -= 1;
    //                 return BuffType.TEST_BUFF_1;
    //             default:
    //                 return BuffType.None;
    //         }
    //     } else {
    //         return BuffType.None;
    //     }
    // }
}
