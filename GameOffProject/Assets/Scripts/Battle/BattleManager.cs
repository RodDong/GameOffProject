using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Skill;

public class BattleManager : MonoBehaviour
{
    enum State
    {
        Preparation,
        PlayerTurn,
        EnemyTurn,
        Death,
        Win
    }

    //public GameObject enemy;
    GameObject player;

    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject gamObjectsInScene;

    float maxHealth;
    float curHealth;

    [SerializeField] GameObject eyebrowUI;
    [SerializeField] GameObject eyeUI;
    [SerializeField] GameObject mouthUI;
    [SerializeField] GameObject playerStatsUI;
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

        // initialize enemy status
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();

        // mask UI
        
        
    }

    void Update() {
        // nothing to do here.
        // all state transition is either instantanious or based on timer
        // or based user input
        handleKeyboardInput();
        if (battleUI.active)
        {
            updatePlayerStatVisual();
            EyeBrow eb = playerStatus.getEquippedEyeBrow();
            Eye e = playerStatus.getEquippedEyes();
            Mouth m = playerStatus.getEquippedMouth();
            eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(eb.getHighLightedImage());
            eyeUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(e.getHighLightedImage());
            mouthUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(m.getHighLightedImage());
        }
        
    }

    void UpdateCurState()
    {
        switch (mCurState)
        {
            case State.Preparation:
                UpdatePreparation();
                break;
            case State.PlayerTurn:
                UpdatePlayerTurn();
                break;
            case State.EnemyTurn:
                UpdateEnemyTurn();
                break;
            case State.Death:
                UpdatePlayerDeath();
                break;
            case State.Win:
                UpdateWin();
                break;
        }
    }

    void UpdatePreparation()
    {
        mCurState = State.PlayerTurn;
    }

    void UpdatePlayerTurn()
    {
        //Player Standby Phase
        // check buff 

        //Player Battle Phase

        //Player End Phase
    }

    void UpdateEnemyTurn()
    {

        //Enemy Standby Phase

        //Enemy Battle Phase

        //Enemy End Phase
    }

    void UpdatePlayerDeath()
    {
        gamObjectsInScene.SetActive(true);
        battleUI.SetActive(false);
    }

    void UpdateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = playerStatus.getCurrentHealth() / PlayerStatus.MAX_HEALTH;
    }
    void UpdateWin()
    {
        gamObjectsInScene.SetActive(true);
        battleUI.SetActive(false);
    }

    #region Handle Skill Button Click
    void handleKeyboardInput() {
        // TODO:
        // Q E, A D, Z C to change equipment
        // W, S, X for detailed item info
        // J K L for skills
    }

    void useSkill(int skillSlotNumber) {
        switch (skillSlotNumber) {
            case 0:
                processSkill(playerStatus.getEquippedEyeBrow().getSkill());
                break;
            case 1: 
                processSkill(playerStatus.getEquippedEyes().getSkill());
                break;
            case 2: 
                processSkill(playerStatus.getEquippedEyeBrow().getSkill());
                break;
            default: return;
        }
    }

    #endregion

    #region Process Skills

    public void processSkill(Skill skill) {
        switch (skill.getSkillType()) {
            case SkillType.ATTACK:
                AttackSkill atkSkill = (AttackSkill)skill;
                enemyStatus.TakeDamage(atkSkill.getAttackSkillDamage(playerStatus, enemyStatus));
                if (skill.GetSkillAttribute() == SkillAttribute.ANGRY) {
                    playerStatus.TakeDamage(playerStatus.getATKbyAttribute(SkillAttribute.ANGRY), SkillAttribute.ANGRY);
                }
                break;
            case SkillType.DEFENSE:
                switch (skill.GetSkillAttribute()) {
                    case SkillAttribute.HAPPY:
                        playerStatus.ProcessHealing(((DefenseSkill)skill).getHealAmount(playerStatus));
                        break;
                    case SkillAttribute.SAD:
                        playerStatus.activateBuff(new Buff(Buff.BuffId.IMMUNE));
                        break;
                    case SkillAttribute.ANGRY:
                        playerStatus.activateBuff(new Buff(Buff.BuffId.REFLECT));
                        break;
                }
                break;
            case SkillType.BUFF:
                switch (skill.GetSkillAttribute()) {
                    case SkillAttribute.HAPPY:
                        playerStatus.activateBuff(new Buff(Buff.BuffId.LIFE_STEAL));
                        break;
                    case SkillAttribute.SAD:
                        enemyStatus.activateBuff(new Buff(Buff.BuffId.PURGE));
                        break;
                    case SkillAttribute.ANGRY:
                        playerStatus.activateBuff(new Buff(Buff.BuffId.BOUNS_DAMAGE));
                        enemyStatus.activateBuff(new Buff(Buff.BuffId.BLIND));
                        break;
                }
                break;
            }
        }

    // skill slot 1: attack skill
    // skill slot 2: defense skill
    // skill slot 3: buff/debuff skill
    /*    public void processAttackSkill() {
            AttackSkill attackSkill = (AttackSkill) playerStatus.GetSkills()[0];
            SkillAttribute attribute = attackSkill.GetSkillAttribute();
            float playerATK = playerStatus.getATKbyAttribute(attribute);
            float targetDEF = enemyStatus.getDEFbyAttribute(attribute);
            enemyStatus.TakeDamage(attackSkill.getAttackSkillDamage(playerATK, targetDEF));

            mCurState = State.EnemyTurn;
        }*/

    public void processDefenseSkill() {
        DefenseSkill defenseSkill = (DefenseSkill) playerStatus.GetSkills()[1];
        SkillAttribute attribute = defenseSkill.GetSkillAttribute();

        // process defense skill

        mCurState = State.EnemyTurn;
    }

    public void processEffectSkill() {
        Skill skill = playerStatus.GetSkills()[2];
        SkillType type = skill.getSkillType();
        switch(type) {
            case SkillType.BUFF:
                processBuffSkill((BuffSkill) skill);
                break;
            case SkillType.DEBUFF:
                processDebuffSkill((DebuffSkill) skill);
                break;
            default:
                break;
        }

        mCurState = State.EnemyTurn;
    }

    void processBuffSkill(BuffSkill skill) {
        // process buff skill
    }

    void processDebuffSkill(DebuffSkill skill) {
        // process debuff skill
    }
    #endregion

    #region Update Equipment UI
    public void rightUpdateEyebrow() {
        List<EyeBrow> ownedEBs = playerStatus.getOwnedEyeBrows();
        EyeBrow eb = playerStatus.getEquippedEyeBrow();
        int n = ownedEBs.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(eb, ownedEBs[i])) {
                EyeBrow neweb = ownedEBs[(i+1)%n];
                playerStatus.setEquippedEyeBrow(neweb);
                playerStatus.updateStatus();
                eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(neweb.getHighLightedImage());
                break;
            }
        }
    }
    public void leftUpdateEyebrow() {
        List<EyeBrow> ownedEBs = playerStatus.getOwnedEyeBrows();
        EyeBrow eb = playerStatus.getEquippedEyeBrow();
        int n = ownedEBs.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(eb, ownedEBs[i])) {
                EyeBrow neweb;
                if (i <= 0)
                {
                    neweb = ownedEBs[n - 1];
                }
                else
                {
                    neweb = ownedEBs[(i - 1) % n];
                }
                playerStatus.setEquippedEyeBrow(neweb);
                playerStatus.updateStatus();
                eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(neweb.getHighLightedImage());
                break;
            }
        }
    }
    
    public void rightUpdateEye() {
        List<Eye> ownedEs = playerStatus.getOwnedEyes();
        Eye eb = playerStatus.getEquippedEyes();
        int n = ownedEs.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(eb, ownedEs[i])) {
                Eye neweb = ownedEs[(i+1)%n];
                playerStatus.setEquippedEyes(neweb);
                playerStatus.updateStatus();
                eyeUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(neweb.getHighLightedImage());
                break;
            }
        }
    }
    public void leftUpdateEye() {
        List<Eye> ownedEs = playerStatus.getOwnedEyes();
        Eye eb = playerStatus.getEquippedEyes();
        int n = ownedEs.Count;
        for (int i = 0; i < n; i++)
        {

            if (Item.Equals(eb, ownedEs[i])) {

                Eye neweb;
                if (i <= 0)
                {
                    neweb = ownedEs[n - 1];
                }
                else
                {
                    neweb = ownedEs[(i - 1) % n];
                }
                playerStatus.setEquippedEyes(neweb);
                playerStatus.updateStatus();
                eyeUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(neweb.getHighLightedImage());
                break;
            }
        }
    }
    
    public void rightUpdateMouth() {
        List<Mouth> ownedEs = playerStatus.getOwnedMouths();
        Mouth eb = playerStatus.getEquippedMouth();
        int n = ownedEs.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(eb, ownedEs[i])) {
                Mouth neweb = ownedEs[(i+1)%n];
                playerStatus.setEquippedMouth(neweb);
                playerStatus.updateStatus();
                mouthUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(neweb.getHighLightedImage());
                break;
            }
        }
    }
    public void leftUpdateMouth() {
        List<Mouth> ownedEs = playerStatus.getOwnedMouths();
        Mouth eb = playerStatus.getEquippedMouth();
        int n = ownedEs.Count;
        for (int i = 0; i < n; i++)
        {
            if (Item.Equals(eb, ownedEs[i])) {
                Mouth neweb;
                if (i <= 0)
                {
                    neweb = ownedEs[n - 1];
                }
                else
                {
                    neweb = ownedEs[(i - 1) % n];
                }
                playerStatus.setEquippedMouth(neweb);
                playerStatus.updateStatus();
                mouthUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(neweb.getHighLightedImage());
                break;
            }
        }
    }

    public void updatePlayerStatVisual()
    {
        string happyStat = "HappyATK: " + playerStatus.getHappyATK() + "\n" + "HappyDEF: " + playerStatus.getHappyDEF() + "\n";
        string angryStat = "AngryATK: " + playerStatus.getAngryATK() + "\n" + "AngryDEF: " + playerStatus.getAngryDEF() + "\n";
        string sadStat = "SadATK: " + playerStatus.getSadATK() + "\n" + "SadDEF: " + playerStatus.getSadDEF() + "\n";
        playerStatsUI.GetComponent<TextMeshProUGUI>().text = happyStat + angryStat + sadStat;
    }
    #endregion
}
