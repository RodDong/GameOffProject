using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackSkill;

public class Eye : Item
{
    public enum EyeId {
        TEST_EYE_1,
    }
    private EyeId id;

    public Eye(EyeId id) {
        this.id = id;

        switch(id) {
            case EyeId.TEST_EYE_1:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(AttackSkillId.TEST_ATTACK_SKILL_1);
                break;
        }
    }
}
