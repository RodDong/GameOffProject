using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class EffectSkill : Skill
{
    public EffectSkill(SkillAttribute attribute) {
        this.attribute = attribute;
        type = SkillType.EFFECT;

        switch(attribute) {
            case SkillAttribute.NONE:
                displayName = "test Effect";
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                power = 50;
                break;
            case SkillAttribute.ANGRY:
                displayName = "angry buff";
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                power = 50;
                break;
            case SkillAttribute.HAPPY:
                displayName = "happy buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_happy";
                power = 50;
                break;
            case SkillAttribute.SAD:
                displayName = "sad buff";
                skillImage = imgRoot + "EmptyButton/" + "debuff_sad";
                power = 50;
                break;
        }
    }

}
