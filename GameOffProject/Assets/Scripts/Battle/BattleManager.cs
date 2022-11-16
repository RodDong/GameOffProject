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

    void Update()
    {
        // nothing to do here.
        // all state transition is either instantanious or based on timer
        // or based user input
        handleKeyboardInput();
        if (battleUI.activeSelf)
        {
            updatePlayerStatVisual();
            EyeBrow equippedEyebrow = playerStatus.getEquippedEyeBrow();
            Eye equippedEyes = playerStatus.getEquippedEyes();
            Mouth equippedMouth = playerStatus.getEquippedMouth();
            eyebrowUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyebrow.getHighLightedImage());
            eyeUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyes.getHighLightedImage());
            mouthUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedMouth.getHighLightedImage());
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
    void handleKeyboardInput()
    {
        // TODO:
        // Q E, A D, Z C to change equipment
        // W, S, X for detailed item info
        // J K L for skills
    }

    void useSkill(int skillSlotNumber)
    {
        switch (skillSlotNumber)
        {
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
        if (mCurState != State.PlayerTurn) {
            Debug.LogError("State Mismatch");
        }
        UpdateCurState();
    }

    #endregion

    #region Process Skills

    public void processSkill(Skill skill) {
        List<Buff> activeBuffs = playerStatus.getActiveBuffs();
        switch (skill.getSkillType()) {
            case SkillType.ATTACK:
                AttackSkill atkSkill = (AttackSkill)skill;
                float effectiveDamage = atkSkill.getAttackSkillDamage(playerStatus, enemyStatus);
                foreach (Buff buff in activeBuffs) {
                    if (buff.GetBuffId() == Buff.BuffId.BONUS_DAMAGE) {
                        effectiveDamage += buff.GetBounusDamage();
                    }
                }
                enemyStatus.TakeDamage(effectiveDamage);
                foreach (Buff buff in activeBuffs) {
                    if (buff.GetBuffId() == Buff.BuffId.LIFE_STEAL) {
                        playerStatus.ProcessHealing(playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) * effectiveDamage);
                    }
                }
                if (skill.GetSkillAttribute() == SkillAttribute.ANGRY) {
                    playerStatus.TakeDamage(playerStatus.getATKbyAttribute(SkillAttribute.ANGRY), SkillAttribute.ANGRY);
                }
                break;
            case SkillType.DEFENSE:
                switch (skill.GetSkillAttribute())
                {
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
                switch (skill.GetSkillAttribute())
                {
                    case SkillAttribute.HAPPY:
                        playerStatus.activateBuff(new Buff(Buff.BuffId.LIFE_STEAL));
                        break;
                    case SkillAttribute.SAD:
                        enemyStatus.activateBuff(new Buff(Buff.BuffId.PURGE));
                        break;
                    case SkillAttribute.ANGRY:
                        Buff bounusDamage = new Buff(Buff.BuffId.BONUS_DAMAGE);
                        float rand = Random.Range(0.0f, 1.0f);
                        bounusDamage.GenerateBounusDamage(playerStatus, rand);
                        playerStatus.activateBuff(bounusDamage);
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

    public void processDefenseSkill()
    {
        DefenseSkill defenseSkill = (DefenseSkill)playerStatus.GetSkills()[1];
        SkillAttribute attribute = defenseSkill.GetSkillAttribute();

        // process defense skill

        mCurState = State.EnemyTurn;
    }

    public void processEffectSkill()
    {
        Skill skill = playerStatus.GetSkills()[2];
        SkillType type = skill.getSkillType();
        switch (type)
        {
            case SkillType.BUFF:
                processBuffSkill((BuffSkill)skill);
                break;
            case SkillType.DEBUFF:
                processDebuffSkill((DebuffSkill)skill);
                break;
            default:
                break;
        }

        mCurState = State.EnemyTurn;
    }

    void processBuffSkill(BuffSkill skill)
    {
        // process buff skill
    }

    void processDebuffSkill(DebuffSkill skill)
    {
        // process debuff skill
    }
    #endregion

    #region Update Equipment UI
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

    public void updatePlayerStatVisual()
    {
        string happyStat = "HappyATK: " + playerStatus.getHappyATK() + "\n" + "HappyDEF: " + playerStatus.getHappyDEF() + "\n";
        string angryStat = "AngryATK: " + playerStatus.getAngryATK() + "\n" + "AngryDEF: " + playerStatus.getAngryDEF() + "\n";
        string sadStat = "SadATK: " + playerStatus.getSadATK() + "\n" + "SadDEF: " + playerStatus.getSadDEF() + "\n";
        playerStatsUI.GetComponent<TextMeshProUGUI>().text = happyStat + angryStat + sadStat;
    }
    #endregion
}