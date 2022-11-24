using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Self : EnemyStatus
{
    void Awake()
    {
        MAX_HEALTH = 300;
        currentHealth = MAX_HEALTH;
    }

    // states
    public override (string, string, string) MakeMove(PlayerStatus playerStatus)
    {
        (string, string, string) curSentences = ("?","?","?");

        // depending on state
        

        return curSentences;
    }

#region Attack Move
    private (string, string, string) HappyATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Happy Attack");
        float damageAmount = 50;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        return ("HappyATK1", "HappyATK2", "HappyATK3");
    }
    private (string, string, string) SadATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Sad Attack");
        float damageAmount = 50;
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        return ("SadATK1", "SadATK2", "SadATK3");
    }
    private (string, string, string) AngryATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Angry Attack");
        float damageAmount = 50;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        return ("AngryATK1", "AngryATK2", "AngryATK3");
    }
#endregion

#region Defense Move
    
    private (string, string, string) Heal(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Heal");
        ProcessHealing(50);
        return ("Heal1", "Heal2", "Heal3");
    }

    private (string, string, string) Immune(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses immune");
        ActivateEffect(new Effect(EffectId.IMMUNE));
        return ("Immune1", "Immune2", "Immune3");
    }

    private (string, string, string) Reflect(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses reflect");
        ActivateEffect(new Effect(EffectId.REFLECT));
        return ("Reflect1", "Reflect2", "Reflect3");
    }

    
#endregion

#region Effect Move

    private (string, string, string) LifeSteal(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Lifesteal");
        ActivateEffect(new Effect(EffectId.LIFE_STEAL));
        return ("Lifesteal1", "Lifesteal2", "Lifesteal3");
    }

    private (string, string, string) Purge(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Purge");
        playerStatus.ClearEffect();
        ClearEffect();
        return ("Purge1", "Purge2", "Purge3");
    }

    private (string, string, string) BonusDamageAndBlind(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses BonusDamageAndBlind");
        Effect bounusDamage = new Effect(EffectId.BONUS_DAMAGE);
        float rand = 0.5f;
        bounusDamage.GenerateBounusDamage(playerStatus, rand);
        Effect blind = new Effect(Effect.EffectId.BLIND);
        blind.GenerateBlindPercentage(playerStatus, 1 - rand);
        playerStatus.ActivateEffect(blind);
        ActivateEffect(bounusDamage);
        return ("BonusDamageAndBlind1", "BonusDamageAndBlind2", "BonusDamageAndBlind3");
    }

#endregion

}