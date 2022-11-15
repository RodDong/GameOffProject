using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefenseSkill;

public class EyeBrow : Item
{
    public enum EyeBrowId {
        TEST_EYEBROW_1,
        TEST_EYEBROW_HAPPY,
        TEST_EYEBROW_SAD,
        TEST_EYEBROW_ANGRY
    }
    private EyeBrowId id;

    public bool isEqual(EyeBrow e1, EyeBrow e2)
    {
        return e1.getID() == e2.getID();
    }

    public EyeBrow(EyeBrowId id) {
        this.id = id;
        itemType = "EyeBrow";
        
        switch (id) {
            case EyeBrowId.TEST_EYEBROW_1:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new DefenseSkill(DefenseSkillId.TEST_DEFENSE_SKILL_1);
                displayName = "test eyebrow";
                itemDescription = "test description";
                imageSrc = imgRoot + "1EyebrowA_N";
                highLightedImage = imgRoot + "1EyebrowA_H";
                selectedImage = imgRoot + "1EyebrowA_S";
                break;
            case EyeBrowId.TEST_EYEBROW_HAPPY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new DefenseSkill(DefenseSkillId.TEST_DEFENSE_SKILL_1);
                displayName = "test eyebrow happy";
                break;
            case EyeBrowId.TEST_EYEBROW_SAD:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new DefenseSkill(DefenseSkillId.TEST_DEFENSE_SKILL_1);
                displayName = "test eyebrow sad";
                break;
            case EyeBrowId.TEST_EYEBROW_ANGRY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new DefenseSkill(DefenseSkillId.TEST_DEFENSE_SKILL_1);
                displayName = "test eyebrow angry";
                break;
        }
    }
}