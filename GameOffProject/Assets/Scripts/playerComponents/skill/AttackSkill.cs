using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    public enum AttackSkillId {
        TEST_ATTACK_SKILL_1,
    }
    private AttackSkillId id;
    public AttackSkill(AttackSkillId id) {
        this.id = id;

        switch(id) {
            case AttackSkillId.TEST_ATTACK_SKILL_1:
                type = SkillType.ATTACK;
                attribute = SkillAttribute.ANGRY;
                power = 50;
                break;
        }
    }
}
