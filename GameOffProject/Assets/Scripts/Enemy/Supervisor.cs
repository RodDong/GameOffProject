using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff;

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
        Buff buff = new Buff(BuffId.BONUS_DAMAGE);
        buff.SetBonusDamage(10);
        ActivateBuff(buff);
        DealDamage(playerStatus, 50, SkillAttribute.ANGRY);
    }

    private void Secondary(PlayerStatus playerStatus) {
        ActivateBuff(new Buff(BuffId.FORTIFIED));
    }

    private void AngryATK(PlayerStatus playerStatus) {
        float damageAmount = 20;
        Buff bonus = buffs.Find( b => { return b.GetBuffId() == BuffId.BONUS_DAMAGE; });
        if (bonus != null) {
            damageAmount += bonus.GetBounusDamage();
        }
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        Buff buff = new Buff(BuffId.WEAK);
        buff.SetDefenseReduction(10, SkillAttribute.SAD);
        playerStatus.ActivateBuff(new Buff(BuffId.WEAK));
    }

    private void SadATK(PlayerStatus playerStatus) {
        float damageAmount = 20;
        Buff bonus = buffs.Find( b => { return b.GetBuffId() == BuffId.BONUS_DAMAGE; });
        if (bonus != null) {
            damageAmount += bonus.GetBounusDamage();
        }
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        Buff buff = new Buff(BuffId.WEAK);
        buff.SetDefenseReduction(10, SkillAttribute.ANGRY);
        playerStatus.ActivateBuff(new Buff(BuffId.WEAK));
    }

}