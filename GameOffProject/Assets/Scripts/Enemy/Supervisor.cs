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
    // states
    public override void MakeMove(PlayerStatus playerStatus)
    {
        // depending on state
        if (ultimateCD == 10) {
            ultimateCD = 0;
            Ultimate(playerStatus);
        } else if (fortifyCD == 5) {
            fortifyCD = 0;
            Secondary(playerStatus);
        } else {
            float playerAngryDEF = playerStatus.getDEFbyAttribute(SkillAttribute.ANGRY);
            float playerSadDEF = playerStatus.getDEFbyAttribute(SkillAttribute.SAD);
            if (playerAngryDEF < playerSadDEF) {
                AngryATK(playerStatus);
            } else {
                SadATK(playerStatus);
            }
        }
        ultimateCD++;
        fortifyCD++;
    }

    private void Ultimate(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Ultimate");
        float damageAmount = 50;
        Effect buff = new Effect(EffectId.BONUS_DAMAGE);
        buff.SetBounusDamage(50);
        ActivateEffect(buff);
        damageAmount += buff.GetBounusDamage();
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
    }

    private void Secondary(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Fortification");
        ActivateEffect(new Effect(EffectId.FORTIFIED));
    }

    private void AngryATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Angry Attack");
        float damageAmount = 30;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        Effect effect = new Effect(EffectId.WEAK);
        effect.SetDefenseReduction(30, SkillAttribute.SAD);
        playerStatus.ActivateEffect(effect);
    }

    private void SadATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Sad Attack");
        float damageAmount = 30;
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        Effect effect = new Effect(EffectId.WEAK);
        effect.SetDefenseReduction(30, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(effect);
    }

}