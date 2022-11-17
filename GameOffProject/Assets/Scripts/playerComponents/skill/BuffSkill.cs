using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff;

public class BuffSkill : Skill
{
    private Buff buff;
    public BuffSkill(SkillAttribute attribute) {
        this.attribute = attribute;

        switch(attribute) {
            case SkillAttribute.NONE:
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
