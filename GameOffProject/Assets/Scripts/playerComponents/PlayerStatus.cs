using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static Eye;
using static EyeBrow;
using static Mouth;
using static Skill;

public class PlayerStatus : MonoBehaviour
{
    public const float MAX_HEALTH = 120;
    private float currentHealth;
    public float getCurrentHealth() {
        return currentHealth;
    } 
    private float happyATK;
    private float happyDEF;
    private float sadATK;
    private float sadDEF;
    private float angryATK;
    private float angryDEF;
    

    // a list of currently active BUFFs
    // ? a list of currently active DeBUFFs? Maybe put this on enemy status

    // Current Skills: List of size 3?4
    // idx 0: attack skill
    // idx 1: defense skill
    // idx 2: buff/debuff skill
   
    private List<Buff> buffs;
    public List<Buff> getActiveBuffs() {
        return buffs;
    } 

    // process round counters for buffs and debuffs
    public void updateBuffDebuffStatus() {
        for (int i = buffs.Count - 1; i >= 0; i--) {
            if (buffs[i].decreaseCounter()) {
                buffs.RemoveAt(i);
            }
        }
    }

    public bool activateBuff(Buff buff) {
        for (int i = 0; i < buffs.Count; i++) {
            if (buffs[i].GetBuffId() == buff.GetBuffId()) {
                buffs[i].resetDuration();
                return true;
            }
        }
        buffs.Insert(0, buff);
        return false;
    }

    public void clearBuff() {
        buffs.Clear();
    }

    private List<Skill> skills;
    public List<Skill> GetSkills() {return skills;}

    private EyeBrow equippedEyebrow;
    private Eye equippedEyes;
    private Mouth equippedMouth;
    private List<EyeBrow> ownedEyebrows = new List<EyeBrow>();
    private List<Eye> ownedEyes = new List<Eye>();
    private List<Mouth> ownedMouth = new List<Mouth>();

    private List<Button> skillSet = new List<Button>();

    private void Awake() {
        // for test purposes -
        equippedEyebrow = new EyeBrow(SkillAttribute.NONE);
        equippedEyes = new Eye(SkillAttribute.NONE);
        equippedMouth = new Mouth(SkillAttribute.NONE);

        ownedEyebrows.Add(equippedEyebrow);
        ownedEyebrows.Add(new EyeBrow(SkillAttribute.HAPPY));
        ownedEyebrows.Add(new EyeBrow(SkillAttribute.SAD));
        ownedEyes.Add(equippedEyes);
        ownedMouth.Add(equippedMouth);
        updateStatus();
        // - for test purposes

        //initialize all clues as unfound
        for (int i = 0; i < clueNumbers; i++)
        {
            allClues.Add(new Clue(-1));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
        // initialize eyes, eyebrow, mouth
        // initialize skills based on equipments.
    }
    
    public bool TakeDamage(float damage, SkillAttribute type) {
        // if immune, takes no damage, 
        // unless attribute is NONE, which means it is the effect of using immune
        if (buffs.Contains(new Buff(Buff.BuffId.IMMUNE)) && type != SkillAttribute.NONE) {
            return false;
        }
        
        currentHealth -= damage * (50f / (50f + getDEFbyAttribute(type)));
        if (currentHealth <= 0) {
            currentHealth = 0;
            return true;
        }
        return false;
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
        switch(attribute) {
            case SkillAttribute.HAPPY:
                return happyDEF;
            case SkillAttribute.SAD:
                return sadDEF;
            case SkillAttribute.ANGRY:
                return angryDEF;
            default:
                return 0.0f;
        }
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

    public void setEquippedMouth(Mouth m) {
        equippedMouth = m;
    }

    public void setEquippedEyeBrow(EyeBrow eb) {
        equippedEyebrow = eb;
    }

    public void setEquippedEyes(Eye e) {
        equippedEyes = e;
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

    public void updateStatus() {
        setHappyATK(equippedEyebrow.getHappyATK() + equippedEyes.getHappyATK() + equippedMouth.getHappyATK());
        setHappyDEF(equippedEyebrow.getHappyDEF() + equippedEyes.getHappyDEF() + equippedMouth.getHappyDEF());
        setSadATK(equippedEyebrow.getSadATK() + equippedEyes.getSadATK() + equippedMouth.getSadATK());
        setSadDEF(equippedEyes.getSadDEF() + equippedEyes.getSadDEF() + equippedMouth.getSadDEF());
        setAngryATK(equippedEyebrow.getAngryATK() + equippedEyes.getAngryATK() + equippedMouth.getAngryATK());
        setAngryDEF(equippedEyebrow.getAngryDEF() + equippedEyes.getAngryDEF() + equippedMouth.getAngryDEF());

        List<Skill> newSkills = new List<Skill>();
        // idx 0: attack skill
        newSkills.Add(equippedEyes.getSkill());
        // idx 1: defense skill
        newSkills.Add(equippedEyebrow.getSkill());
        // idx 2: buff/debuff skill
        newSkills.Add(equippedMouth.getSkill());
        setSkills(newSkills);
    }

    //Clues
    [SerializeField]
    private int clueNumbers;
    private List<Clue> allClues = new List<Clue>(); //clueID = -1 means not found
    public List<Clue> playerClues { get { return allClues;} }
    public void findClue(int id)
    {
        if (id < clueNumbers)
            allClues[id] = new Clue(id);
        else
            Debug.LogError("ID exceed total number!");
    }
}
