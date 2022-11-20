using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public enum EffectId
    {
        TEST_Effect_1,
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
        STOLEN, // Player reduce attribute, increase enemy attribute
        HEALREDUCTION, // Player reduce heal amount
        TAUNTED, // Player can only use attack skill
    }

    private EffectId id;
    public EffectId GetEffectId()
    {
        return id;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return id == ((Effect)obj).id;
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
            case EffectId.IMMUNE: duration = 1; break;
            case EffectId.REFLECT: duration = 1; break;
            case EffectId.BONUS_DAMAGE: duration = 4; break;
            case EffectId.LIFE_STEAL: duration = 2; break;
            case EffectId.POISON: duration = 4; break;
            case EffectId.BLIND: duration = 3; break;
            case EffectId.FORTIFIED: duration = 4; break;
            case EffectId.MUTE: duration = 4; break;
            case EffectId.SILENCED: duration = 2; break;
            case EffectId.CHAOS: duration = 2; break;
            case EffectId.WATCHED: duration = 999; break;
            case EffectId.BROKEN: duration = 4; break;
            case EffectId.DISMEMBERED: duration = 6; break;
            case EffectId.REDUCED: duration = 2; break;
            case EffectId.WEAK: duration = 4; break;
            case EffectId.STOLEN: duration = 3; break;
            case EffectId.HEALREDUCTION: duration = 3; break;
            case EffectId.TAUNTED: duration = 2; break;
            default: break;
        }
    }

    public Effect(EffectId id)
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
        if (id == EffectId.BONUS_DAMAGE)
            bounusDamageAmount = playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * random;
        else
            Debug.LogWarning("This method is not available for this type of Effect");
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
        if (id == EffectId.BLIND)
            blindChance = playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * random;
        else
            Debug.LogWarning("This method is not available for this type of Effect");
    }

    public float GetBlindPercentage()
    {
        return blindChance;
    }
#endregion

#region Reduced

    private Dictionary<SkillAttribute, float> reducedAmount = new Dictionary<SkillAttribute, float>();

    public void SetAttackReduction(float amount, SkillAttribute attribute){
        if (id == EffectId.REDUCED)
            reducedAmount[attribute] = amount;
        else
            Debug.LogWarning("This method is not available for this type of Effect");
    }

    public float GetAttackReduction(SkillAttribute attribute){
        if (id == EffectId.REDUCED)
            return reducedAmount[attribute];
        else
            Debug.LogWarning("This method is not available for this type of Effect");
        return 0;
    }

#endregion

#region Weak

    private Dictionary<SkillAttribute, float> weakenedAmount = new Dictionary<SkillAttribute, float>();

    public void SetDefenseReduction(float amount, SkillAttribute attribute){
        if (id == EffectId.WEAK)
            weakenedAmount[attribute] = amount;
        else
            Debug.LogWarning("This method is not available for this type of Effect");
    }

    public float GetDefenseReduction(SkillAttribute attribute){
         if (id == EffectId.WEAK)
            return weakenedAmount[attribute];
        else
            Debug.LogWarning("This method is not available for this type of Effect");
        return 0;
    }

#endregion

#region Stolen

    private Dictionary<SkillAttribute, float> stolenAmount = new Dictionary<SkillAttribute, float>();

    public void SetStolenAmount(float amount, SkillAttribute attribute){
        if (id == EffectId.STOLEN)
            stolenAmount[attribute] = amount;
        else
            Debug.LogWarning("This method is not available for this type of Effect");
    }

    public float GetStolenAmount(SkillAttribute attribute){
         if (id == EffectId.STOLEN)
            return stolenAmount[attribute];
        else
            Debug.LogWarning("This method is not available for this type of Effect");
        return 0;
    }

#endregion
}
