using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class BuffSkill : Skill
{
    private Effect Effect;
    public BuffSkill(SkillAttribute attribute) {
        this.attribute = attribute;

        switch(attribute) {
            case SkillAttribute.NONE:
                type = SkillType.EFFECT;
                attribute = SkillAttribute.SAD;
                displayName = "test Effect";
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
                displayName = "happy buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_happy";
                power = 50;
                buff = new Buff(BuffId.TEST_BUFF_1);
                break;
            case SkillAttribute.SAD:
                type = SkillType.BUFF;
                attribute = SkillAttribute.HAPPY;
                displayName = "sad buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_sad";
                power = 50;
                Effect = new Effect(EffectId.TEST_Effect_1);
                break;
        }
    }

    public Effect getEffect() {
        return Effect;
    }
}
