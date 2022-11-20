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
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                power = 50;
                buff = new Buff(BuffId.TEST_BUFF_1);
                break;
            case SkillAttribute.ANGRY:
                type = SkillType.BUFF;
                attribute = SkillAttribute.ANGRY;
                displayName = "angry buff";
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                power = 50;
                buff = new Buff(BuffId.TEST_BUFF_1);
                break;
            case SkillAttribute.HAPPY:
                type = SkillType.BUFF;
                attribute = SkillAttribute.HAPPY;
                displayName = "angry buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_happy";
                power = 50;
                buff = new Buff(BuffId.TEST_BUFF_1);
                break;
            case SkillAttribute.SAD:
                type = SkillType.BUFF;
                attribute = SkillAttribute.HAPPY;
                displayName = "angry buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_sad";
                power = 50;
                buff = new Buff(BuffId.TEST_BUFF_1);
                break;
        }
    }

    public Buff getBuff() {
        return buff;
    }
}
