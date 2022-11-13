using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackSkill;
public class Skill : MonoBehaviour
{
    protected enum SkillType {
        ATTACK,
        DEFENSE,
        BUFF,
        DEBUFF
    }
    protected enum SkillAttribute {
        HAPPY,
        ANGRY,
        SAD
    }
    protected SkillType type;
    protected SkillAttribute attribute;
    protected float power;
}