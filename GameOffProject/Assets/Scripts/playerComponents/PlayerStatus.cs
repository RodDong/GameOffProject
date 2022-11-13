using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Eye;
using static EyeBrow;
using static Mouth;

public class PlayerStatus : MonoBehaviour
{
    public const float MAX_HEALTH = 120;
    private float currentHealth;
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
    private List<Skill> skills;
    private EyeBrow equippedEyebrow;
    private Eye equippedEyes;
    private Mouth equippedMouth;
    private List<EyeBrow> ownedEyebrows = new List<EyeBrow>();
    private List<Eye> ownedEyes = new List<Eye>();
    private List<Mouth> ownedMouth = new List<Mouth>();

    private void Awake() {
        equippedEyebrow = new EyeBrow(EyeBrowId.TEST_EYEBROW_1);
        equippedEyes = new Eye(EyeId.TEST_EYE_1);
        equippedMouth = new Mouth(MouthId.TEST_MOUTH_1);

        ownedEyebrows.Add(equippedEyebrow);
        ownedEyes.Add(equippedEyes);
        ownedMouth.Add(equippedMouth);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
        // initialize eyes, eyebrow, mouth
        // initialize skills based on equipments.
    }

    public float getHappyATK() {
        return happyATK;
    }
    public float getHappyDEF() {
        return happyDEF;
    }
    public float getSadATK() {
        return sadATK;
    }
    public float getSadDEF() {
        return sadDEF;
    }
    public float getAngryATK() {
        return angryATK;
    }
    public float getAngryDEF() {
        return angryDEF;
    }

    public float getMaxHealth() {
        return MAX_HEALTH;
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

    public void addEyebrow(EyeBrowId id) {
        ownedEyebrows.Add(new EyeBrow(id));
    }

    public void addEye(EyeId id) {
        ownedEyes.Add(new Eye(id));
    }

    public void addMouth(MouthId id) {
        ownedMouth.Add(new Mouth(id));
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
}
