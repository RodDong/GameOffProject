using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Supervisor : EnemyStatus
{
    int ultimateCD = 0;
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
        Effect buff = new Effect(EffectId.BONUS_DAMAGE);
        buff.SetBonusDamage(10);
        ActivateEffect(buff);
        DealDamage(playerStatus, 50, SkillAttribute.ANGRY);
    }

    private void Secondary(PlayerStatus playerStatus) {
        ActivateEffect(new Effect(EffectId.FORTIFIED));
    }

    private void AngryATK(PlayerStatus playerStatus) {
        float damageAmount = 20;
        Effect bonus = Effects.Find( b => { return b.GetEffectId() == EffectId.BONUS_DAMAGE; });
        if (bonus != null) {
            damageAmount += bonus.GetBounusDamage();
        }
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        Effect Effect = new Effect(EffectId.WEAK);
        Effect.SetDefenseReduction(10, SkillAttribute.SAD);
        playerStatus.ActivateEffect(new Effect(EffectId.WEAK));
    }

    private void SadATK(PlayerStatus playerStatus) {
        float damageAmount = 20;
        Effect bonus = Effects.Find( b => { return b.GetEffectId() == EffectId.BONUS_DAMAGE; });
        if (bonus != null) {
            damageAmount += bonus.GetBounusDamage();
        }
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        Effect Effect = new Effect(EffectId.WEAK);
        Effect.SetDefenseReduction(10, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(new Effect(EffectId.WEAK));
    }

}