using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff;

public class Lust : EnemyStatus
{
    public override void MakeMove(PlayerStatus playerStatus)
    {
        Queue<int> castOrder = new Queue<int>();
        while (castOrder.Count != 0)
        {
            int skillId = castOrder.Dequeue();
        }
    }

    private void Ultimate(PlayerStatus playerStatus) {
        playerStatus.ActivateBuff(new Buff(BuffId.BROKEN));
        //TODO: change equipped to sad ????
    }

    private void Secondary(PlayerStatus playerStatus) {
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        //TODO: CHAOS in BattleManager.processSkill
        //playerStatus.ActivateBuff(new Buff(BuffId.CHAOS));
    }

    private void HappyATK(PlayerStatus playerStatus) {
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
    }

    private void SadATK(PlayerStatus playerStatus) {
        // float damageAmount = 10;
        // DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        playerStatus.ActivateBuff(new Buff(BuffId.POISON));
    }
}