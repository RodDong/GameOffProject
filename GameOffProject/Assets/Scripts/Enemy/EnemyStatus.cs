using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Effect;

public abstract class EnemyStatus: MonoBehaviour
{
    protected float MAX_HEALTH;
    protected float currentHealth;

    public float GetCurrentHealth() {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return MAX_HEALTH;
    }

    public void ResetCurrentHealth()
    {
        if(this.GetType() == typeof(Chef))
        {
            Chef thisEnemy = (Chef)this;
            thisEnemy.ResetChefPhase();
        }
        currentHealth = MAX_HEALTH;
    }
    protected float happyATK;
    protected float happyDEF;
    protected float sadATK;
    protected float sadDEF;
    protected float angryATK;
    protected float angryDEF;
    protected int hitsTakenCounter;
    protected int attackCounter;
    protected List<Effect> Effects = new List<Effect>();

    public List<Effect> GetActiveEffects() { return Effects; }

    // process round counters for Effects and deEffects
    public void UpdateEffectStatus() {
        for (int i = Effects.Count - 1; i >= 0; i--) {
            if (Effects[i].decreaseCounter()) {
                Effects.RemoveAt(i);
            }
        }
    }

    public bool ActivateEffect(Effect effect) {
        
        for (int i = 0; i < Effects.Count; i++) {
            if (Effects[i].GetEffectId() == effect.GetEffectId()) {
                Effects[i].resetDuration();
                return true;
            }
        }
        Effects.Add(effect);
        return false;
    }

    public void ClearEffect() {
        Effects.Clear();
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
        if (Effects.Contains(new Effect(Effect.EffectId.IMMUNE)) && attribute != SkillAttribute.NONE) {
            return 0;
        }
        
        float effectiveDamage = damage * (50f / (50f + getDEFbyAttribute(attribute)));
        currentHealth -= effectiveDamage;
        if (currentHealth <= 0) {
            currentHealth = 0;
        }

        Debug.Log("Damage taken by enemy: " + effectiveDamage);
        return effectiveDamage * Random.Range(0.95f, 1.05f);
    }

    public void ProcessHealing(float healAmount) {
        currentHealth += healAmount;
        if (currentHealth > MAX_HEALTH) {
            currentHealth = MAX_HEALTH;
        }
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
        if (Effects.Contains(new Effect(EffectId.FORTIFIED))) {
            // temp value for testing
            fortifiedDEF += 50.0f;
        }

        float baseDEF;
        switch(attribute) {
            case SkillAttribute.HAPPY:
                return happyDEF + fortifiedDEF;
            case SkillAttribute.SAD:
                return sadDEF + fortifiedDEF;
            case SkillAttribute.ANGRY:
                return angryDEF + fortifiedDEF;
            default:
                baseDEF = 0.0f;
                break;
        }

        return baseDEF + fortifiedDEF;
    }
    //return the skill attribute that was used
    public abstract (string, string, string) MakeMove(PlayerStatus playerStatus);

    public virtual void DealDamage(PlayerStatus playerStatus, float damage, SkillAttribute attribute) {
        Effect blind = Effects.Find((Effect b) => { return b.GetEffectId() == EffectId.BLIND; });
        if (blind != null && Random.Range(0f, 1f) < blind.GetBlindPercentage()) {
            return; // MISS
        }
        // if has bonusDMG by chaos, add to original damage
        Effect bonusDMG = Effects.Find((Effect b) => { return b.GetEffectId() == EffectId.BONUS_DAMAGE; });
        if (bonusDMG != null)
        {
            damage += bonusDMG.GetBounusDamage();
        }
        
        Effect reductionEffect = Effects.Find(element => element.GetEffectId() == EffectId.REDUCED);
        if (reductionEffect != null) {
            damage -= reductionEffect.GetAttackReduction(attribute);
        }
        
        float dealtDamage = playerStatus.TakeDamage(damage, attribute);
        // if has lifesteal by effect of chaos, heal percentage is based on player stats
        foreach (Effect Effect in Effects)
        {
            if (Effect.GetEffectId() == EffectId.LIFE_STEAL)
            {
                // the denominator can be adjusted later depending on stats and life steal ratio
                ProcessHealing((playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) / 100.0f) * dealtDamage);
            }
        }
        if (playerStatus.GetActiveEffects().Contains(new Effect(EffectId.REFLECT))){
            TakeDamage(damage, attribute);
        }
    }

    [Header("Ink JSON")]
    [SerializeField] public TextAsset defeatInkJSON;

    protected List<Item> dropItems = new List<Item>();
    public void DropItems(PlayerStatus playerStatus) {
        foreach (Item item in dropItems) {
            playerStatus.AddItem(item);
        }
    } 

    protected string imgRoot = "Art/Tachies/";
    public string enemyImage;
}
