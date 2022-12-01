using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Self : EnemyStatus
{
    private int moveCycle = 0;

    void Awake()
    {
        MAX_HEALTH = 300;
        tachie = "Art/BossTachie/Self";
        currentHealth = MAX_HEALTH;
        enemyImage = imgRoot + "Self_Battle";
        enemySentences = ("Ha, you are just self-deceiving", "What have you done!", "You do not know what you are talking about!");
        enemyName = "Self";
    }

    public override void ResetCurrentHealth()
    {
        base.ResetCurrentHealth();
        moveCycle = 0;
    }

    // states
    public override (string, string, string) MakeMove(PlayerStatus playerStatus)
    {
        (string, string, string) curSentences = ("?","?","?");

        // depending on state
        switch (moveCycle) {
            case 0: case 3:
                if (currentHealth > 0.8 * MAX_HEALTH) {
                    curSentences = BonusDamageAndBlind(playerStatus);
                } else {
                    float rand = Random.Range(0f,1f);
                    curSentences = (rand < currentHealth / MAX_HEALTH) 
                    ? BonusDamageAndBlind(playerStatus) : LifeSteal(playerStatus);
                }
                break;
            case 1: case 4: 
                float playerHappyDEF = playerStatus.getDEFbyAttribute(SkillAttribute.HAPPY);
                float playerAngryDEF = playerStatus.getDEFbyAttribute(SkillAttribute.ANGRY);
                float playerSadDEF = playerStatus.getDEFbyAttribute(SkillAttribute.SAD);
                if (playerAngryDEF < playerSadDEF && playerAngryDEF < playerHappyDEF) {
                    curSentences = AngryATK(playerStatus);
                } else if (playerSadDEF < playerAngryDEF && playerSadDEF < playerHappyDEF) {
                    curSentences = SadATK(playerStatus);
                } else {
                    curSentences = HappyATK(playerStatus);
                }
                break;
            case 2: 
                if (currentHealth > 0.8 * MAX_HEALTH) {
                    curSentences = Reflect(playerStatus);
                } else {
                    float rand = Random.Range(0f,1f);
                    curSentences = (rand < currentHealth / MAX_HEALTH) 
                    ? Reflect(playerStatus) : Heal(playerStatus);
                }
                break;
            default: break;
        }
        
        moveCycle = (moveCycle + 1) % 5;

        return curSentences;
    }

#region Attack Move
    private (string, string, string) HappyATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Happy Attack");
        float damageAmount = 50;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        return ("Ha, you are just self-deceiving", "What have you done!", "You do not know what you are talking about!");
    }
    private (string, string, string) SadATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Sad Attack");
        float damageAmount = 50;
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        return ("I should not abandon my desires before", "Wait, where is my power!", "You utilize your desires to create pain");
    }
    private (string, string, string) AngryATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Angry Attack");
        float damageAmount = 50;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        return ("HAHA, like a little mouse runs in the maze.", "Oh, boy, you do not know what a life is", "You do not know what you are talking about!");
    }
#endregion

#region Defense Move
    
    private (string, string, string) Heal(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Heal");
        ProcessHealing(50);
        return ("I do not want to disappear from this world!", "I do not want to disappear from this world!", "I do not want to disappear from this world!");
    }

    private (string, string, string) Immune(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses immune");
        ActivateEffect(new Effect(EffectId.IMMUNE));
        return ("YOU CAN'T HURT ME!", "YOU CAN'T HURT ME!", "YOU CAN'T HURT ME! ");
    }

    private (string, string, string) Reflect(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses reflect");
        ActivateEffect(new Effect(EffectId.REFLECT));
        return ("HAHAHAH! YOU ARE HURTING YOURSELF", "HAHAHAH! YOU ARE HURTING YOURSELF", "HAHAHAH! YOU ARE HURTING YOURSELF");
    }

    
#endregion

#region Effect Move

    private (string, string, string) LifeSteal(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Lifesteal");
        ActivateEffect(new Effect(EffectId.LIFE_STEAL));
        return ("Looks familiar?", "Looks familiar?", "Looks familiar?");
    }

    private (string, string, string) Purge(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses Purge");
        playerStatus.ClearEffect();
        ClearEffect();
        return ("We have nothing!", "We have nothing!", "We have nothing!");
    }

    private (string, string, string) BonusDamageAndBlind(PlayerStatus playerStatus) {
        Debug.Log("Boss Self Uses BonusDamageAndBlind");
        Effect bounusDamage = new Effect(EffectId.BONUS_DAMAGE);
        float rand = 0.5f;
        bounusDamage.GenerateBounusDamage(playerStatus, rand);
        Effect blind = new Effect(Effect.EffectId.BLIND);
        blind.GenerateBlindPercentage(playerStatus, 1 - rand);
        playerStatus.ActivateEffect(blind);
        ActivateEffect(bounusDamage);
        return ("How dare you to stare at me!", "How dare you to stare at me!", "How dare you to stare at me!");
    }

#endregion

}