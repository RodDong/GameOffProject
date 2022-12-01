using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Lust : EnemyStatus
{
    Queue<int> castOrder = new Queue<int>();
    void Awake() 
    {
        MAX_HEALTH = 150.0f;
        currentHealth = MAX_HEALTH;
        tachie = "Art/BossTachie/Lust";
        dropItems.Add(new EyeBrow(SkillAttribute.SAD));
        dropItems.Add(new Eye(SkillAttribute.HAPPY));
        dropItems.Add(new Mouth(SkillAttribute.HAPPY));
        enemyImage = imgRoot + "Lust_Battle";
        enemySentences = ("Lust is part of me, why are you resisting me...", "You are just one of us.", "You fear no more? What a LIE!");
    }

    public override void ResetCurrentHealth()
    {
        base.ResetCurrentHealth();
        castOrder.Clear();
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
                return HappyATK(playerStatus);
            case 1:
                return SadATK(playerStatus);
            case 2:
                return Secondary(playerStatus);
            case 3:
                return Ultimate(playerStatus);
            default:
                return ("Error", "Error", "Error");
        }
    }

    private (string, string, string) Ultimate(PlayerStatus playerStatus) {
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
        return ("You enjoy it, don't you?", "Oh, see what you made me do...", "Just surrender yourself to me.");
    }

    private (string, string, string) Secondary(PlayerStatus playerStatus) {
        Debug.Log("Boss Lust Uses Secondary");
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        playerStatus.ActivateEffect(new Effect(EffectId.CHAOS));
        return ("Lust is part of me, why are you resisting me...", "Humans are all the same. They only trap themselves in the Swamp of Desire.", "You fear no more? What a LIE!");
    }

    private (string, string, string) HappyATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Lust Uses HappyATK");
        float damageAmount = 10.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        return ("Lust is part of me, why are you resisting me...", "You are just one of us.", "You fear no more? What a LIE!");
    }

    private (string, string, string) SadATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Lust Uses SadATK");
        // float damageAmount = 10;
        // DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        Effect poisonEffect = new Effect(EffectId.POISON);
        poisonEffect.SetPoison(SkillAttribute.SAD, 5.0f);
        playerStatus.ActivateEffect(poisonEffect);
        return ("HAHA, you have nowhere to go", "You thought...and you do not know what to do? that cannot be right...", "What a mess... my plan is ruined now");
    }
}