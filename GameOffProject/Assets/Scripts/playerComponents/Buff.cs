using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public enum BuffId {
        // attack up
        TEST_BUFF_1,
        IMMUNE, // Ignore all incoming damage
        REFLECT, // When taking damage, deal same damage to source
        BONUS_DAMAGE, // Attack deals additional damage
        LIFE_STEAL, // Heal the attacker based on the amount of damage dealt
        PURGE, // to be removed
        POISON, // Deals damage per round
        BLIND, // Attack has chance to miss
        FORTIFIED, // All Defensive attributes increase
        MUTE, // Cannot change items
        SILENCED, // Cannot use skill
        CHAOS, // Spell casts on the wrong target
        WATCHED, // Playing under chef's rules
        BROKEN, // All defensive attributes become 0
        DISMEMBERED, // Max HP reduced
        REDUCED, // Reduce attack of an attribute
        WEAK, // Reduce defense of an attribute
        STOLEN // Player reduce attribute, increase enemy attribute
    }

    private BuffId id;
    public BuffId GetBuffId() {
        return id;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        return id == ((Buff)obj).id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
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
            bounusDamageAmount = playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * random;
        else 
            Debug.LogWarning("This method is not avaible for this type of buff");
    } 

    public float GetBounusDamage() {
        return bounusDamageAmount;
    }

    private float blindChance;

    public void GenerateBlindPercentage(PlayerStatus playerStatus, float random) {
        if (id == BuffId.BLIND)
            blindChance = playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * random;
        else 
            Debug.LogWarning("This method is not avaible for this type of buff");
    } 

    public float GetBlindPercentage() {
        return blindChance;
    }
}
