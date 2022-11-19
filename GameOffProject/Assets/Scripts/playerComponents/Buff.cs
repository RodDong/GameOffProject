using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public enum BuffId
    {
        TEST_BUFF_1,
        IMMUNE, // Ignore all incoming damage
        REFLECT, // When taking damage, deal same damage to source
        BONUS_DAMAGE, // Attack deals additional damage
        LIFE_STEAL, // Heal the attacker based on the amount of damage dealt
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
    public BuffId GetBuffId()
    {
        return id;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return id == ((Buff)obj).id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private int duration;
    public void resetDuration()
    {
        switch (id)
        {
            case BuffId.IMMUNE: duration = 1; break;
            case BuffId.REFLECT: duration = 1; break;
            case BuffId.BONUS_DAMAGE: duration = 4; break;
            case BuffId.LIFE_STEAL: duration = 2; break;
            case BuffId.POISON: duration = 4; break;
            case BuffId.BLIND: duration = 3; break;
            case BuffId.FORTIFIED: duration = 4; break;
            case BuffId.MUTE: duration = 4; break;
            case BuffId.SILENCED: duration = 2; break;
            case BuffId.CHAOS: duration = 2; break;
            case BuffId.WATCHED: duration = 999; break;
            case BuffId.BROKEN: duration = 4; break;
            case BuffId.DISMEMBERED: duration = 6; break;
            case BuffId.REDUCED: duration = 2; break;
            case BuffId.WEAK: duration = 4; break;
            case BuffId.STOLEN: duration = 3; break;
            default: break;
        }
    }

    public Buff(BuffId id)
    {
        this.id = id;
        resetDuration();
    }

    public bool decreaseCounter()
    {
        duration -= 1;
        return duration <= 0;
    }

#region Bonus Damage
    private float bounusDamageAmount;

    public void GenerateBounusDamage(PlayerStatus playerStatus, float random)
    {
        if (id == BuffId.BONUS_DAMAGE)
            bounusDamageAmount = playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * random;
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
    }

    public float GetBounusDamage()
    {
        return bounusDamageAmount;
    }
#endregion

#region Blind
    private float blindChance;

    public void GenerateBlindPercentage(PlayerStatus playerStatus, float random)
    {
        if (id == BuffId.BLIND)
            blindChance = playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * random;
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
    }

    public float GetBlindPercentage()
    {
        return blindChance;
    }
#endregion

#region Reduced

    private Dictionary<SkillAttribute, float> reducedAmount = new Dictionary<SkillAttribute, float>();

    public void SetAttackReduction(float amount, SkillAttribute attribute){
        if (id == BuffId.REDUCED)
            reducedAmount[attribute] = amount;
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
    }

    public float GetAttackReduction(SkillAttribute attribute){
        if (id == BuffId.REDUCED)
            return reducedAmount[attribute];
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
        return 0;
    }

#endregion

#region Weak

    private Dictionary<SkillAttribute, float> weakenedAmount = new Dictionary<SkillAttribute, float>();

    public void SetDefenseReduction(float amount, SkillAttribute attribute){
        if (id == BuffId.WEAK)
            weakenedAmount[attribute] = amount;
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
    }

    public float GetDefenseReduction(SkillAttribute attribute){
         if (id == BuffId.WEAK)
            return weakenedAmount[attribute];
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
        return 0;
    }

#endregion

#region Stolen

    private Dictionary<SkillAttribute, float> stolenAmount = new Dictionary<SkillAttribute, float>();

    public void SetStolenAmount(float amount, SkillAttribute attribute){
        if (id == BuffId.STOLEN)
            weakenedAmount[attribute] = amount;
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
    }

    public float GetStolenAmount(SkillAttribute attribute){
         if (id == BuffId.STOLEN)
            return weakenedAmount[attribute];
        else
            Debug.LogWarning("This method is not avaible for this type of buff");
        return 0;
    }

#endregion

}
