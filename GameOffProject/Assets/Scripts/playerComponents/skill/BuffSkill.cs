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
                power = 50;
                Effect = new Effect(EffectId.TEST_Effect_1);
                break;
        }
    }

    public Effect getEffect() {
        return Effect;
    }
}
