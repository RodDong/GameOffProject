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
        tachie = "Art/BossTachie/Chef";
        dropItems.Add(new EyeBrow(SkillAttribute.HAPPY));
        dropItems.Add(new Eye(SkillAttribute.ANGRY));
    }

    public override void ResetCurrentHealth()
    {
        base.ResetCurrentHealth();
        chefPhase = 0;
    }
    public override (string, string, string) MakeMove(PlayerStatus playerStatus) {

        if (playerStatus.GetCurrentHealth() <= playerStatus.GetMaxHealth() * 0.7) {
            skillRandom -= 0.2f;
        } else {
            skillRandom = 0.6f;
        }

        switch (chefPhase) {
            // start
            case 0:
                ChangePhase(playerStatus);
                return ("Change Phase1", "Change Phase2", "Change Phase3");

            // appetizer
            // skill list: mix damage & taunt(angry) attack
            case 1:
                if (currentHealth <= 0.4 * MAX_HEALTH) {
                    ChangePhase(playerStatus);
                } else {
                    if (Random.Range(0.0f, 1.0f) <= skillRandom) {
                        return MixDamage(playerStatus);
                    } else {
                        return TauntAttack(playerStatus);
                    }
                }
                return ("Error", "Error", "Error");

            // main dish
            // skill list: mix damage & heal reduction(happy) attack
            case 2:
                if (currentHealth <= 0.5 * MAX_HEALTH) {
                    ChangePhase(playerStatus);
                } else {
                    if (Random.Range(0.0f, 1.0f) <= skillRandom) {
                       return MixDamage(playerStatus);
                    } else {
                        return HealReductionAttack(playerStatus);
                    }
                }
                return ("Error", "Error", "Error");

            // dessert
            // skill list: mix damage & taunt(angry) attack
            case 3:
                if (currentHealth <= 0) {
                    // process death
                    ProcessDeath(playerStatus);
                } else {
                    if (Random.Range(0.0f, 1.0f) <= skillRandom) {
                        return MixDamage(playerStatus);
                    } else {
                        return TauntAttack(playerStatus);
                    }
                }
                return ("Error", "Error", "Error");

            default:
                return ("Error", "Error", "Error");
        }
    }

#region skills

    private void Ultimate() {
        // chef animation/talk
        Debug.Log("Enemy cast ultlimate");
        Debug.Log("Current enemy phase: " + chefPhase);
        currentHealth = MAX_HEALTH;
    }

    private (string, string, string) MixDamage(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast mix damage");
        Debug.Log("Current enemy phase: " + chefPhase);
        float damageAmount = 15.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        return ("Mix1", "Mix2", "Mix3");
    }

    private (string, string, string) HealReductionAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast heal reduction");
        Debug.Log("Current enemy phase: " + chefPhase);
        skillRandom += 0.3f;
        float damageAmount = 15.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        playerStatus.ActivateEffect(new Effect(EffectId.HEALREDUCTION));
        return ("Happy1", "Happy2", "Happy3");
    }

    private (string, string, string) TauntAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast taunt");
        Debug.Log("Current enemy phase: " + chefPhase);
        skillRandom += 0.3f;
        float damageAmount = 15.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        if (!playerStatus.GetActiveEffects().Contains(new Effect(EffectId.SILENCED))) {
            playerStatus.ActivateEffect(new Effect(EffectId.TAUNTED));
        }
        return ("Angry1", "Angry2", "Angry3");
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
