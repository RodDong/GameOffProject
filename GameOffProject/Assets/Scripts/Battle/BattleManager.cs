using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Skill;
using static Buff;

public class BattleManager : MonoBehaviour
{
    enum State
    {
        Preparation,
        Battle,
        Death,
        Win
    }

    //public GameObject enemy;
    GameObject player;

    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject gamObjectsInScene;
    [SerializeField] GameObject eyebrowUI;
    [SerializeField] GameObject eyeUI;
    [SerializeField] GameObject mouthUI;
    [SerializeField] GameObject playerStatsUI;
    [SerializeField] GameObject MaskUI;
    [SerializeField] GameObject SkillButtons;
    PlayerStatus playerStatus;
    EnemyStatus enemyStatus;

    State mCurState;
    bool isInBattle = false;
    public void SetIsInBattle(bool inBattle) { isInBattle = inBattle; }
    public bool GetIsInBattle() { return isInBattle; }


    // Start is called before the first frame update
    void Start()
    {

        mCurState = State.Preparation;
        battleUI.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        // initialize player status
        playerStatus = player.GetComponent<PlayerStatus>();
        playerStatus.SetBattleManager(this);

        // initialize enemy status
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();
        if (enemyStatus == null) {
            Debug.LogWarning("No Enemy Object in Scene");
        }
        // mask UI


    }

    void Update()
    {
        //if (mCurState == State.Preparation) { return; }
        // nothing to do here.
        // all state transition is either instantanious or based on timer
        // or based user input

        handleKeyboardInput();
    }

    void UpdatePreparation()
    {
        mCurState = State.Battle;
    }

    void ProcessEnemyTurn()
    {
        // end of player turn
        // process end of turn buffs/effects here
        if (playerStatus.GetActiveBuffs().Contains(new Buff(BuffId.POISON))) {
            float poisonDmg = 5.0f;
            playerStatus.TakeDamage(poisonDmg, SkillAttribute.SAD);
        }

        // delay xxx sec 
        // boss speak
        // delay 
        // boss use skill
        enemyStatus.MakeMove(playerStatus);
        // delay

        //Enemy Standby Phase

        //Enemy Battle Phase

        //Enemy End Phase
        playerStatus.UpdateEffectStatus();
        enemyStatus.UpdateEffectStatus();
        // mCurState = State.PlayerTurn;
        // enable button interactions
        // adjust alpha of all buttons
    }

    void UpdatePlayerDeath()
    {
        mCurState = State.Death;
        gamObjectsInScene.SetActive(true);
        battleUI.SetActive(false);
    }

    void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = playerStatus.getCurrentHealth() / playerStatus.GetMaxHealth();
    }
    void UpdateWin()
    {
        gamObjectsInScene.SetActive(true);
        battleUI.SetActive(false);
    }

    #region Handle Skill Button Click
    void handleKeyboardInput()
    {
        // TODO:
        // Q E, A D, Z C to change equipment
        // W, S, X for detailed item info
        // J K L for skills
    }

    void UseSkill(int skillSlotNumber)
    {
        switch (skillSlotNumber)
        {
            case 0:
                ProcessSkill(playerStatus.getEquippedEyeBrow().getSkill());
                break;
            case 1: 
                ProcessSkill(playerStatus.getEquippedEyes().getSkill());
                break;
            case 2: 
                ProcessSkill(playerStatus.getEquippedEyeBrow().getSkill());
                break;
            default: return;
        }
        if (mCurState != State.Battle) {
            Debug.LogError("State Mismatch");
        }
        // disable all interactable buttons
        // lower alpha of buttons
        ProcessEnemyTurn();
    }

    #endregion

    #region Process Skills

    public void ProcessSkill(Skill skill) {
        List<Buff> activeBuffs = playerStatus.GetActiveBuffs();

        // Chaos changes target 
        bool chaos = false;
        if (activeBuffs.Contains(new Buff(BuffId.CHAOS))) {
            chaos = true;
        }

        switch (skill.getSkillType()) {
            case SkillType.ATTACK:
                ProcessAttackSkill(skill, activeBuffs, chaos);
                break;
            case SkillType.DEFENSE:
                ProcessDefensiveSkill(skill, chaos);
                break;
            case SkillType.BUFF:
                ProcessEffectSkill(skill, chaos);
                break;
        }
    }

    
    // public void ProcessSkill(Skill skill) {
    //     List<Buff> activeBuffs = playerStatus.GetActiveBuffs();
    //     switch (skill.getSkillType()) {
    //         case SkillType.ATTACK:
    //             ProcessAttackSkill(skill, activeBuffs);
    //             break;
    //         case SkillType.DEFENSE:
    //             ProcessDefensiveSkill(skill);
    //             break;
    //         case SkillType.BUFF:
    //             ProcessEffectSkill(skill);
    //             break;
    //     }
    // }

    private void ProcessEffectSkill(Skill skill, bool chaos)
    {
        switch (skill.GetSkillAttribute())
        {
            case SkillAttribute.HAPPY:
                // default target = player
                if (chaos) {
                    enemyStatus.ActivateBuff(new Buff(BuffId.LIFE_STEAL));
                } else {
                    playerStatus.ActivateBuff(new Buff(BuffId.LIFE_STEAL));
                }
                break;
            case SkillAttribute.SAD:
                playerStatus.ClearBuff();
                enemyStatus.ClearBuff();
                break;
            case SkillAttribute.ANGRY:
                Buff bounusDamage = new Buff(BuffId.BONUS_DAMAGE);
                float rand = Random.Range(0.0f, 1.0f);
                bounusDamage.GenerateBounusDamage(playerStatus, rand);
                Buff blind = new Buff(Buff.BuffId.BLIND);
                blind.GenerateBlindPercentage(playerStatus, 1 - rand);
                // default buff target = player, debuff target = enemy
                // TODO: blind for player and bonusDMG for enemy
                if (chaos) {
                    break;
                    enemyStatus.ActivateBuff(bounusDamage);
                    playerStatus.ActivateBuff(blind);
                } else {
                    playerStatus.ActivateBuff(bounusDamage);
                    enemyStatus.ActivateBuff(blind);
                }
                break;
        }
    }
    // private void ProcessEffectSkill(Skill skill)
    // {
    //     switch (skill.GetSkillAttribute())
    //     {
    //         case SkillAttribute.HAPPY:
    //             playerStatus.ActivateBuff(new Buff(Buff.BuffId.LIFE_STEAL));
    //             break;
    //         case SkillAttribute.SAD:
    //             playerStatus.ClearBuff();
    //             enemyStatus.ClearBuff();
    //             break;
    //         case SkillAttribute.ANGRY:
    //             Buff bounusDamage = new Buff(Buff.BuffId.BONUS_DAMAGE);
    //             float rand = Random.Range(0.0f, 1.0f);
    //             bounusDamage.GenerateBounusDamage(playerStatus, rand);
    //             playerStatus.ActivateBuff(bounusDamage);
    //             Buff blind = new Buff(Buff.BuffId.BLIND);
    //             blind.GenerateBlindPercentage(playerStatus, 1 - rand);
    //             enemyStatus.ActivateBuff(blind);
    //             break;
    //     }
    // }

    private void ProcessDefensiveSkill(Skill skill, bool chaos)
    {
        switch (skill.GetSkillAttribute())
        {
            case SkillAttribute.HAPPY:
                float healAmount = ((DefenseSkill)skill).getHealAmount(playerStatus);
                // default target = player
                if (chaos) {
                    enemyStatus.ProcessHealing(healAmount);
                } else {
                    playerStatus.ProcessHealing(healAmount);
                }
                break;
            case SkillAttribute.SAD:
                playerStatus.TakeDamage(playerStatus.GetMaxHealth() / 4, SkillAttribute.NONE);
                // default target = player
                if (chaos) {
                    enemyStatus.ActivateBuff(new Buff(BuffId.IMMUNE));
                } else {
                    playerStatus.ActivateBuff(new Buff(BuffId.IMMUNE));
                }
                break;
            case SkillAttribute.ANGRY:
                // default target = player
                if (chaos) {
                    enemyStatus.ActivateBuff(new Buff(BuffId.REFLECT));
                } else {
                    playerStatus.ActivateBuff(new Buff(BuffId.REFLECT));
                }
                break;
        }
    }
    // private void ProcessDefensiveSkill(Skill skill)
    // {
    //     switch (skill.GetSkillAttribute())
    //     {
    //         case SkillAttribute.HAPPY:
    //             playerStatus.ProcessHealing(((DefenseSkill)skill).getHealAmount(playerStatus));
    //             break;
    //         case SkillAttribute.SAD:
    //             playerStatus.ActivateBuff(new Buff(Buff.BuffId.IMMUNE));
    //             playerStatus.TakeDamage(playerStatus.GetMaxHealth() / 4, SkillAttribute.NONE);
    //             break;
    //         case SkillAttribute.ANGRY:
    //             playerStatus.ActivateBuff(new Buff(Buff.BuffId.REFLECT));
    //             break;
    //     }
    // }

    private void ProcessAttackSkill(Skill skill, List<Buff> activeBuffs, bool chaos)
    {
        AttackSkill atkSkill = (AttackSkill)skill;
        float effectiveDamage = atkSkill.getAttackSkillDamage(playerStatus);
        foreach (Buff buff in activeBuffs)
        {
            if (buff.GetBuffId() == Buff.BuffId.BONUS_DAMAGE)
            {
                effectiveDamage += buff.GetBounusDamage();
            }
        }
        float dealtDamage = enemyStatus.TakeDamage(effectiveDamage, skill.GetSkillAttribute());
        foreach (Buff buff in activeBuffs)
        {
            if (buff.GetBuffId() == Buff.BuffId.LIFE_STEAL)
            {
                // the denominator can be adjusted later depending on stats and life steal ratio
                playerStatus.ProcessHealing((playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) / 100.0f) * dealtDamage);
            }
        }
        if (skill.GetSkillAttribute() == SkillAttribute.ANGRY)
        {
            playerStatus.TakeDamage(playerStatus.getATKbyAttribute(SkillAttribute.ANGRY), SkillAttribute.ANGRY);
        }
    }

    // private void ProcessAttackSkill(Skill skill, List<Buff> activeBuffs)
    // {
    //     AttackSkill atkSkill = (AttackSkill)skill;
    //     float effectiveDamage = atkSkill.getAttackSkillDamage(playerStatus);
    //     foreach (Buff buff in activeBuffs)
    //     {
    //         if (buff.GetBuffId() == Buff.BuffId.BONUS_DAMAGE)
    //         {
    //             effectiveDamage += buff.GetBounusDamage();
    //         }
    //     }
    //     enemyStatus.TakeDamage(effectiveDamage, skill.GetSkillAttribute());
    //     foreach (Buff buff in activeBuffs)
    //     {
    //         if (buff.GetBuffId() == Buff.BuffId.LIFE_STEAL)
    //         {
    //             playerStatus.ProcessHealing(playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) * effectiveDamage);
    //         }
    //     }
    //     if (skill.GetSkillAttribute() == SkillAttribute.ANGRY)
    //     {
    //         playerStatus.TakeDamage(playerStatus.getATKbyAttribute(SkillAttribute.ANGRY), SkillAttribute.ANGRY);
    //     }
    // }

    public void ProcessMute()
    {
        Transform[] buttons = MaskUI.GetComponentsInChildren<Transform>();
        foreach (var button in buttons)
        {
            if (button.GetComponent<Button>())
            {
                button.GetComponent<Button>().interactable = false;
                Image buttonSprite = button.gameObject.GetComponent<Image>();
                Color tempColor = buttonSprite.color;
                tempColor.a = 0.1f;
                buttonSprite.color = tempColor;
            }
        }
    }

    public void ProcessSilence()
    {
        Transform[] buttons = SkillButtons.GetComponentsInChildren<Transform>();
        foreach (var button in buttons)
        {
            if (button.GetComponent<Button>())
            {
                button.GetComponent<Button>().interactable = false;
                Image buttonSprite = button.gameObject.GetComponent<Image>();
                Color tempColor = buttonSprite.color;
                tempColor.a = 0.1f;
                buttonSprite.color = tempColor;
            }
        }
    }

    public void UnMute()
    {
        Transform[] buttons = MaskUI.GetComponentsInChildren<Transform>();
        foreach (var button in buttons)
        {
            if (button.GetComponent<Button>())
            {
                button.GetComponent<Button>().interactable = true;
                Image buttonSprite = button.gameObject.GetComponent<Image>();
                Color tempColor = buttonSprite.color;
                tempColor.a = 255.0f;
                buttonSprite.color = tempColor;
            }
        }
    }

    public void UnSlience()
    {
        Transform[] buttons = SkillButtons.GetComponentsInChildren<Transform>();
        foreach (var button in buttons)
        {
            if (button.GetComponent<Button>())
            {
                button.GetComponent<Button>().interactable = true;
                Image buttonSprite = button.gameObject.GetComponent<Image>();
                Color tempColor = buttonSprite.color;
                tempColor.a = 255.0f;
                buttonSprite.color = tempColor;
            }
        }
    }
    #endregion

    #region Update Equipment UI

    public void UpdateEquippedMask()
    {
        EyeBrow equippedEyebrow = playerStatus.getEquippedEyeBrow();
        Eye equippedEyes = playerStatus.getEquippedEyes();
        Mouth equippedMouth = playerStatus.getEquippedMouth();
        eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyebrow.getHighLightedImage());
        eyeUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyes.getHighLightedImage());
        mouthUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedMouth.getHighLightedImage());
    }

    public void UpdateSkillButtons()
    {
        EyeBrow equippedEyebrow = playerStatus.getEquippedEyeBrow();
        Eye equippedEyes = playerStatus.getEquippedEyes();
        Mouth equippedMouth = playerStatus.getEquippedMouth();
        Button[] buttons = SkillButtons.GetComponentsInChildren<Button>();
        
        buttons[0].GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyebrow.getSkillImage());
        buttons[1].GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyes.getSkillImage());
        buttons[2].GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedMouth.getSkillImage());
    }
    public void rightUpdateEyebrow()
    {
        List<EyeBrow> ownedEyebrows = playerStatus.getOwnedEyeBrows();
        EyeBrow equippedEyebrow = playerStatus.getEquippedEyeBrow();
        int n = ownedEyebrows.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(equippedEyebrow, ownedEyebrows[i]))
            {
                EyeBrow newEyebrow;
                if (i >= n - 1)
                {
                    newEyebrow = ownedEyebrows[0];
                }
                else
                {
                    newEyebrow = ownedEyebrows[Mathf.Clamp(i + 1, 0, n - 1)];
                }
                playerStatus.setEquippedEyeBrow(newEyebrow);
                playerStatus.updateStatus();
                eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(newEyebrow.getHighLightedImage());
                break;
            }
        }
    }
    public void leftUpdateEyebrow()
    {
        List<EyeBrow> ownedEyebrows = playerStatus.getOwnedEyeBrows();
        EyeBrow equippedEyebrow = playerStatus.getEquippedEyeBrow();
        int n = ownedEyebrows.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(equippedEyebrow, ownedEyebrows[i]))
            {
                EyeBrow newEyebrow;
                if (i <= 0)
                {
                    newEyebrow = ownedEyebrows[n - 1];
                }
                else
                {
                    newEyebrow = ownedEyebrows[Mathf.Clamp(i - 1, 0, n - 1)];
                }
                playerStatus.setEquippedEyeBrow(newEyebrow);
                playerStatus.updateStatus();
                eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(newEyebrow.getHighLightedImage());
                break;
            }
        }
    }

    public void rightUpdateEye()
    {
        List<Eye> ownedEyes = playerStatus.getOwnedEyes();
        Eye equippedEyes = playerStatus.getEquippedEyes();
        int n = ownedEyes.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(equippedEyes, ownedEyes[i]))
            {
                Eye newEyes;
                if (i >= n - 1)
                {
                    newEyes = ownedEyes[0];
                }
                else
                {
                    newEyes = ownedEyes[Mathf.Clamp(i + 1, 0, n - 1)];
                }
                playerStatus.setEquippedEyes(newEyes);
                playerStatus.updateStatus();
                eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(newEyes.getHighLightedImage());
                break;
            }
        }
    }
    public void leftUpdateEye()
    {
        List<Eye> ownedEyes = playerStatus.getOwnedEyes();
        Eye equippedEyes = playerStatus.getEquippedEyes();
        int n = ownedEyes.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(equippedEyes, ownedEyes[i]))
            {
                Eye newEyes;
                if (i <= 0)
                {
                    newEyes = ownedEyes[n - 1];
                }
                else
                {
                    newEyes = ownedEyes[Mathf.Clamp(i - 1, 0, n - 1)];
                }
                playerStatus.setEquippedEyes(newEyes);
                playerStatus.updateStatus();
                eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(newEyes.getHighLightedImage());
                break;
            }
        }
    }

    public void rightUpdateMouth()
    {
        List<Mouth> ownedMouths = playerStatus.getOwnedMouths();
        Mouth equippedMouth = playerStatus.getEquippedMouth();
        int n = ownedMouths.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(equippedMouth, ownedMouths[i]))
            {
                Mouth newMouth = ownedMouths[Mathf.Clamp(i + 1, 0, n - 1)];
                playerStatus.setEquippedMouth(newMouth);
                playerStatus.updateStatus();
                mouthUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(newMouth.getHighLightedImage());
                break;
            }
        }
    }
    public void leftUpdateMouth()
    {
        List<Mouth> ownedMouths = playerStatus.getOwnedMouths();
        Mouth equippedMouth = playerStatus.getEquippedMouth();
        int n = ownedMouths.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(equippedMouth, ownedMouths[i]))
            {
                Mouth newMouth;
                if (i <= 0)
                {
                    newMouth = ownedMouths[n - 1];
                }
                else
                {
                    newMouth = ownedMouths[Mathf.Clamp(i - 1, 0, n - 1)];
                }
                playerStatus.setEquippedMouth(newMouth);
                playerStatus.updateStatus();
                mouthUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(newMouth.getHighLightedImage());
                break;
            }
        }
    }

    public void UpdatePlayerStatVisual()
    {
        //update buff visuals

        string happyStat = "HappyATK: " + playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) + "\n" 
        + "HappyDEF: " + playerStatus.getDEFbyAttribute(SkillAttribute.HAPPY) + "\n";
        string angryStat = "AngryATK: " + playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) + "\n" + "AngryDEF: " 
        + playerStatus.getDEFbyAttribute(SkillAttribute.ANGRY) + "\n";
        string sadStat = "SadATK: " + playerStatus.getATKbyAttribute(SkillAttribute.SAD) + "\n" 
        + "SadDEF: " + playerStatus.getDEFbyAttribute(SkillAttribute.SAD) + "\n";
        playerStatsUI.GetComponent<TextMeshProUGUI>().text = happyStat + angryStat + sadStat;
    }
    #endregion
}