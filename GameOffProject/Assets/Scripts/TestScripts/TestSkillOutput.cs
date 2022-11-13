using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;
using static AttackSkill;

public class TestSkillOutput : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private EnemyStatus enemyStatus;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testAttackSkillOutput() {
        AttackSkill testPlayerAttackSkill = (AttackSkill) playerStatus.GetSkills()[0];
        SkillAttribute testAttribute = testPlayerAttackSkill.GetSkillAttribute();
        float testPlayerATK = playerStatus.getATKbyAttribute(testAttribute);
        float testTargetDEF = enemyStatus.getDEFbyAttribute(testAttribute);
        Debug.Log("The attack damge is: " + testPlayerAttackSkill.getAttackSkillDamage(testPlayerATK, testTargetDEF));
    }
}
