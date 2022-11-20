using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class BuffSkill : Skill
{
    private Effect effect;
    public BuffSkill(SkillAttribute attribute) {
        this.attribute = attribute;

        switch(attribute) {
            case SkillAttribute.NONE:
                type = SkillType.EFFECT;
                attribute = SkillAttribute.SAD;
                displayName = "test Effect";
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                power = 50;
                effect = new Effect(EffectId.TEST_EFFECT_1);
                break;
            case SkillAttribute.ANGRY:
                type = SkillType.EFFECT;
                attribute = SkillAttribute.ANGRY;
                displayName = "angry buff";
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                power = 50;
                effect = new Effect(EffectId.TEST_EFFECT_1);
                break;
            case SkillAttribute.HAPPY:
                type = SkillType.EFFECT;
                attribute = SkillAttribute.HAPPY;
                displayName = "happy buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_happy";
                power = 50;
                effect = new Effect(EffectId.TEST_EFFECT_1);
                break;
            case SkillAttribute.SAD:
                type = SkillType.EFFECT;
                attribute = SkillAttribute.HAPPY;
                displayName = "sad buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_sad";
                power = 50;
                effect = new Effect(EffectId.TEST_EFFECT_1);
                break;
        }
    }

    public Effect getEffect() {
        return effect;
    }
}
