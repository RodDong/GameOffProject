using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public enum BuffId {
        // attack up
        TEST_BUFF_1,
        IMMUNE,
        REFLECT,
        BONUS_DAMAGE,
        LIFE_STEAL,
        PURGE,
        BLIND
    }

    private BuffId id;
    public BuffId GetBuffId() {
        return id;
    }

    private int duration;
    public void resetDuration() {
        switch(id) {
            case BuffId.TEST_BUFF_1:
                duration = 3;
                break;
            case BuffId.IMMUNE:
                duration = 1;
                break;
            case BuffId.REFLECT:
                duration = 1;
                break;
            case BuffId.BONUS_DAMAGE:
                duration = 3;
                break;
            case BuffId.LIFE_STEAL:
                duration = 3;
                break;
            case BuffId.PURGE:
                duration = 1;
                break;
            case BuffId.BLIND:
                duration = 3;
                break;
            default:
                break;
        }
    }

    public Buff(BuffId id) {
        this.id = id;
        resetDuration();
    }

    public bool decreaseCounter() {
        duration -= 1;
        return duration <= 0;
    }

    private float bounusDamageAmount;

    public void GenerateBounusDamage(PlayerStatus playerStatus, float random) {
        if (id == BuffId.BONUS_DAMAGE)
            bounusDamageAmount = playerStatus.getATKbyAttribute(Skill.SkillAttribute.ANGRY) * random;
        else 
            Debug.LogWarning("This method is not avaible for this type of buff");
    } 

    public float GetBounusDamage() {
        return bounusDamageAmount;
    }
}
