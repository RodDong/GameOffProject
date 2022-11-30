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
                displayName = "Emotional Dice";
                skillImage = imgRoot + "EmptyButton/" + "atk_default";
                power = 50;
                break;
            case SkillAttribute.ANGRY:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.ANGRY;
                displayName = "Double edged attack";
                skillImage = imgRoot + "EmptyButton/" + "atk_angry";
                power = 80;
                break;
            case SkillAttribute.HAPPY:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.HAPPY;
                displayName = "Mental Break Down";
                skillImage = imgRoot + "EmptyButton/" + "atk_happy";
                power = 50;
                break;
            case SkillAttribute.SAD:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.SAD;
                displayName = "Heavy Cry";
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
                attributeMultiplier = (attacker.getATKbyAttribute(SkillAttribute.HAPPY) + 50) / 50;
                return attributeMultiplier * power * k;
            case SkillAttribute.SAD:
                // attacker.getATKbyAttribute(SkillAttribute.ANGRY) * critBalancer computes the multiplier of crit
                // attacker.getATKbyAttribute(SkillAttribute.HAPPY) > Random.Range(0.0f, 50.0f) decides if the crit happens
                float critMultiplier = 1.0f + attacker.getATKbyAttribute(SkillAttribute.ANGRY) * critBalancer 
                * ((attacker.getATKbyAttribute(SkillAttribute.HAPPY) > Random.Range(0.0f, 50.0f)) ? 1.0f : 0.0f);
                attributeMultiplier = (attacker.getATKbyAttribute(SkillAttribute.SAD) + 50) / 50;
                return attributeMultiplier * power * k;
            case SkillAttribute.ANGRY:
                attributeMultiplier = (attacker.getATKbyAttribute(SkillAttribute.ANGRY) + 50) / 50;
                return attributeMultiplier * power * k;
            case SkillAttribute.NONE:
                attributeMultiplier = (attacker.getATKbyAttribute(SkillAttribute.NONE) + 50) / 50;
                return attributeMultiplier * power * k;
            default:
                return 0.0f;
        }
    }
}
