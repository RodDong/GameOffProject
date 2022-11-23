using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Lust : EnemyStatus
{
    float LUST_MAXHEALTH = 150.0f;
    Queue<int> castOrder = new Queue<int>();

    void Awake() 
    {
        currentHealth = LUST_MAXHEALTH;
    }

    public override (string, string, string) MakeMove(PlayerStatus playerStatus)
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
                break;
        }
        return ("", "", "");
    }

    private void Ultimate(PlayerStatus playerStatus) {
        Debug.Log("Boss Lust Uses Ultimate");
        playerStatus.ActivateEffect(new Effect(EffectId.BROKEN));
        // force items to be sad, even if the player does not own them
        playerStatus.setEquippedEyeBrow(new EyeBrow(SkillAttribute.SAD));
        playerStatus.setEquippedEyes(new Eye(SkillAttribute.SAD));
        playerStatus.setEquippedMouth(new Mouth(SkillAttribute.SAD));
        playerStatus.updateStatus();
        playerStatus.updateMask();
        // cannot change items
        // REQUIRES mute and broken to have same duration
        playerStatus.ActivateEffect(new Effect(EffectId.MUTE));
        // note that this does not switch back equipped items once effect wears off
    }

    private void Secondary(PlayerStatus playerStatus) {
        Debug.Log("Boss Lust Uses Secondary");
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        playerStatus.ActivateEffect(new Effect(EffectId.CHAOS));
    }

    private void HappyATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Lust Uses HappyATK");
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
    }

    private void SadATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Lust Uses SadATK");
        // float damageAmount = 10;
        // DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        playerStatus.ActivateEffect(new Effect(EffectId.POISON));
    }
}