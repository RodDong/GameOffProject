using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public class Supervisor : EnemyStatus
{
    void Awake()
    {
        MAX_HEALTH = 100;
        currentHealth = MAX_HEALTH;
        tachie = "Art/BossTachie/Boss";
        dropItems.Add(new EyeBrow(SkillAttribute.ANGRY));
        dropItems.Add(new Mouth(SkillAttribute.ANGRY));
        enemyImage = imgRoot + "Boss_Battle";
        enemySentences = ("HA? oppress the emplyees? cause that's how the society works!", "You are so naive, the higher class won't be willing to see more wages, young man.", "It's human's instinct to desire the higher power and authority!");
        playerSentences = ("Why you always oppress the emplyees?", "You use the labor of employees just to ingratiate yourself with the higher class?", "Why not try to have a win-win solution? It's not difficult");
        enemyName = "Supervisor";
    }

    int ultimateCD = 9;
    int fortifyCD = 5;

    public override void ResetCurrentHealth()
    {
        base.ResetCurrentHealth();
        ultimateCD = 9;
        fortifyCD = 5;
    }

    public override (string, string, string) MakeMove(PlayerStatus playerStatus)
    {
        (string, string, string) curSentences;

        // depending on state
        if (ultimateCD == 10) {
            ultimateCD = 0;
            curSentences = Ultimate(playerStatus);
        } else if (fortifyCD == 5) {
            fortifyCD = 0;
            curSentences = Secondary(playerStatus);
        } else {
            float playerAngryDEF = playerStatus.getDEFbyAttribute(SkillAttribute.ANGRY);
            float playerSadDEF = playerStatus.getDEFbyAttribute(SkillAttribute.SAD);
            if (playerAngryDEF < playerSadDEF) {
                curSentences = AngryATK(playerStatus);
            } else {
                curSentences = SadATK(playerStatus);
            }
        }
        ultimateCD++;
        fortifyCD++;
        return curSentences;
    }

    private (string, string, string) Ultimate(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Ultimate");
        float damageAmount = 50;
        Effect buff = new Effect(EffectId.BONUS_DAMAGE);
        buff.SetBounusDamage(20);
        ActivateEffect(buff);
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);

        return ("HA? oppress the emplyees? cause that's how the society works!", "You are so naive, the higher class won't be willing to see more wages, young man.", "It's human's instinct to desire the higher power and authority!");
        
    }

    private (string, string, string) Secondary(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Fortification");
        ActivateEffect(new Effect(EffectId.FORTIFIED));
        return ("Win-win solution? 'Oh I'm sorry' You think I would say that?", "YOU never jduge my career!", "You know nothing about this society!");
    }

    private (string, string, string) AngryATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Angry Attack");
        float damageAmount = 10;
        DealDamage(playerStatus, damageAmount, SkillAttribute.ANGRY);
        Effect effect = new Effect(EffectId.WEAK);
        effect.SetDefenseReduction(20, SkillAttribute.SAD);
        playerStatus.ActivateEffect(effect);
        return ("You know nothing about the meaning of success and the structure of this society!", "Shut up you silly RUBBISH!", "How dare you say that!");
    }

    private (string, string, string) SadATK(PlayerStatus playerStatus) {
        Debug.Log("Boss Supervisor Uses Sad Attack");
        float damageAmount = 10;
        DealDamage(playerStatus, damageAmount, SkillAttribute.SAD);
        Effect effect = new Effect(EffectId.WEAK);
        effect.SetDefenseReduction(20, SkillAttribute.ANGRY);
        playerStatus.ActivateEffect(effect);
        return ("Don't you ever compare me to those fucking rubbish!", "feel sorry when you see the employees? I have never thought about that, and I do not need to think about that ever!", "I have already chosen my way and I won't go back.");
    }

}