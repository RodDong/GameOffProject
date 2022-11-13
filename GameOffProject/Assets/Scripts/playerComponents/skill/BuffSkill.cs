using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : Skill
{
    public enum BuffSkillId {
        TEST_BUFF_SKILL_1,
    }
    private BuffSkillId id;
    public BuffSkill(BuffSkillId id) {
        this.id = id;

        switch(id) {
            case BuffSkillId.TEST_BUFF_SKILL_1:
                type = SkillType.BUFF;
                attribute = SkillAttribute.SAD;
                power = 50;
                break;
        }
    }
}
