using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;
using static PlayerStatus;
using static Buff;

public abstract class EnemyStatus: MonoBehaviour
{
    protected float MAX_HEALTH;
    protected float currentHealth;
    protected float happyATK;
    protected float happyDEF;
    protected float sadATK;
    protected float sadDEF;
    protected float angryATK;
    protected float angryDEF;
    protected int hitsTakenCounter;
    protected int attackCounter;
    protected List<Buff> buffs;

    // process round counters for buffs and debuffs
    public void UpdateEffectStatus() {
        for (int i = buffs.Count - 1; i >= 0; i--) {
            if (buffs[i].decreaseCounter()) {
                buffs.RemoveAt(i);
            }
        }
    }

    public bool ActivateBuff(Buff buff) {
        //play buff animation here ??? 

        //purge clears all buff/debuff and exits
        if (buff.GetBuffId == BuffId.PURGE) {
            PlayerStatus.ClearBuff();
            EnemyStatus.ClearBuff();
            return false;
        }
        for (int i = 0; i < buffs.Count; i++) {
            if (buffs[i].GetBuffId() == buff.GetBuffId()) {
                buffs[i].resetDuration();
                return true;
            }
        }
        buffs.Insert(0, buff);
        return false;
    }

    public void ClearBuff() {
        buffs.Clear();
    }

    private void Awake() {
        // for test purposes -
        MAX_HEALTH = 500.0f;
        currentHealth = MAX_HEALTH;
        happyATK = 50.0f;
        happyDEF = 50.0f;
        sadATK = 50.0f;
        sadDEF = 50.0f;
        angryATK = 50.0f;
        angryDEF = 50.0f;
        // - for test purposes
    }

     public float TakeDamage(float damage, SkillAttribute type) {
        // if immune, takes no damage, 
        // unless attribute is NONE, which means it is the effect of using immune
        if (buffs.Contains(new Buff(Buff.BuffId.IMMUNE)) && type != SkillAttribute.NONE) {
            return 0;
        }
        
        // if reflect, takes no damage, and deal damage to opponent 
        // NOT of same value since player DEF is different from enemy
        // unless attribute is NONE, which means it is the effect of using reflect
        if (buffs.Contains(new Buff(Buff.BuffId.REFLECT)) && type != SkillAttribute.NONE) {
            PlayerStatus.TakeDamage(damage, type);
        }
        

        float effectiveDamage = damage * (50f / (50f + getDEFbyAttribute(type)));
        currentHealth -= effectiveDamage;
        if (currentHealth <= 0) {
            currentHealth = 0;
        }
        return effectiveDamage;
    }

    public float getATKbyAttribute(SkillAttribute attribute) {
        switch(attribute) {
            case SkillAttribute.HAPPY:
                return happyATK;
            case SkillAttribute.SAD:
                return sadATK;
            case SkillAttribute.ANGRY:
                return angryATK;
            default:
                return 0.0f;
        }
    }

    public float getDEFbyAttribute(SkillAttribute attribute) {
        
        float fortifiedDEF = 0; 
        if (buffs.Contains(new Buff(BuffId.FORTIFIED))) {
            // temp value for testing
            fortifiedDEF += 100.0f;
        }

        switch(attribute) {
            case SkillAttribute.HAPPY:
                return happyDEF;
            case SkillAttribute.SAD:
                return sadDEF;
            case SkillAttribute.ANGRY:
                return angryDEF;
            default:
                return 0.0f + fortifiedDEF;
        }
    }

    public abstract void MakeMove(PlayerStatus playerStatus);

    public void DealDamage(PlayerStatus playerStatus, float damage, SkillAttribute attribute) {

        Buff blind = buffs.Find((Buff b) => { return b.GetBuffId() == Buff.BuffId.BLIND; });
        if (blind != null && Random.Range(0f, 1f) < blind.GetBlindPercentage()) {
            return; // MISS
        }
        playerStatus.TakeDamage(damage, SkillAttribute.HAPPY);
        if (playerStatus.GetActiveBuffs().Contains(new Buff(Buff.BuffId.REFLECT))) {
            TakeDamage(damage, SkillAttribute.HAPPY);
        }
    }
}
