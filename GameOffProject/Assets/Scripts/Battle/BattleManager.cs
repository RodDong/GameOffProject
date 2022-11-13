using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //EnemyStatus enemyStatus;

    State mCurState;

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
        //enemyStatus = enemy.GetComponent<EnemyStatus>();

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

    public void UseSkill1()
    {
        Skill skill = playerStatus.GetSkills()[0];
    }

    public void UseSkill2()
    {

    }

    public void UseSkill3()
    {

    }
}
