using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Supervisor : EnemyStatus
{
    void Awake()
    {
        MAX_HEALTH = 150;
        currentHealth = MAX_HEALTH;
    }

    int ultimateCD = 9;
    int fortifyCD = 5;

    public override void ResetCurrentHealth()
    {
        base.ResetCurrentHealth();
        ultimateCD = 9;
        fortifyCD = 5;
    }

    public override (string, string, string) MakeMove(PlayerStatus playerStatus)
    {
        (string, string, string) curSentences;

        // depending on state
        if (ultimateCD == 10) {
            ultimateCD = 0;
            curSentences = Ultimate(playerStatus);
        } else if (fortifyCD == 5) {
            fortifyCD = 0;
            curSentences = Secondary(playerStatus);
        } else {
            float playerAngryDEF = playerStatus.getDEFbyAttribute(SkillAttribute.ANGRY);
            float playerSadDEF = playerStatus.getDEFbyAttribute(SkillAttribute.SAD);
            if (playerAngryDEF < playerSadDEF) {
                curSentences = AngryATK(playerStatus);
            } else {
                curSentences = SadATK(playerStatus);
            }
        }
        ultimateCD++;
        fortifyCD++;
        return curSentences;
    }

    private (string, string, string) Ultimate(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Ultimate");
        float damageAmount = 50;
        Effect buff = new Effect(EffectId.BONUS_DAMAGE);
        buff.SetBounusDamage(50);
        ActivateEffect(buff);
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);

        return ("Ultimate1", "Ultimate2", "Ultimate3");
        
    }

    private (string, string, string) Secondary(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Fortification");
        ActivateEffect(new Effect(EffectId.FORTIFIED));
        return ("Secondary1", "Secondary2", "Secondary3");
    }

    private (string, string, string) AngryATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Angry Attack");
        float damageAmount = 30;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        Effect effect = new Effect(EffectId.WEAK);
        effect.SetDefenseReduction(30, SkillAttribute.SAD);
        playerStatus.ActivateEffect(effect);
        return ("AngryATK1", "AngryATK2", "AngryATK3");
    }

    private (string, string, string) SadATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Sad Attack");
        float damageAmount = 30;
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        Effect effect = new Effect(EffectId.WEAK);
        effect.SetDefenseReduction(30, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(effect);
        return ("SadATK1", "SadATK2", "SadATK3");
    }

}