using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Buff;

public class Lust : EnemyStatus
{
    float LUST_MAXHEALTH = 150.0f;
    Queue<int> castOrder = new Queue<int>();

    public override void Awake() 
    {
        currentHealth = LUST_MAXHEALTH;
    }

    public override void MakeMove(PlayerStatus playerStatus)
    {
        Queue<int> castOrder = new Queue<int>();
        if (castOrder.Count == 0)
        {
            //insert random order of normal skills + 1 ult
            int num_normalATKs = Random.Range(3,6);
            for (int i = 0; i < num_normalATKs; i++)
            {
                castOrder.Enqueue(Random.Range(0, 3));
            }
            castOrder.Enqueue(3);
        } 
        int skillId = castOrder.Dequeue();
        switch (skillId)
        {
            case 0:
                HappyATK(playerStatus);
                break;
            case 1:
                SadATK(playerStatus);
                break;
            case 2:
                Secondary(playerStatus);
                break;
            case 3:
                Ultimate(playerStatus);
                break;
            default:
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