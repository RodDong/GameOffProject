using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

     public virtual float TakeDamage(float damage, SkillAttribute attribute) {
        // if immune, takes no damage, 
        // unless attribute is NONE, which means it is the effect of using immune
        if (buffs.Contains(new Buff(Buff.BuffId.IMMUNE)) && attribute != SkillAttribute.NONE) {
            return 0;
        }
        
        float effectiveDamage = damage * (50f / (50f + getDEFbyAttribute(attribute)));
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

        float baseDEF;
        switch(attribute) {
            case SkillAttribute.HAPPY:
                baseDEF = happyDEF;
                break;
            case SkillAttribute.SAD:
                baseDEF = sadDEF;
                break;
            case SkillAttribute.ANGRY:
                baseDEF = angryDEF;
                break;
            default:
                baseDEF = 0.0f;
                break;
        }

        return baseDEF + fortifiedDEF;
    }

    public abstract void MakeMove(PlayerStatus playerStatus);

    public virtual void DealDamage(PlayerStatus playerStatus, float damage, SkillAttribute attribute) {
        Buff blind = buffs.Find((Buff b) => { return b.GetBuffId() == BuffId.BLIND; });
        if (blind != null && Random.Range(0f, 1f) < blind.GetBlindPercentage()) {
            return; // MISS
        }
        playerStatus.TakeDamage(damage, attribute);
        if (playerStatus.GetActiveBuffs().Contains(new Buff(BuffId.REFLECT))){
            TakeDamage(damage, attribute);
        }
    }
}
