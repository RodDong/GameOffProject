using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;

public class EnemyStatus: MonoBehaviour
{
    protected float MAX_HEALTH;
    protected float health;
    protected float happyATK;
    protected float happyDEF;
    protected float sadATK;
    protected float sadDEF;
    protected float angryATK;
    protected float angryDEF;

    public bool TakeDamage(float damage) {
        if (health <= damage) {
            health = 0;
            return true;
        } else {
            health -= damage;
            return false;
        }
    }

    public float getATKbyAttribute(SkillAttribute attribute) {
        switch(attribute) {
            case SkillAttribute.HAPPY:
                return happyATK;
            case SkillAttribute.SAD:
                return sadATK;
            case SkillAttribute.ANGRY:
                return angryATK;
            default:
                return 0.0f;
        }
    }

    public float getDEFbyAttribute(SkillAttribute attribute) {
        switch(attribute) {
            case SkillAttribute.HAPPY:
                return happyDEF;
            case SkillAttribute.SAD:
                return sadDEF;
            case SkillAttribute.ANGRY:
                return angryDEF;
            default:
                return 0.0f;
        }
    }

    void makeMove() {
        // ???????
    }
}
