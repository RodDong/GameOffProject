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
    private float happyATK;
    private float happyDEF;
    private float sadATK;
    private float sadDEF;
    private float angryATK;
    private float angryDEF;

    [SerializeField] GameObject Buttons;
    [SerializeField] Button buttonPrefab;
    [SerializeField] GameObject Description;
    [SerializeField] GameObject Stats;
    

    // a list of currently active BUFFs
    // ? a list of currently active DeBUFFs? Maybe put this on enemy status

    // Current Skills: List of size 3?4
    // idx 0: attack skill
    // idx 1: defense skill
    // idx 2: buff/debuff skill
   
    private List<Buff> buffs;
    private List<Debuff> debuffs; 

    // process round counters for buffs and debuffs
    public void updateBuffDebuffStatus() {
        for (int i = buffs.Count - 1; i >= 0; i--) {
            if (buffs[i].decreaseCounter()) {
                buffs.RemoveAt(i);
            }
        }
        for (int i = debuffs.Count - 1; i >= 0; i--) {
            if (debuffs[i].decreaseCounter()) {
                debuffs.RemoveAt(i);
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
        equippedEyebrow = new EyeBrow(EyeBrowId.TEST_EYEBROW_1);
        equippedEyes = new Eye(EyeId.TEST_EYE_1);
        equippedMouth = new Mouth(MouthId.TEST_MOUTH_1);

        ownedEyebrows.Add(equippedEyebrow);
        ownedEyes.Add(equippedEyes);
        ownedMouth.Add(equippedMouth);
        updateStatus();
        // - for test purposes
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
        // initialize eyes, eyebrow, mouth
        // initialize skills based on equipments.
    }


    void Update()
    {
        DisplayStats();
    }
    
    public bool TakeDamage(float damage, SkillAttribute type) {
        currentHealth -= damage / getDEFbyAttribute(type);
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

    public void DisplayStats()
    {
        string happyAttack = "HappyAttack: ";
        happyAttack += getHappyATK() + "\n";

        string angryAttack = "AngryAttack: ";
        angryAttack += getAngryATK() + "\n";

        string sadAttack = "SadAttack: ";
        sadAttack += getSadATK() + "\n";

        string happyDefense = "HappyDefense: ";
        happyDefense += getHappyDEF() + "\n";

        string angryDefense = "AngryDefense: ";
        angryDefense += getAngryDEF() + "\n";

        string sadDefense = "SadDefense: ";
        sadDefense += getSadDEF() + "\n";

        string stats = happyAttack + angryAttack + sadAttack + happyDefense + angryDefense + sadDefense;

        Stats.GetComponentInChildren<TextMeshProUGUI>().text = stats;
    }

    public void DisplayEyeBrowInv()
    {
        float posX = -200.0f;
        float posY = -30.0f;
        for (int i = 0; i < ownedEyebrows.Count; i++)
        {
            Button tempButton = Button.Instantiate(buttonPrefab);
            Transform tempButtonTrans = tempButton.GetComponent<Transform>();
            tempButtonTrans.SetParent(Buttons.transform);
            tempButtonTrans.localPosition = new Vector3(posX, posY, 0.0f);
            tempButtonTrans.localScale = new Vector3(1.25f,1.25f,0);
            Item curItem = ownedEyebrows[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem);});
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedEyebrows[i].getDisplayName();
            posY -= 60.0f;
        }
    }

    public void DisplayEyesInv()
    {
        float posX = -200.0f;
        float posY = -30.0f;
        for (int i = 0; i < ownedEyes.Count; i++)
        {
            Button tempButton = Button.Instantiate(buttonPrefab);
            Transform tempButtonTrans = tempButton.GetComponent<Transform>();
            tempButtonTrans.SetParent(Buttons.transform);
            tempButtonTrans.localPosition = new Vector3(posX, posY, 0.0f);
            tempButtonTrans.localScale = new Vector3(1.25f,1.25f,0);
            Item curItem = ownedEyes[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem);});
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedEyes[i].getDisplayName();
            posY -= 60.0f;
        }
    }

    public void DisplayMouthInv()
    {
        float posX = -200.0f;
        float posY = -30.0f;
        for (int i = 0; i < ownedMouth.Count; i++)
        {
            Button tempButton = Button.Instantiate(buttonPrefab);
            Transform tempButtonTrans = tempButton.transform;
            tempButtonTrans.SetParent(Buttons.transform);
            tempButtonTrans.localPosition = new Vector3(posX, posY, 0.0f);
            tempButtonTrans.localScale = new Vector3(1.25f,1.25f,0);
            Item curItem = ownedMouth[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem); });
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedMouth[i].getDisplayName();
            posY -= 60.0f;
        }
    }

    public void DisplayDescription(Item item)
    {
        Description.GetComponent<TextMeshProUGUI>().text = item.getDescription();
    }
}
