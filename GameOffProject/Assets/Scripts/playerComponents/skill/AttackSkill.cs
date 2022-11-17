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
                power = 50;
                break;
        }
    }

    // damage = random(0.95, 1.05) * (atk/def) * power * k
    // public float getAttackSkillDamage(float attackerATK, float targetDEF) {
    //     return getSkillRandom() * (attackerATK / targetDEF) * power * k;
    // }

    public float getAttackSkillDamage(PlayerStatus attacker, EnemyStatus enemy) {
        float critBalancer = 1.0f / 20.0f;

        float attributeMultiplier;
        switch (attribute) {
            case SkillAttribute.HAPPY:
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.HAPPY) / enemy.getDEFbyAttribute(SkillAttribute.HAPPY);
                return getSkillRandom() * attributeMultiplier * power * k;
            case SkillAttribute.SAD:
                // attacker.getATKbyAttribute(SkillAttribute.ANGRY) * critBalancer computes the multiplier of crit
                // attacker.getATKbyAttribute(SkillAttribute.HAPPY) > Random.Range(0.0f, 50.0f) decides if the crit happens
                float critMultiplier = 1.0f + attacker.getATKbyAttribute(SkillAttribute.ANGRY) * critBalancer 
                * ((attacker.getATKbyAttribute(SkillAttribute.HAPPY) > Random.Range(0.0f, 50.0f)) ? 1.0f : 0.0f);
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.SAD) / enemy.getDEFbyAttribute(SkillAttribute.SAD);
                return getSkillRandom() * attributeMultiplier * power * k;
            case SkillAttribute.ANGRY:
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.ANGRY) / enemy.getDEFbyAttribute(SkillAttribute.ANGRY);
                return getSkillRandom() * attributeMultiplier * power * k;
            case SkillAttribute.NONE:
                attributeMultiplier = attacker.getATKbyAttribute(SkillAttribute.ANGRY) / enemy.getDEFbyAttribute(SkillAttribute.ANGRY);
                return getSkillRandom() * attributeMultiplier * power * k;
            default:
                return 0.0f;
        }
    }
}
