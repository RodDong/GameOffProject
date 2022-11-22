using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Chef : EnemyStatus
{
    private int chefPhase = 0;
    private float skillRandom = 0.6f;

    private void Awake() {
        MAX_HEALTH = 150.0f;
        currentHealth = MAX_HEALTH;
    }
    public override void MakeMove(PlayerStatus playerStatus) {

        if (playerStatus.GetCurrentHealth() <= playerStatus.GetMaxHealth() * 0.7) {
            skillRandom -= 0.2f;
        } else {
            skillRandom = 0.6f;
        }

        switch (chefPhase) {
            // start
            case 0:
                ChangePhase(playerStatus);
                break;

            // appetizer
            // skill list: mix damage & taunt(angry) attack
            case 1:
                if (currentHealth <= 0.4 * MAX_HEALTH) {
                    ChangePhase(playerStatus);
                } else {
                    if (Random.Range(0.0f, 1.0f) <= skillRandom) {
                        MixDamage(playerStatus);
                    } else {
                        TauntAttack(playerStatus);
                    }
                }
                break;

            // main dish
            // skill list: mix damage & heal reduction(happy) attack
            case 2:
                if (currentHealth <= 0.5 * MAX_HEALTH) {
                    ChangePhase(playerStatus);
                } else {
                    if (Random.Range(0.0f, 1.0f) <= skillRandom) {
                        MixDamage(playerStatus);
                    } else {
                        HealReductionAttack(playerStatus);
                    }
                }
                break;

            // dessert
            // skill list: mix damage & taunt(angry) attack
            case 3:
                if (currentHealth <= 0) {
                    // process death
                    ProcessDeath(playerStatus);
                } else {
                    if (Random.Range(0.0f, 1.0f) <= skillRandom) {
                        MixDamage(playerStatus);
                    } else {
                        TauntAttack(playerStatus);
                    }
                }
                break;

            default:
                break;
        }
    }

#region skills

    private void Ultimate() {
        // chef animation/talk
        Debug.Log("Enemy cast ultlimate");
        Debug.Log("Current enemy phase: " + chefPhase);
        currentHealth = MAX_HEALTH;
    }

    private void MixDamage(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast mix damage");
        Debug.Log("Current enemy phase: " + chefPhase);
        float damageAmount = 15.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
    }

    private void HealReductionAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast heal reduction");
        Debug.Log("Current enemy phase: " + chefPhase);
        skillRandom += 0.3f;
        float damageAmount = 15.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        playerStatus.ActivateEffect(new Effect(EffectId.HEALREDUCTION));
    }

    private void TauntAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast taunt");
        Debug.Log("Current enemy phase: " + chefPhase);
        skillRandom += 0.3f;
        float damageAmount = 15.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        if (!playerStatus.GetActiveEffects().Contains(new Effect(EffectId.SILENCED))) {
            playerStatus.ActivateEffect(new Effect(EffectId.TAUNTED));
        }
    }

#endregion
    private void ProcessDeath(PlayerStatus playerStatus) {
        // TODO: move process death to battle manager
        playerStatus.ClearEffect();
    }

    private void ChangePhase(PlayerStatus playerStatus) {
        switch (chefPhase) {
            // start
            case 0:
                playerStatus.ActivateEffect(new Effect(EffectId.WATCHED));
                chefPhase = 1;
                break;
            // appetizer
            case 1:
                Ultimate();
                chefPhase = 2;
                break;
            // main dish
            case 2:
                chefPhase = 3;
                break;
            default:
                break;
        }
    }

    public int getChefPhase() {
        return chefPhase;
    }
}
