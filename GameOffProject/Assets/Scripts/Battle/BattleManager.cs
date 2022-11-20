using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Skill;
using static Effect;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.EventSystems;

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
    [SerializeField] GameObject StatusBar;
    PlayerStatus playerStatus;
    EnemyStatus enemyStatus;

    int UILayer;

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
        UILayer = LayerMask.NameToLayer("EffectUI");
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
        // process end of turn Effects/effects here
        if (playerStatus.GetActiveEffects().Contains(new Effect(EffectId.POISON))) {
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
        List<Effect> activeEffects = playerStatus.GetActiveEffects();

        // Chaos changes target 
        bool chaos = activeEffects.Contains(new Effect(EffectId.CHAOS));

        // Watched gives negative effects
        bool watched = activeEffects.Contains(new Effect(EffectId.WATCHED));

        switch (skill.getSkillType()) {
            case SkillType.ATTACK:
                ProcessAttackSkill(skill, activeEffects, chaos, watched);
                break;
            case SkillType.DEFENSE:
                ProcessDefensiveSkill(skill, chaos, watched);
                break;
            case SkillType.EFFECT:
                ProcessEffectSkill(skill, chaos, watched);
                break;
        }

        // if player has watched effect, enemy must be chef, cast always success
        if (watched) {
            int chefPhase = ((Chef) enemyStatus).getChefPhase();
            switch (skill.GetSkillAttribute()) {
                case SkillAttribute.HAPPY:
                    // if chef phase is 2 (main dish) and player uses happy skill
                    // bleed the player for 3 turn
                    // ?BLEED == POISON?
                    if (chefPhase == 2) {
                        // TODO: Is bleed = poison?
                        playerStatus.ActivateEffect(new Effect(EffectId.POISON));
                    }
                    break;
                case SkillAttribute.ANGRY:
                    // if chef phase is 1 (appetizer) and player uses angry skill
                    // silence the player for 1 turn
                    if (chefPhase == 1) {
                        playerStatus.ActivateEffect(new Effect(EffectId.SILENCED));
                    }
                    break;
                case SkillAttribute.SAD:
                    // if chef phase is 3 (dessert) and player uses sad skill
                    // WEAK the player for 2 turns (differ from weak in current effects)
                    if (chefPhase == 3) {
                        // TODO: Add negative effect for chef phase 3
                    }
                    break;
                default:
                    break;
            }
        }
    }

    
    // public void ProcessSkill(Skill skill) {
    //     List<Effect> activeEffects = playerStatus.GetActiveEffects();
    //     switch (skill.getSkillType()) {
    //         case SkillType.ATTACK:
    //             ProcessAttackSkill(skill, activeEffects);
    //             break;
    //         case SkillType.DEFENSE:
    //             ProcessDefensiveSkill(skill);
    //             break;
    //         case SkillType.Effect:
    //             ProcessEffectSkill(skill);
    //             break;
    //     }
    // }

    private void ProcessEffectSkill(Skill skill, bool chaos, bool watched)
    {
        switch (skill.GetSkillAttribute())
        {
            case SkillAttribute.HAPPY:
                // default target = player
                if (chaos) {
                    enemyStatus.ActivateEffect(new Effect(EffectId.LIFE_STEAL));
                } else {
                    playerStatus.ActivateEffect(new Effect(EffectId.LIFE_STEAL));
                }

                break;
            case SkillAttribute.SAD:
                playerStatus.ClearEffect();
                enemyStatus.ClearEffect();

                break;
            case SkillAttribute.ANGRY:
                Effect bounusDamage = new Effect(EffectId.BONUS_DAMAGE);
                float rand = Random.Range(0.0f, 1.0f);
                bounusDamage.GenerateBounusDamage(playerStatus, rand);
                Effect blind = new Effect(Effect.EffectId.BLIND);
                blind.GenerateBlindPercentage(playerStatus, 1 - rand);
                // default Effect target = player, deEffect target = enemy
                // TODO: blind for player and bonusDMG for enemy
                if (chaos) {
                    break;
                    enemyStatus.ActivateEffect(bounusDamage);
                    playerStatus.ActivateEffect(blind);
                } else {
                    playerStatus.ActivateEffect(bounusDamage);
                    enemyStatus.ActivateEffect(blind);
                }

                break;
        }
    }
    // private void ProcessEffectSkill(Skill skill)
    // {
    //     switch (skill.GetSkillAttribute())
    //     {
    //         case SkillAttribute.HAPPY:
    //             playerStatus.ActivateEffect(new Effect(Effect.EffectId.LIFE_STEAL));
    //             break;
    //         case SkillAttribute.SAD:
    //             playerStatus.ClearEffect();
    //             enemyStatus.ClearEffect();
    //             break;
    //         case SkillAttribute.ANGRY:
    //             Effect bounusDamage = new Effect(Effect.EffectId.BONUS_DAMAGE);
    //             float rand = Random.Range(0.0f, 1.0f);
    //             bounusDamage.GenerateBounusDamage(playerStatus, rand);
    //             playerStatus.ActivateEffect(bounusDamage);
    //             Effect blind = new Effect(Effect.EffectId.BLIND);
    //             blind.GenerateBlindPercentage(playerStatus, 1 - rand);
    //             enemyStatus.ActivateEffect(blind);
    //             break;
    //     }
    // }

    private void ProcessDefensiveSkill(Skill skill, bool chaos, bool watched)
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
                    enemyStatus.ActivateEffect(new Effect(EffectId.IMMUNE));
                } else {
                    playerStatus.ActivateEffect(new Effect(EffectId.IMMUNE));
                }
                break;
            case SkillAttribute.ANGRY:
                // default target = player
                if (chaos) {
                    enemyStatus.ActivateEffect(new Effect(EffectId.REFLECT));
                } else {
                    playerStatus.ActivateEffect(new Effect(EffectId.REFLECT));
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
    //             playerStatus.ActivateEffect(new Effect(Effect.EffectId.IMMUNE));
    //             playerStatus.TakeDamage(playerStatus.GetMaxHealth() / 4, SkillAttribute.NONE);
    //             break;
    //         case SkillAttribute.ANGRY:
    //             playerStatus.ActivateEffect(new Effect(Effect.EffectId.REFLECT));
    //             break;
    //     }
    // }

    private void ProcessAttackSkill(Skill skill, List<Effect> activeEffects, bool chaos, bool watched)
    {
        AttackSkill atkSkill = (AttackSkill)skill;
        float effectiveDamage = atkSkill.getAttackSkillDamage(playerStatus);
        foreach (Effect Effect in activeEffects)
        {
            if (Effect.GetEffectId() == Effect.EffectId.BONUS_DAMAGE)
            {
                effectiveDamage += Effect.GetBounusDamage();
            }
        }
        float dealtDamage = enemyStatus.TakeDamage(effectiveDamage, skill.GetSkillAttribute());
        foreach (Effect Effect in activeEffects)
        {
            if (Effect.GetEffectId() == Effect.EffectId.LIFE_STEAL)
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

    // private void ProcessAttackSkill(Skill skill, List<Effect> activeEffects)
    // {
    //     AttackSkill atkSkill = (AttackSkill)skill;
    //     float effectiveDamage = atkSkill.getAttackSkillDamage(playerStatus);
    //     foreach (Effect Effect in activeEffects)
    //     {
    //         if (Effect.GetEffectId() == Effect.EffectId.BONUS_DAMAGE)
    //         {
    //             effectiveDamage += Effect.GetBounusDamage();
    //         }
    //     }
    //     enemyStatus.TakeDamage(effectiveDamage, skill.GetSkillAttribute());
    //     foreach (Effect Effect in activeEffects)
    //     {
    //         if (Effect.GetEffectId() == Effect.EffectId.LIFE_STEAL)
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
        Skill DefenseSkill = equippedEyebrow.getSkill();
        Skill AttackSkill = equippedEyes.getSkill();
        Skill EffectSkill = equippedMouth.getSkill();


        buttons[0].GetComponent<Image>().sprite = Resources.Load<Sprite>(DefenseSkill.getSkillImage());
        buttons[0].GetComponentsInChildren<TextMeshProUGUI>()[0].text = DefenseSkill.getDisplayName();
        buttons[0].GetComponentsInChildren<TextMeshProUGUI>()[1].text = DefenseSkill.getPower().ToString();
        buttons[1].GetComponent<Image>().sprite = Resources.Load<Sprite>(AttackSkill.getSkillImage());
        buttons[1].GetComponentsInChildren<TextMeshProUGUI>()[0].text = AttackSkill.getDisplayName();
        buttons[1].GetComponentsInChildren<TextMeshProUGUI>()[1].text = AttackSkill.getPower().ToString();
        buttons[2].GetComponent<Image>().sprite = Resources.Load<Sprite>(EffectSkill.getSkillImage());
        buttons[2].GetComponentsInChildren<TextMeshProUGUI>()[0].text = EffectSkill.getDisplayName();
        buttons[2].GetComponentsInChildren<TextMeshProUGUI>()[1].text = EffectSkill.getPower().ToString();
    }

    void UpdateStatusBar()
    {
        
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }

    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
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
        //update Effect visuals

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