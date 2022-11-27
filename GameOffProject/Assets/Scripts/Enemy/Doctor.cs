using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Doctor : EnemyStatus
{
    private Queue<int> castOrder = new Queue<int>();
    private int UltimateCd = 10;
    private void Awake() {
        MAX_HEALTH = 150.0f;
        currentHealth = MAX_HEALTH;
    }

    public override void ResetCurrentHealth()
    {
        base.ResetCurrentHealth();
        UltimateCd = 10;
        castOrder.Clear();
    }

    public override (string, string, string) MakeMove(PlayerStatus playerStatus) {

        (string, string, string) curSentences;

        if (currentHealth <= 0) {
            ProcessDeath();
        }

        if (UltimateCd == 0) {
            curSentences = Ultimate(playerStatus);
            UltimateCd = 10;
        }

        if (castOrder.Count != 0)
        {
            curSentences = UseSkill(castOrder.Dequeue(), playerStatus);
        } else {
            int roundsBeforeSed = Random.Range(3,4);
            bool hasReducedDef = false;
            for (int i = 0; i < roundsBeforeSed; i++) {
                if (hasReducedDef) {
                    castOrder.Enqueue(2);
                } else {
                    int randomBasicMove = Random.Range(1,2);
                    if (randomBasicMove == 1) {
                        hasReducedDef = true;
                    }
                    castOrder.Enqueue(randomBasicMove);
                }
            }
            castOrder.Enqueue(0);

            curSentences = UseSkill(castOrder.Dequeue(), playerStatus);
        }

        UltimateCd -= 1;

        return curSentences;
    }

    private (string, string, string) UseSkill(int skillId, PlayerStatus playerStatus) {
            switch(skillId) {
                case 0:
                    return Secondary(playerStatus);
                case 1:
                    return DefReduceAttack(playerStatus);
                case 2:
                    return AtkReduceAttack(playerStatus);
                default:
                    return("Sad1", "Sad2", "Sad3");
            }
    }

    private void ProcessDeath() {
        Debug.Log("boss has died");
    }

#region doctor skills

    private (string, string, string) Ultimate(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast ultimate");
        playerStatus.ActivateEffect(new Effect(EffectId.DISMEMBERED));
        ProcessHealing(playerStatus.GetMaxHealth() / 3.0f);
        return ("Ultimate1", "Ultimate2", "Ultimate3");
    }

    private (string, string, string) Secondary(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast steal");
        Effect stolenEffect = new Effect(EffectId.STOLEN);
        stolenEffect.SetStolenAmount(playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * 0.5f, SkillAttribute.ANGRY);
        stolenEffect.SetStolenAmount(playerStatus.getATKbyAttribute(SkillAttribute.SAD) * 0.5f, SkillAttribute.SAD);
        stolenEffect.SetStolenAmount(playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) * 0.5f, SkillAttribute.HAPPY);
        playerStatus.ActivateEffect(stolenEffect);
        return ("Sad1", "Sad2", "Sad3");
    }

    private (string, string, string) DefReduceAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast def reduce");
        Effect weakEffect = new Effect(EffectId.WEAK);
        weakEffect.SetDefenseReduction(20.0f, SkillAttribute.SAD);
        weakEffect.SetDefenseReduction(0.0f, SkillAttribute.HAPPY);
        weakEffect.SetDefenseReduction(0.0f, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(weakEffect);
        return ("Sad1", "Sad2", "Sad3");
    }

    private (string, string, string) AtkReduceAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast atk reduce");
        Effect reducedEffect = new Effect(EffectId.REDUCED);
        reducedEffect.SetAttackReduction(20.0f, SkillAttribute.SAD);
        reducedEffect.SetAttackReduction(0.0f, SkillAttribute.HAPPY);
        reducedEffect.SetAttackReduction(0.0f, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(reducedEffect);
        playerStatus.TakeDamage(20.0f, SkillAttribute.SAD);
        return ("Sad1", "Sad2", "Sad3");
    }

#endregion
}
