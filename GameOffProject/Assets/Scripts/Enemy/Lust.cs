using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Lust : EnemyStatus
{
    public override (string, string, string) MakeMove(PlayerStatus playerStatus)
    {
        Queue<int> castOrder = new Queue<int>();
        while (castOrder.Count != 0)
        {
            int skillId = castOrder.Dequeue();
        }
        return ("", "", "");
    }

    private void Ultimate(PlayerStatus playerStatus) {
        playerStatus.ActivateEffect(new Effect(EffectId.BROKEN));
        //TODO: change equipped to sad ????
    }

    private void Secondary(PlayerStatus playerStatus) {
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        //TODO: CHAOS in BattleManager.processSkill
        //playerStatus.ActivateEffect(new Effect(EffectId.CHAOS));
    }

    private void HappyATK(PlayerStatus playerStatus) {
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
    }

    private void SadATK(PlayerStatus playerStatus) {
        // float damageAmount = 10;
        // DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        playerStatus.ActivateEffect(new Effect(EffectId.POISON));
    }
}