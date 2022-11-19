using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff;

public class EnemySupervisor : EnemyStatus
{

    public override void MakeMove(PlayerStatus playerStatus)
    {
        
    }

    private void Ultimate(PlayerStatus playerStatus) {

    }

    private void Secondary(PlayerStatus playerStatus) {

    }

    private void AngryATK(PlayerStatus playerStatus) {
        float damageAmount = 10;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        playerStatus.ActivateBuff(new Buff(BuffId.WEAK));
    }

    private void SadATK(PlayerStatus playerStatus) {
        float damageAmount = 10;
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        playerStatus.ActivateBuff(new Buff(BuffId.WEAK));
    }
}