using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;

public class Enemy : EnemyStatus
{
    public override void makeMove(PlayerStatus playerStatus)
    {
        float effectiveDamage = 30;
        Buff blind = buffs.Find((Buff b) => { return b.GetBuffId() == Buff.BuffId.BLIND; });
        if (blind != null && Random.Range(0f, 1f) < blind.GetBlindPercentage()) {
            return; // MISS
        }
        playerStatus.TakeDamage(effectiveDamage, SkillAttribute.HAPPY);
        if (playerStatus.getActiveBuffs().Contains(new Buff(Buff.BuffId.REFLECT))) {
            TakeDamage(effectiveDamage, SkillAttribute.HAPPY);
        }
    }
}