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
    public override void MakeMove(PlayerStatus playerStatus) {
        if (UltimateCd == 0) {
            Ultimate(playerStatus);
            UltimateCd = 10;
            return;
        }

        if (castOrder.Count != 0)
        {
            UseSkill(castOrder.Dequeue(), playerStatus);
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

            UseSkill(castOrder.Dequeue(), playerStatus);
        }

        UltimateCd -= 1;
    }

    private void UseSkill(int skillId, PlayerStatus playerStatus) {
            switch(skillId) {
                case 0:
                    Secondary(playerStatus);
                    break;
                case 1:
                    DefReduceAttack(playerStatus);
                    break;
                case 2:
                    AtkReduceAttack(playerStatus);
                    break;
                default:
                    break;
            }
    }

#region doctor skills

    private void Ultimate(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast ultimate");
        playerStatus.ActivateEffect(new Effect(EffectId.DISMEMBERED));
        ProcessHealing(playerStatus.GetMaxHealth() / 3.0f);
    }

    private void Secondary(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast steal");
        Effect stolenEffect = new Effect(EffectId.STOLEN);
        stolenEffect.SetStolenAmount(playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) * 0.5f, SkillAttribute.ANGRY);
        stolenEffect.SetStolenAmount(playerStatus.getATKbyAttribute(SkillAttribute.SAD) * 0.5f, SkillAttribute.SAD);
        stolenEffect.SetStolenAmount(playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) * 0.5f, SkillAttribute.HAPPY);
        playerStatus.ActivateEffect(stolenEffect);
    }

    private void DefReduceAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast def reduce");
        Effect weakEffect = new Effect(EffectId.WEAK);
        weakEffect.SetDefenseReduction(20.0f, SkillAttribute.SAD);
        weakEffect.SetDefenseReduction(0.0f, SkillAttribute.HAPPY);
        weakEffect.SetDefenseReduction(0.0f, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(weakEffect);
    }

    private void AtkReduceAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast atk reduce");
        Effect reducedEffect = new Effect(EffectId.REDUCED);
        reducedEffect.SetAttackReduction(20.0f, SkillAttribute.SAD);
        reducedEffect.SetAttackReduction(0.0f, SkillAttribute.HAPPY);
        reducedEffect.SetAttackReduction(0.0f, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(reducedEffect);
        playerStatus.TakeDamage(20.0f, SkillAttribute.SAD);
    }

#endregion
}
