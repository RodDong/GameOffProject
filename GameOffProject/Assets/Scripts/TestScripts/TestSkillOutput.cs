using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;
using static AttackSkill;
using TMPro;
using System;

public class TestSkillOutput : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private EnemyStatus enemyStatus;

    public GameObject damageDisplay;

    private void Start()
    {
        playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();
    }

    private void Update()
    {
        
    }

    public void testAttackSkillOutput()
    {
        AttackSkill testPlayerAttackSkill = (AttackSkill) playerStatus.GetSkills()[0];
        SkillAttribute testAttribute = testPlayerAttackSkill.GetSkillAttribute();
        float testPlayerATK = playerStatus.getATKbyAttribute(testAttribute);
        float testTargetDEF = enemyStatus.getDEFbyAttribute(testAttribute);
        float damage = testPlayerAttackSkill.getAttackSkillDamage(playerStatus);
        Debug.Log("You damage the enemy by: " + damage);
    }

    public void processSkill(Skill skill)
    {
        switch (skill.getSkillType()) {
            case SkillType.ATTACK:
                AttackSkill atkSkill = (AttackSkill)skill;
                enemyStatus.TakeDamage(atkSkill.getAttackSkillDamage(playerStatus), skill.GetSkillAttribute());
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
                        playerStatus.activateBuff(new Buff(Buff.BuffId.BONUS_DAMAGE));
                        enemyStatus.activateBuff(new Buff(Buff.BuffId.BLIND));
                        break;
                }
                break;
        }
    }

    // private void DisplayDamage(float damage)
    // {
    //     GameObject d = GameObject.Instantiate<GameObject>(damageDisplay);
    //     d.GetComponent<TextMeshPro>().text = Convert.ToString(damage);
    //     Destroy(d, 1);
    // }
}
