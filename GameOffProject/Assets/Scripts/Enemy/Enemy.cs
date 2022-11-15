using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;

public class Enemy: MonoBehaviour
{
    protected float MAX_HEALTH;
    protected float health;
    protected float happyATK;
    protected float happyDEF;
    protected float sadATK;
    protected float sadDEF;
    protected float angryATK;
    protected float angryDEF;
    protected int hitsTakenCounter;
    protected int attackCounter;
    protected List<Buff> buffs;
    protected List<Debuff> debuffs;
    protected List<Skill> skills;
}