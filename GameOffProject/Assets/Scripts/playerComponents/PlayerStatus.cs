using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static Eye;
using static EyeBrow;
using static Mouth;
using static Skill;
using static Effect;

public class PlayerStatus : MonoBehaviour
{
    private float MAX_HEALTH = 120;
    public float GetMaxHealth() { return MAX_HEALTH; }
    private float currentHealth;
    private BattleManager battleManager;
    public void SetBattleManager(BattleManager bm) { battleManager = bm; }
    public float GetCurrentHealth() {
        return currentHealth;
    } 

    public void ResetCurrentHealth()
    {
        currentHealth = MAX_HEALTH;
    }

    private float happyATK;
    private float happyDEF;
    private float sadATK;
    private float sadDEF;
    private float angryATK;
    private float angryDEF;
    

    // a list of currently active Effects
    // ? a list of currently active DeEffects? Maybe put this on enemy status

    // Current Skills: List of size 3?4
    // idx 0: attack skill
    // idx 1: defense skill
    // idx 2: Effect/deEffect skill
   
    private List<Effect> effects = new List<Effect>();
    public List<Effect> GetActiveEffects() {
        return effects;
    } 

    // process round counters for Effects and deEffects
    public void UpdateEffectStatus() {
        for (int i = effects.Count - 1; i >= 0; i--) {
            if (effects[i].decreaseCounter()) {
                if (effects[i].GetEffectId() == EffectId.MUTE)
                {
                    battleManager.UnMute();
                }

                if (effects[i].GetEffectId() == EffectId.SILENCED)
                {
                    battleManager.UnSlience();
                }

                if (effects[i].GetEffectId() == EffectId.TAUNTED) {
                    battleManager.UnTaunted();
                }

                if(effects[i].GetEffectId() == EffectId.DISMEMBERED)
                {
                    MAX_HEALTH = 120;
                }

                effects.RemoveAt(i);
            }
        }
    }

    public bool ActivateEffect(Effect effect) {
        //play Effect animation here ??? 
        for (int i = 0; i < effects.Count; i++) {
            if (effects[i].GetEffectId() == effect.GetEffectId()) {
                effects[i].resetDuration();
                return true;
            }
        }

        if(effect.GetEffectId() == EffectId.MUTE)
        {
            battleManager.ProcessMute();
        }

        if (effect.GetEffectId() == EffectId.SILENCED)
        {
            battleManager.ProcessSilence();
        }

        if (effect.GetEffectId() == EffectId.TAUNTED)
        {
            battleManager.ProcessTaunted();
        }

        if (effect.GetEffectId() == EffectId.DISMEMBERED)
        {
            MAX_HEALTH = 80;
            currentHealth /= 1.5f;
        }

        effects.Add(effect);
        return false;
    }

    public void ClearEffect() {
        effects.Clear();
    }

    private List<Skill> skills;
    public List<Skill> GetSkills() {return skills;}

    private EyeBrow equippedEyebrow;
    private Eye equippedEyes;
    private Mouth equippedMouth;
    private List<EyeBrow> ownedEyebrows = new List<EyeBrow>();
    private List<Eye> ownedEyes = new List<Eye>();
    private List<Mouth> ownedMouth = new List<Mouth>();

    public void AddItem(Item item) {
        if (item is EyeBrow) {
            if (ownedEyebrows.Contains(item as EyeBrow)) {
                Debug.LogError("Already Owned this Item");
                return;
            }
            ownedEyebrows.Add(item as EyeBrow);
        } else if (item is Eye) {
            if (ownedEyes.Contains(item as Eye)) {
                Debug.LogError("Already Owned this Item");
                return;
            }
            ownedEyes.Add(item as Eye); 
        } else if (item is Mouth) {
            if (ownedMouth.Contains(item as Mouth)) {
                Debug.LogError("Already Owned this Item");
                return;
            }
            ownedMouth.Add(item as Mouth);
        }
    }

    private List<Button> skillSet = new List<Button>();

    private void Awake() {
        // for test purposes -
        equippedEyebrow = new EyeBrow(SkillAttribute.NONE);
        equippedEyes = new Eye(SkillAttribute.NONE);
        equippedMouth = new Mouth(SkillAttribute.NONE);

        ownedEyebrows.Add(equippedEyebrow);
        ownedEyebrows.Add(new EyeBrow(SkillAttribute.HAPPY));
        ownedEyes.Add(equippedEyes);
        // ownedEyes.Add(new Eye(SkillAttribute.HAPPY));
        // ownedEyes.Add(new Eye(SkillAttribute.SAD));
        // ownedEyes.Add(new Eye(SkillAttribute.ANGRY));
        ownedMouth.Add(equippedMouth);
        // ownedMouth.Add(new Mouth(SkillAttribute.HAPPY));
        // ownedMouth.Add(new Mouth(SkillAttribute.SAD));
        // ownedMouth.Add(new Mouth(SkillAttribute.ANGRY));

        updateStatus();

        for (int i = 0; i <= 12; i++) {
            ownedClues.Add(new Clue(i));
        }
        // Effects.Add(new Effect(EffectId.BLIND));
        // Effects.Add(new Effect(EffectId.POISON));
        // Effects.Add(new Effect(EffectId.IMMUNE));
        // - for test purposes
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
        // initialize eyes, eyebrow, mouth
        // initialize skills based on equipments.
    }
    
    public float TakeDamage(float damage, SkillAttribute attribute) {
        // TODO: animations

        // if immune, takes no damage, 
        // unless attribute is NONE, which means it is the effect of using immune
        if (effects.Contains(new Effect(EffectId.IMMUNE)) && attribute != SkillAttribute.NONE) {
            return 0;
        }

        // if break, deal true damage
        if (effects.Contains(new Effect(EffectId.BROKEN))) {
            currentHealth -= damage;
            if (currentHealth <= 0) {
                currentHealth = 0;
            }
            return damage;
        }
        
        float effectiveDamage = damage * (50f / (50f + getDEFbyAttribute(attribute)));
        currentHealth -= effectiveDamage;
        if (currentHealth <= 0) {
            currentHealth = 0;
        }

        if (GetActiveEffects().Contains(new Effect(EffectId.FRAGILE))) {
            effectiveDamage *= 1.2f;
        }

        Debug.Log("Damage taken by player: " + effectiveDamage);
        return effectiveDamage * Random.Range(0.95f, 1.05f);
    }

    public void ProcessHealing(float healAmount) {
        Effect healReductionEffect = effects.Find(element => element.GetEffectId() == EffectId.HEALREDUCTION);
        if (healReductionEffect != null) {
            healAmount *= 0.8f;
        }

        currentHealth += healAmount;
        if (currentHealth > MAX_HEALTH) {
            currentHealth = MAX_HEALTH;
        }
    }

    public float getATKbyAttribute(SkillAttribute attribute) {
        float reduction = 0;
        
        Effect reductionEffect = effects.Find(element => element.GetEffectId() == EffectId.REDUCED);
        if (reductionEffect != null) {
            reduction = reductionEffect.GetAttackReduction(attribute);
        }

        float stolen = 0;
        Effect stolenEffect = effects.Find(element => element.GetEffectId() == EffectId.STOLEN);
        if (stolenEffect != null) {
            stolen = stolenEffect.GetStolenAmount(attribute);
        }

        float attributeFromItem;
        switch(attribute) {
            case SkillAttribute.HAPPY:
                attributeFromItem = happyATK;
                break;
            case SkillAttribute.SAD:
                attributeFromItem = sadATK;
                break;
            case SkillAttribute.ANGRY:
                attributeFromItem = angryATK;
                break;
            default:
                attributeFromItem = 0.0f;
                break;
        }

        return attributeFromItem - reduction - stolen;
    }

    public float getDEFbyAttribute(SkillAttribute attribute) {
        if (effects.Contains(new Effect(EffectId.BROKEN))) {
            return 0;
        } 

        float reduction = 0;
        Effect reductionEffect = effects.Find(element => element.GetEffectId() == EffectId.WEAK);
        if (reductionEffect != null) {
            reduction = reductionEffect.GetDefenseReduction(attribute);
        }

        float stolen = 0;
        Effect stolenEffect = effects.Find(element => element.GetEffectId() == EffectId.STOLEN);
        if (stolenEffect != null) {
            stolen = stolenEffect.GetStolenAmount(attribute);
        }

        float fortifiedDEF = 0; 
        if (effects.Contains(new Effect(EffectId.FORTIFIED))) {
            // temp value for testing
            fortifiedDEF += 20.0f;
        }

        float attributeFromItem;
        switch(attribute) {
            case SkillAttribute.HAPPY:
                attributeFromItem = happyDEF;
                break;
            case SkillAttribute.SAD:
                attributeFromItem = sadDEF;
                break;
            case SkillAttribute.ANGRY:
                attributeFromItem = angryDEF;
                break;
            default:
                attributeFromItem = 0.0f;
                break;
        }

        return attributeFromItem - reduction - stolen + fortifiedDEF;
    }

    public void setHappyATK(float happyATK) {
        this.happyATK = happyATK;
    }

    public void setHappyDEF(float happyDEF) {
        this.happyDEF = happyDEF;
    }

    public void setSadATK(float sadATK) {
        this.sadATK = sadATK;
    }

    public void setSadDEF(float sadDEF) {
        this.sadDEF = sadDEF;
    }

    public void setAngryATK(float angryATK) {
        this.angryATK = angryATK;
    }

    public void setAngryDEF(float angryDEF) {
        this.angryDEF = angryDEF;
    }

    public void setSkills(List<Skill> skills) {
        this.skills = skills;
    }

    public EyeBrow getEquippedEyeBrow() {
        return equippedEyebrow;
    }

    public Eye getEquippedEyes() {
        return equippedEyes;
    }

    public Mouth getEquippedMouth() {
        return equippedMouth;
    }

    public void setEquippedMouth(Mouth equippedMouth) {
        this.equippedMouth = equippedMouth;
    }

    public void setEquippedEyeBrow(EyeBrow equippedEyebrow) {
        this.equippedEyebrow = equippedEyebrow;
    }

    public void setEquippedEyes(Eye equippedEyes) {
        this.equippedEyes = equippedEyes;
    }

    public void addEyebrow(SkillAttribute attribute) {
        ownedEyebrows.Add(new EyeBrow(attribute));
    }

    public void addEye(SkillAttribute attribute) {
        ownedEyes.Add(new Eye(attribute));
    }

    public void addMouth(SkillAttribute attribute) {
        ownedMouth.Add(new Mouth(attribute));
    }

    public List<EyeBrow> getOwnedEyeBrows() {
        return ownedEyebrows;
    }

    public List<Eye> getOwnedEyes() {
        return ownedEyes;
    }

    public List<Mouth> getOwnedMouths() {
        return ownedMouth;
    }

    public Effect GetEffect(EffectId id) {
        foreach (Effect effect in effects) {
            if (effect.GetEffectId() == id) {
                return effect;
            }
        }
        return new Effect(EffectId.NONE);
    }

    public void updateStatus() {

        //(TODO) UPDATE NEEDS TO CHECK EffectS 

        setHappyATK(equippedEyebrow.getHappyATK() + equippedEyes.getHappyATK() + equippedMouth.getHappyATK());
        setHappyDEF(equippedEyebrow.getHappyDEF() + equippedEyes.getHappyDEF() + equippedMouth.getHappyDEF());
        setSadATK(equippedEyebrow.getSadATK() + equippedEyes.getSadATK() + equippedMouth.getSadATK());
        setSadDEF(equippedEyebrow.getSadDEF() + equippedEyes.getSadDEF() + equippedMouth.getSadDEF());
        setAngryATK(equippedEyebrow.getAngryATK() + equippedEyes.getAngryATK() + equippedMouth.getAngryATK());
        setAngryDEF(equippedEyebrow.getAngryDEF() + equippedEyes.getAngryDEF() + equippedMouth.getAngryDEF());

        List<Skill> newSkills = new List<Skill>();
        // idx 0: attack skill
        newSkills.Add(equippedEyes.getSkill());
        // idx 1: defense skill
        newSkills.Add(equippedEyebrow.getSkill());
        // idx 2: Effect/deEffect skill
        newSkills.Add(equippedMouth.getSkill());
        setSkills(newSkills);
    }

    private List<Clue> ownedClues = new List<Clue>();
    public void addClue(int id) {
        ownedClues.Add(new Clue(id));
    }
    public Clue getClue(int id)
    {
        for (int i = 0; i < ownedClues.Count; i++) {
            if (ownedClues[i].getClueId() == id) {
                return ownedClues[i];
            }
        }
        return new Clue(-1);
    }

    public void updateMask()
    {
        battleManager.UpdateEquippedMask();
    }
}
