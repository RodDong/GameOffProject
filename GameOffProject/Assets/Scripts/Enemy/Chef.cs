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
        enemyImage = imgRoot + "Chef_Battle";
        enemySentences = ("Here comes the appetizer!", "Here comes the appetizer!", "Here comes the appetizer!");
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
                return ("Here comes the appetizer!", "What a feast!", "It's time for dessert!");

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
                return ("You are smiling. Mockery, is it? How dare you. You have yet to taste that Bouillabaisse I just added to the menu¡­with freshly grated meat from some thick, healthy thighs, of course!"
                    , "You don¡¯t seem to be happy tasting my¡­carefully prepared meat bourguignon. Don't you show that face of yours! How ungrate you are, not enjoying my food and my work? "
                    , "You questioned my choice of meat? You questioned my carefully designed menu? Why, you mad at me for cannibalism?");

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
                return ("Awww, you seem so happy, happy that you can escape from me? Happy that you are about to taste this pan-seared human belly? ",
                    "You know, I am in a good mood and don¡¯t think I can stand anyone frowning over my food. I was just about to experiment on making sausage from some freshly cut intestines!",
                    "Huh, yeah, I know I own the best restaurants in the world, with only the most exquisite ingredients that can carry out the true depth of my skill. Which is exactly why I would let anyone take it away from me. Why are you so mad at me when you can sit back and relax, waiting for me to bring you a fest? ");

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
                return ("You are smiling. Mockery, is it? How dare you. You have yet to taste that Bouillabaisse I just added to the menu¡­with freshly grated meat from some thick, healthy thighs, of course! "
                    , "You don¡¯t seem to be happy tasting my¡­carefully prepared meat bourguignon. Don't you show that face of yours! How ungrate you are, not enjoying my food and my work?"
                    , "You questioned my choice of meat? You questioned my carefully designed menu? Why, you mad at me for cannibalism?");

            default:
                return ("You are smiling. Mockery, is it? How dare you. You have yet to taste that Bouillabaisse I just added to the menu¡­with freshly grated meat from some thick, healthy thighs, of course! "
                    , "You don¡¯t seem to be happy tasting my¡­carefully prepared meat bourguignon. Don't you show that face of yours! How ungrate you are, not enjoying my food and my work?"
                    , "You questioned my choice of meat? You questioned my carefully designed menu? Why, you mad at me for cannibalism?");
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
        return ("You are smiling. Mockery, is it? How dare you. You have yet to taste that Bouillabaisse I just added to the menu¡­with freshly grated meat from some thick, healthy thighs, of course!"
                    , "You don¡¯t seem to be happy tasting my¡­carefully prepared meat bourguignon. Don't you show that face of yours! How ungrate you are, not enjoying my food and my work? "
                    , "You questioned my choice of meat? You questioned my carefully designed menu? Why, you mad at me for cannibalism?");
    }

    private (string, string, string) HealReductionAttack(PlayerStatus playerStatus) {
        Debug.Log("Enemy cast heal reduction");
        Debug.Log("Current enemy phase: " + chefPhase);
        skillRandom += 0.3f;
        float damageAmount = 15.0f;
        DealDamage(playerStatus, damageAmount, SkillAttribute.HAPPY);
        playerStatus.ActivateEffect(new Effect(EffectId.HEALREDUCTION));
        return ("Awww, you seem so happy, happy that you can escape from me? Happy that you are about to taste this pan-seared human belly? ",
                    "You know, I am in a good mood and don¡¯t think I can stand anyone frowning over my food. I was just about to experiment on making sausage from some freshly cut intestines!",
                    "Huh, yeah, I know I own the best restaurants in the world, with only the most exquisite ingredients that can carry out the true depth of my skill. Which is exactly why I would let anyone take it away from me. Why are you so mad at me when you can sit back and relax, waiting for me to bring you a fest? ");
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
        return ("You are smiling. Mockery, is it? How dare you. You have yet to taste that Bouillabaisse I just added to the menu¡­with freshly grated meat from some thick, healthy thighs, of course!"
                    , "You don¡¯t seem to be happy tasting my¡­carefully prepared meat bourguignon. Don't you show that face of yours! How ungrate you are, not enjoying my food and my work? "
                    , "You questioned my choice of meat? You questioned my carefully designed menu? Why, you mad at me for cannibalism?");
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
