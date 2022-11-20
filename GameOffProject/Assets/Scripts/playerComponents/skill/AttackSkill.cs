using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    private const float k = 0.25f;
    public AttackSkill(SkillAttribute attribute) {
        this.attribute = attribute;

        switch(attribute) {
            case SkillAttribute.NONE:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.ANGRY;
                displayName = "test attack";
                skillImage = imgRoot + "EmptyButton/" + "atk_angry";
                power = 50;
                break;
            case SkillAttribute.ANGRY:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.ANGRY;
                displayName = "angry attack";
                skillImage = imgRoot + "EmptyButton/" + "atk_angry";
                power = 50;
                break;
            case SkillAttribute.HAPPY:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.HAPPY;
                displayName = "happy attack";
                skillImage = imgRoot + "EmptyButton/" + "atk_happy";
                power = 50;
                break;
            case SkillAttribute.SAD:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.SAD;
                displayName = "sad attack";
                skillImage = imgRoot + "EmptyButton/" + "atk_sad";
                power = 50;
                break;
        }
    }

    // damage = random(0.95, 1.05) * (atk/def) * power * k
    // public float getAttackSkillDamage(float attackerATK, float targetDEF) {
    //     return getSkillRandom() * (attackerATK / targetDEF) * power * k;
    // }

    public float getAttackSkillDamage(PlayerStatus attacker) {
        float critBalancer = 1.0f / 20.0f;

        float attributeMultiplier;
        switch (attribute) {
            case SkillAttribute.HAPPY:
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.HAPPY);
                return getSkillRandom() * attributeMultiplier * power * k;
            case SkillAttribute.SAD:
                // attacker.getATKbyAttribute(SkillAttribute.ANGRY) * critBalancer computes the multiplier of crit
                // attacker.getATKbyAttribute(SkillAttribute.HAPPY) > Random.Range(0.0f, 50.0f) decides if the crit happens
                float critMultiplier = 1.0f + attacker.getATKbyAttribute(SkillAttribute.ANGRY) * critBalancer 
                * ((attacker.getATKbyAttribute(SkillAttribute.HAPPY) > Random.Range(0.0f, 50.0f)) ? 1.0f : 0.0f);
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.SAD);
                return getSkillRandom() * attributeMultiplier * power * k;
            case SkillAttribute.ANGRY:
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.ANGRY);
                return getSkillRandom() * attributeMultiplier * power * k;
            case SkillAttribute.NONE:
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.ANGRY);
                return getSkillRandom() * attributeMultiplier * power * k;
            default:
                return 0.0f;
        }
    }
}
