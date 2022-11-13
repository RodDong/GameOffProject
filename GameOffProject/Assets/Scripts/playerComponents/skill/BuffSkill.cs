using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff;

public class BuffSkill : Skill
{
    public enum BuffSkillId {
        TEST_BUFF_SKILL_1,
        TEST_BUFF_SKILL_HAPPY,
        TEST_BUFF_SKILL_SAD,
        TEST_BUFF_SKILL_ANGRY
    }
    private BuffSkillId id;
    private Buff buff;
    public BuffSkill(BuffSkillId id) {
        this.id = id;

        switch(id) {
            case BuffSkillId.TEST_BUFF_SKILL_1:
                type = SkillType.BUFF;
                attribute = SkillAttribute.SAD;
                displayName = "test buff";
                power = 50;
                buff = new Buff(BuffId.TEST_BUFF_1);
                break;
        }
    }

    public Buff getBuff() {
        return buff;
    }
}
