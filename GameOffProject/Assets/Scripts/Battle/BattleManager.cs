using System.Collections;
using System.Collections.Generic;
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
        maxHealth = playerStatus.getMaxHealth();
        curHealth = maxHealth;

        // initialize enemy status
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();

    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateHealth();
        UpdateCurState();
    }

    void UpdateCurState()
    {
        if (curHealth <= 0.0f)
        {
            mCurState = State.Death;
        }

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

    void UpdateHealth()
    {
        healthBar.GetComponent<Slider>().value = curHealth / maxHealth;
    }

    void RegenerateHealth(float heal)
    {
        curHealth = Mathf.Min(curHealth + heal, maxHealth);
    }

    void TakeDamage(float damage)
    {
        curHealth -= damage;
    }

    void UpdateWin()
    {
        gamObjectsInScene.SetActive(true);
        battleUI.SetActive(false);
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
}
