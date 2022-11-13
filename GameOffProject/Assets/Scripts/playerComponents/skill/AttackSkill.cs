using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    private const float k = 1.0f;
    public enum AttackSkillId {
        TEST_ATTACK_SKILL_1,
        TEST_ATTACK_SKILL_HAPPY,
        TEST_ATTACK_SKILL_SAD,
        TEST_ATTACK_SKILL_ANGRY
    }
    private AttackSkillId id;
    public AttackSkill(AttackSkillId id) {
        this.id = id;

        switch(id) {
            case AttackSkillId.TEST_ATTACK_SKILL_1:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.ANGRY;
                displayName = "test attack";
                power = 50;
                break;
        }
    }

    // damage = random(0.95, 1.05) * (atk/def)*power*k
    public float getAttackSkillDamage(float attackerATK, float targetDEF) {
        return getSkillRandom() * (attackerATK / targetDEF) * power * k;
    }
}
