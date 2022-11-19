using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff;

public class Supervisor : EnemyStatus
{
    int mana = 0;
    int mana2 = 0;
    // states
    public override void MakeMove(PlayerStatus playerStatus)
    {
        // depending on state
        if (mana == 50) {
            mana = 0;
            Ultimate(playerStatus);
        } else if (mana2 == 6) {
            Secondary(playerStatus);
        } else {
            if (Random.Range(0f, 2f) < 1) {
                AngryATK(playerStatus);
            } else {
                SadATK(playerStatus);
            }
        }

        mana++;
        mana2++;
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

    public override void DealDamage(PlayerStatus playerStatus, float damage, SkillAttribute attribute)
    {
        base.DealDamage(playerStatus, damage, attribute);
    }
}