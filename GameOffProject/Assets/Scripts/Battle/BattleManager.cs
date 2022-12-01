using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Skill;
using static Effect;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEditor.Search;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public enum State
    {
        Preparation,
        Battle,
        Death,
        Win
    }

    //public GameObject enemy;
    public GameObject gameObjectsInScene;
    GameObject player;

    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject EnemyImage;
    [SerializeField] GameObject playerHealthBar;
    [SerializeField] GameObject enemyHealthBar;
    [SerializeField] GameObject eyebrowUI;
    [SerializeField] GameObject eyeUI;
    [SerializeField] GameObject mouthUI;
    [SerializeField] GameObject playerStatsUI;
    [SerializeField] GameObject MaskUI;
    [SerializeField] GameObject SkillButtons;
    [SerializeField] GameObject PlayerStatusBar;
    [SerializeField] GameObject EnemyStatusBar;
    [SerializeField] GameObject PlayerMessageBox;
    [SerializeField] GameObject EnemyMessageBox;
    [SerializeField] GameObject Left1, Left2, Left3;
    [SerializeField] GameObject Right1, Right2, Right3;
    [SerializeField] GameObject AttackSkillButton;
    PlayerStatus playerStatus;
    PlayerMove playerMove;
    EnemyStatus enemyStatus;
    List<Transform> playerEffectIconTransforms = new List<Transform>();
    List<Transform> enemyEffectIconTransforms = new List<Transform>();

    SkillAttribute playerPrevSkillAttribute;
    (string, string, string) enemySentences = ("sentence1", "sentence2", "sentence3");
    (string, string, string) playerSentences = ("playertalk1", "playertalk2", "playertalk3");

    State mCurState;
    bool isInBattle = false;
    private ProgressManager progressManager;
    private Sprite buffIcon;
    private Sprite debuffIcon;

    private EyeBrow playerPrevEyeBrow;
    private Eye playerPrevEye;
    private Mouth playerPrevMouth;

    public List<Transform> GetPlayerEffectTransfroms() { return playerEffectIconTransforms; }
    public List<Transform> GetEnemyEffectTransfroms() { return enemyEffectIconTransforms; }
    public void SetBattleState(State state) { mCurState = state; }

    // Start is called before the first frame update
    void Start()
    {
        mCurState = State.Preparation;
        battleUI.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        // initialize player status
        playerStatus = player.GetComponent<PlayerStatus>();
        playerStatus.SetBattleManager(this);

        //initialize player move
        playerMove = player.GetComponent<PlayerMove>();

        // initialize enemy status
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();
        if (enemyStatus == null) {
            Debug.LogWarning("No Enemy Object in Scene");
        }

        buffIcon = Resources.Load<Sprite>("Art/UI/buffIcons/buff");
        debuffIcon = Resources.Load<Sprite>("Art/UI/buffIcons/debuff");

        UpdatePlayerStatusBar();
        UpdateEnemyStatusBar();
        if (enemyStatus)
        {
            enemySentences = enemyStatus.GetEnemySentences();
            playerSentences = enemyStatus.GetPlayerSentences();
        }

        progressManager = FindObjectOfType<ProgressManager>();
    }

    void Update()
    {
        if (!enemyStatus)
        {
            return;
        }
        if (!gameObjectsInScene) {
            gameObjectsInScene = GameObject.FindGameObjectWithTag("ObjectsToHide");
        }

        if (enemyStatus.GetCurrentHealth() <= 0.0f)
        {
            UpdateWin();
        }

        if (playerStatus.GetCurrentHealth() <= 0.0f)
        {
            UpdatePlayerDeath();
        }

        EnemyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(enemyStatus.enemyImage);

        handleKeyboardInput();
        UpdatePlayerStatusBar();
        UpdateEnemyStatusBar();
    }
    public void ResetBattleVisuals()
    {
        enemySentences = enemyStatus.GetEnemySentences();
        playerSentences = enemyStatus.GetPlayerSentences();
        UpdateEnemyHealthBar();
        UpdatePlayerHealthBar();
        UpdatePlayerStatVisual();
        UpdateEnemyStatusBar();
        UpdatePlayerStatusBar();
    }

    public void DeactivateGameObjectsInScene()
    {
        gameObjectsInScene.SetActive(false);
    }

    public void ActivateGameObjectsInScene()
    {
        gameObjectsInScene.SetActive(true);
    }

    public void DeactivateLeftArrows()
    {
        Left1.SetActive(false);
        Left2.SetActive(false);
        Left3.SetActive(false);
    }

    public void DeactivateRightArrows()
    {
        Right1.SetActive(false);
        Right2.SetActive(false);
        Right3.SetActive(false);
    }

    public void ActivateLeftArrows()
    {
        Left1.SetActive(true);
        Left2.SetActive(true);
        Left3.SetActive(true);
    }

    public void ActivateRightArrows()
    {
        Right1.SetActive(true);
        Right2.SetActive(true);
        Right3.SetActive(true);
    }

    async void ProcessEnemyTurn()
    {
        List<Effect> activeEffects = playerStatus.GetActiveEffects();
        Effect taunted = activeEffects.Find((Effect b) => { return b.GetEffectId() == EffectId.TAUNTED; });
        Effect silenced = activeEffects.Find((Effect b) => { return b.GetEffectId() == EffectId.SILENCED; });
        Effect mute = activeEffects.Find((Effect b) => { return b.GetEffectId() == EffectId.MUTE; });
        // end of player turn
        // process end of turn effects here
        if(taunted == null && silenced == null && mute == null)
        {
            DisablePlayerSkillButtons();
        }

        PlayerOnClickTalkEvent();
        DeactivateLeftArrows();
        DeactivateRightArrows();

        if (enemyStatus.enemyImage == "Art/Tachies/Boss_Battle")
        {
            await Task.Delay(1000); ;
        }
        // obsolete: Replaced by poison in ProcessSkill
        // if (playerStatus.GetActiveEffects().Contains(new Effect(EffectId.POISON))) {
        //     float poisonDmg = 5.0f;
        //     playerStatus.TakeDamage(poisonDmg, SkillAttribute.SAD);
        // }



        Debug.Log("Ending Player Turn");

        /*        Debug.Log("Player HP： " + playerStatus.GetCurrentHealth());
                Debug.Log("Enemy HP： " + enemyStatus.GetCurrentHealth());*/

        Debug.Log("Start Enemy Turn");
        // delay xxx sec 
        // boss speak
        EnemyTalk();
        await Task.Delay(1000);

        ActivateLeftArrows();
        ActivateRightArrows();
        
        if (taunted == null && silenced == null && mute == null)
        {
            EnablePlayerSkillButtons();
        }
        EnemyMessageBox.SetActive(false);

        // delay 
        // boss use skill
        enemySentences = enemyStatus.MakeMove(playerStatus);
        // delay
        Debug.Log("Ending Enemy Turn");
        //Enemy Standby Phase
        /*Debug.Log("Player HP： " + playerStatus.GetCurrentHealth());
        Debug.Log("Enemy HP： " + enemyStatus.GetCurrentHealth());*/
        //Enemy Battle Phase

        //Enemy End Phase
        playerStatus.UpdateEffectStatus();
        // mCurState = State.PlayerTurn;
        // enable button interactions
        // adjust alpha of all buttons
        Debug.Log("Start Player Turn");

        UpdatePlayerHealthBar();
        UpdateEnemyHealthBar();
        
    }

    void UpdatePlayerDeath()
    {
        ActivateGameObjectsInScene();
        player.GetComponent<SpriteRenderer>().enabled = true;
        playerStatus.ResetCurrentHealth();
        enemyStatus.ResetCurrentHealth();
        playerStatus.ClearEffect();
        enemyStatus.ClearEffect();
        UpdatePlayerHealthBar();
        UpdateEnemyHealthBar();
        UpdatePlayerStatusBar();
        UpdateEnemyStatusBar();
        battleUI.SetActive(false);
        playerMove.SetCurState(PlayerMove.State.Idle);
        if (FindObjectOfType<OpeningManager>(true) != null)
        {
            FindObjectOfType<OpeningManager>(true).playEndDream();
        }
        if (FindObjectOfType<InsideOfficeManager>(true) != null) {
            FindObjectOfType<InsideOfficeManager>(true).ProcessPlayerDeath();
        } 
        if (FindObjectOfType<StudioProgressManager>(true) != null) {
            FindObjectOfType<StudioProgressManager>(true).ProcessPlayerDeath();
        }
        if (FindObjectOfType<InsideClinicManager>(true) != null) {
            FindObjectOfType<InsideClinicManager>(true).ProcessPlayerDeath();
        }
        if (FindObjectOfType<BedroomProgressManager>(true) != null) {
            FindObjectOfType<BedroomProgressManager>(true).ProcessPlayerDeath();
        }
        if (FindObjectOfType<kitchenManager>(true) != null)
        {
            FindObjectOfType<kitchenManager>(true).ProcessPlayerDeath();
        }
    }

    void UpdateWin()
    {
        ActivateGameObjectsInScene();
        if (FindObjectOfType<InsideOfficeManager>(true) != null) {
            FindObjectOfType<InsideOfficeManager>(true).ProcessEnemyDeath();
        }
        if (FindObjectOfType<InsideClinicManager>(true) != null) {
            FindObjectOfType<InsideClinicManager>(true).ProcessEnemyDeath();
        }
        if (FindObjectOfType<kitchenManager>(true) != null)
        {
            FindObjectOfType<kitchenManager>(true).ProcessEnemyDeath();
        }
        if(FindObjectOfType<StudioProgressManager>(true) != null)
        {
            FindObjectOfType<StudioProgressManager>(true).ProcessProgress_36_2();
        }

        player.GetComponent<SpriteRenderer>().enabled = true;
        playerStatus.ResetCurrentHealth();
        enemyStatus.ResetCurrentHealth();
        playerStatus.ClearEffect();
        enemyStatus.ClearEffect();
        UpdatePlayerHealthBar();
        UpdateEnemyHealthBar();
        UpdatePlayerStatusBar();
        UpdateEnemyStatusBar();
        battleUI.SetActive(false);
        playerMove.SetCurState(PlayerMove.State.Idle);
        DisplayVictoryDialogue();

    }

    async void DisplayVictoryDialogue() {
        enemyStatus.DropItems(playerStatus);
        if (enemyStatus.defeatInkJSON) {
            playerMove.EnterDialogueMode();
            await Task.Delay(200);
            DialogueManager.GetInstance().EnterDialogueMode(enemyStatus.defeatInkJSON);
        }
    }

    void DisablePlayerSkillButtons()
    {
        foreach (Transform button in SkillButtons.transform)
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

    void EnablePlayerSkillButtons()
    {
        foreach (Transform button in SkillButtons.transform)
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

    void UpdatePlayerHealthBar()
    {
        float playerCurHealth = playerStatus.GetCurrentHealth();
        float playerMaxHealth = playerStatus.GetMaxHealth();
        playerHealthBar.GetComponentInChildren<Slider>().value = playerCurHealth / playerMaxHealth;
        playerHealthBar.GetComponentInChildren<TextMeshProUGUI>().text = (int)playerCurHealth + " / " + playerMaxHealth;
    }

    void UpdateEnemyHealthBar()
    {
        float enemyCurHealth = enemyStatus.GetCurrentHealth();
        float enemyMaxHealth = enemyStatus.GetMaxHealth();
        enemyHealthBar.GetComponentInChildren<Slider>().value = enemyCurHealth / enemyMaxHealth;
        enemyHealthBar.GetComponentInChildren<TextMeshProUGUI>().text = (int)enemyCurHealth + " / " + enemyMaxHealth;
    }

    #region Handle Skill Button Click
    void handleKeyboardInput()
    {
        // TODO:
        // Q E, A D, Z C to change equipment
        // W, S, X for detailed item info
        // J K L for skills
    }

    public void UseSkill(int skillSlotNumber)
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
                ProcessSkill(playerStatus.getEquippedMouth().getSkill());
                break;
            default: return;
        }
        if (mCurState != State.Battle) {
            Debug.LogError("State Mismatch");
        }
        // disable all interactable buttons
        // lower alpha of buttons
        enemyStatus.UpdateEffectStatus();
        ProcessEnemyTurn();
        UpdatePlayerHealthBar();
    }

    #endregion

    #region Process Skills

    public void ProcessSkill(Skill skill) {

        //Update playerPrevskill attribute for usage in enemy turn
        playerPrevSkillAttribute = skill.GetSkillAttribute();

        // Chaos changes target 
        bool chaos = playerStatus.GetActiveEffects().Contains(new Effect(EffectId.CHAOS));

        // Watched gives negative effects
        bool watched = playerStatus.GetActiveEffects().Contains(new Effect(EffectId.WATCHED));

        switch (skill.getSkillType()) {
            case SkillType.ATTACK:
                ProcessAttackSkill(skill, chaos);
                break;
            case SkillType.DEFENSE:
                ProcessDefensiveSkill(skill, chaos);
                break;
            case SkillType.EFFECT:
                ProcessEffectSkill(skill, chaos);
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
                        Effect poisonEffect = new Effect(EffectId.POISON);
                        poisonEffect.SetPoison(SkillAttribute.ANGRY, 5.0f);
                        playerStatus.ActivateEffect(poisonEffect);
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
                        playerStatus.ActivateEffect(new Effect(EffectId.FRAGILE));
                    }
                    break;
                default:
                    break;
            }
        }

        // poison is processed at the end phase of player turn
        Effect poison = playerStatus.GetEffect(EffectId.POISON);
        if (poison.GetEffectId() != EffectId.NONE) {
            Debug.Log("Player get poison damage by: " + poison.GetPoisonAmount());
            playerStatus.TakeDamage(poison.GetPoisonAmount(), poison.GetPoisonAttribute());
        }
    }

    private void ProcessEffectSkill(Skill skill, bool chaos)
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
                    enemyStatus.ActivateEffect(bounusDamage);
                    playerStatus.ActivateEffect(blind);
                } else {
                    playerStatus.ActivateEffect(bounusDamage);
                    enemyStatus.ActivateEffect(blind);
                }

                break;
            case SkillAttribute.NONE:
                Effect reducedEffect = new Effect(EffectId.REDUCED);
                reducedEffect.SetAttackReduction(10.0f, SkillAttribute.SAD);
                reducedEffect.SetAttackReduction(10.0f, SkillAttribute.HAPPY);
                reducedEffect.SetAttackReduction(10.0f, SkillAttribute.ANGRY);
                enemyStatus.ActivateEffect(reducedEffect);
                break;
        }
    }

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
            case SkillAttribute.NONE:
                if (chaos) {
                    enemyStatus.ActivateEffect(new Effect(EffectId.FORTIFIED));
                } else {
                    playerStatus.ActivateEffect(new Effect(EffectId.FORTIFIED));
                }
                break;
        }
    }
    private void ProcessAttackSkill(Skill skill, bool chaos)
    {
        // if player has blind by chaos, has a chance to miss
        Effect blind = playerStatus.GetActiveEffects().Find((Effect b) => { return b.GetEffectId() == EffectId.BLIND; });
        if (blind != null && Random.Range(0f, 1f) < blind.GetBlindPercentage()) {
            // play missed animation?
            return; // MISS
        } else {
            // did not miss -> proceed as normal
            AttackSkill attackSkill = (AttackSkill) skill;
            float effectiveDamage = attackSkill.getAttackSkillDamage(playerStatus);
            foreach (Effect effect in playerStatus.GetActiveEffects())
            {
                if (effect.GetEffectId() == Effect.EffectId.BONUS_DAMAGE)
                {
                    effectiveDamage += effect.GetBounusDamage();
                }
            }

            if (playerStatus.GetActiveEffects().Contains(new Effect(EffectId.FRAGILE))) {
                effectiveDamage *= 0.8f;
            }
            
            float dealtDamage;
            // default target = enemy
            SkillAttribute attr = skill.GetSkillAttribute();
            if (attr == SkillAttribute.NONE) {
                float rand = Random.Range(0,3);
                if (rand < 1) {
                    attr = SkillAttribute.HAPPY;
                } else if (rand < 2) {
                    attr = SkillAttribute.SAD;
                } else {
                    attr = SkillAttribute.ANGRY;
                }
            }

            if (chaos) {
                dealtDamage = playerStatus.TakeDamage(effectiveDamage, skill.GetSkillAttribute());
            } else {
                dealtDamage = enemyStatus.TakeDamage(effectiveDamage, skill.GetSkillAttribute());
            }

            // if player atks player still heal if player has life steal regardless of chaos
            foreach (Effect effect in playerStatus.GetActiveEffects())
            {
                if (effect.GetEffectId() == Effect.EffectId.LIFE_STEAL)
                {
                    // the denominator can be adjusted later depending on stats and life steal ratio
                    float lifestealBaseStat = 100.0f;
                    playerStatus.ProcessHealing((playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) / lifestealBaseStat) * dealtDamage);
                }
            }
            if (!chaos) // if !chaos, player hit enemy
                foreach (Effect effect in enemyStatus.GetActiveEffects())
                {
                    if (effect.GetEffectId() == Effect.EffectId.REFLECT) // if enemt has reflect
                    {   // damage reflected
                        playerStatus.TakeDamage(dealtDamage, skill.GetSkillAttribute());
                    }
                }
        }
        // player should take dmg for angryATK regardless of missing due to blind
        if (skill.GetSkillAttribute() == SkillAttribute.ANGRY)
        {
            float sacrificeRatio = 4.0f;
            playerStatus.TakeDamage(playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) / sacrificeRatio, SkillAttribute.ANGRY);
        }

        UpdateEnemyHealthBar();
    }

    #endregion

    #region Process Skill-induced UI changes

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

    public async void ProcessSilence()
    {
        DisablePlayerSkillButtons();
        
        foreach(Transform arrow in MaskUI.transform)
        {
            if (arrow.GetComponent<Button>())
            {
                arrow.GetComponent<Button>().interactable = false;
                Image arrowSprite = arrow.gameObject.GetComponent<Image>();
                Color tempColor = arrowSprite.color;
                tempColor.a = 0.1f;
                arrowSprite.color = tempColor;
            }
        }

        await Task.Delay(2000);

        playerPrevEyeBrow = playerStatus.getEquippedEyeBrow();
        playerPrevEye = playerStatus.getEquippedEyes();
        playerPrevMouth = playerStatus.getEquippedMouth();

        playerStatus.setEquippedEyeBrow(new EyeBrow(SkillAttribute.NONE));
        playerStatus.setEquippedEyes(new Eye(SkillAttribute.NONE));
        playerStatus.setEquippedMouth(new Mouth(SkillAttribute.NONE));
        EnablePlayerSkillButtons();
        UpdateSkillButtons();

    }

    public void ProcessTaunted()
    {
        int index = 0;
        foreach (Transform button in SkillButtons.transform) {
            if (index != 1) {
                button.GetComponent<Button>().interactable = false;
                Image buttonSprite = button.gameObject.GetComponent<Image>();
                Color tempColor = buttonSprite.color;
                tempColor.a = 255.0f;
                buttonSprite.color = tempColor;
            }

            index++;
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
        foreach (Transform arrow in MaskUI.transform)
        {
            if (arrow.GetComponent<Button>())
            {
                arrow.GetComponent<Button>().interactable = true;
                Image arrowSprite = arrow.gameObject.GetComponent<Image>();
                Color tempColor = arrowSprite.color;
                tempColor.a = 255.0f;
                arrowSprite.color = tempColor;
            }
        }

        playerStatus.setEquippedEyeBrow(playerPrevEyeBrow);
        playerStatus.setEquippedEyes(playerPrevEye);
        playerStatus.setEquippedMouth(playerPrevMouth);

        UpdateSkillButtons();

    }

    public void UnTaunted()
    {
        int index = 0;
        foreach (Transform button in SkillButtons.transform) {
            if (index != 1) {
                button.GetComponent<Button>().interactable = true;
                Image buttonSprite = button.gameObject.GetComponent<Image>();
                Color tempColor = buttonSprite.color;
                tempColor.a = 255.0f;
                buttonSprite.color = tempColor;
            }

            index++;
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

    void UpdatePlayerStatusBar()
    {
        List<Effect> activeEffects = playerStatus.GetActiveEffects();
        Transform[] childTransforms = PlayerStatusBar.GetComponentsInChildren<Transform>(true);
        
        foreach(var child in childTransforms)
        {
            if (child.GetComponent<Image>())
            {
                playerEffectIconTransforms.Add(child);
            }
        }

        for (int i = 9; i >= activeEffects.Count; i--)
        {
            if (playerEffectIconTransforms[i].gameObject.activeSelf)
            {
                playerEffectIconTransforms[i].gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < activeEffects.Count; i++)
        {
            Transform effectTrans = playerEffectIconTransforms[i];
            if (!effectTrans.gameObject.activeSelf)
            {
                effectTrans.gameObject.SetActive(true);
            }
            if (activeEffects[i].isBuff())
            {
                effectTrans.GetComponent<Image>().sprite = buffIcon;
            }
            else
            {
                effectTrans.GetComponent<Image>().sprite = debuffIcon;
            }
        }
    }

    void UpdateEnemyStatusBar()
    {
        if (!enemyStatus)
        {
            return;
        }
        List<Effect> activeEffects = enemyStatus.GetActiveEffects();
        Transform[] childTransforms = EnemyStatusBar.GetComponentsInChildren<Transform>(true);

        foreach (var child in childTransforms)
        {
            if (child.GetComponent<Image>())
            {
                enemyEffectIconTransforms.Add(child);
            }
        }

        for (int i = 9; i >= activeEffects.Count; i--)
        {
            if (enemyEffectIconTransforms[i].gameObject.activeSelf)
            {
                enemyEffectIconTransforms[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < activeEffects.Count; i++)
        {
            Transform effectTrans = enemyEffectIconTransforms[i];
            if (!effectTrans.gameObject.activeSelf)
            {
                effectTrans.gameObject.SetActive(true);
            }
            if (activeEffects[i].isBuff())
            {
                effectTrans.GetComponent<Image>().sprite = buffIcon;
            }
            else
            {
                effectTrans.GetComponent<Image>().sprite = debuffIcon;
            }
        }
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
                Mouth newMouth;
                if (i >= n - 1)
                {
                    newMouth = ownedMouths[0];
                }
                else
                {
                    newMouth = ownedMouths[Mathf.Clamp(i + 1, 0, n - 1)];
                }
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

    void PlayerTalk()
    {
        PlayerMessageBox.SetActive(true);
        TextMeshProUGUI textBox = PlayerMessageBox.GetComponentInChildren<TextMeshProUGUI>(true);
        int i = Random.Range(0, 3);
        switch (playerPrevSkillAttribute)
        {
            case SkillAttribute.HAPPY:
                textBox.text = playerSentences.Item1;
                break;
            case SkillAttribute.SAD:
                textBox.text = playerSentences.Item2;
                break;
            case SkillAttribute.ANGRY:
                textBox.text = playerSentences.Item3;
                break;
            case SkillAttribute.NONE:
                if (i <= 0)
                {
                    textBox.text = playerSentences.Item1;
                }
                else if (i <= 1)
                {
                    textBox.text = playerSentences.Item2;
                }
                else
                {
                    textBox.text = playerSentences.Item3;
                }

                break;
        }

    }

    void PlayerStopTalk()
    {
        PlayerMessageBox.SetActive(false);
    }

    public async void PlayerOnClickTalkEvent()
    {
        if(enemyStatus.enemyImage != "Art/Tachies/Boss_Battle")
        {
            return;
        }
        PlayerTalk();
        await Task.Delay(1000);
        PlayerStopTalk();
        await Task.Delay(2000);
    }

    void EnemyTalk()
    {
        EnemyMessageBox.SetActive(true);
        Debug.Log("talk");
        TextMeshProUGUI textBox = EnemyMessageBox.GetComponentInChildren<TextMeshProUGUI>(true);
        int i = Random.Range(0, 3);
        switch (playerPrevSkillAttribute)
        {
            case SkillAttribute.HAPPY:
                textBox.text = enemySentences.Item1;
                break;
            case SkillAttribute.SAD:
                textBox.text = enemySentences.Item2;
                break;
            case SkillAttribute.ANGRY:
                textBox.text = enemySentences.Item3;
                break;
            case  SkillAttribute.NONE:
                if (i <= 0) {
                    textBox.text = enemySentences.Item1;
                }
                else if (i <= 1)
                {
                    textBox.text = enemySentences.Item2;
                }
                else
                {
                    textBox.text = enemySentences.Item3;
                }
                
                break;
        }
    }

    public void ResetEnemyStatus() {
        enemyStatus = FindObjectOfType<EnemyStatus>();
    }
}