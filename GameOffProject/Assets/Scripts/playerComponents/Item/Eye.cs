using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackSkill;

public class Eye : Item
{
    public enum EyeId {
        TEST_EYE_1,
        TEST_EYE_HAPPY,
        TEST_EYE_SAD,
        TEST_EYE_ANGRY
    }
    private EyeId id;

    public bool isEqual(Eye e1, Eye e2)
    {
        return e1.getID() == e2.getID();
    }

    public Eye(EyeId id) {
        this.id = id;
        itemType = "Eye";

        switch(id) {
            case EyeId.TEST_EYE_1:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(AttackSkillId.TEST_ATTACK_SKILL_1);
                displayName = "test eye";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeA_N";
                highLightedImage = imgRoot + "2EyeA_H";
                selectedImage = imgRoot + "2EyeA_S";
                break;
            case EyeId.TEST_EYE_HAPPY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(AttackSkillId.TEST_ATTACK_SKILL_HAPPY);
                displayName = "test eye happy";
                break;
            case EyeId.TEST_EYE_SAD:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(AttackSkillId.TEST_ATTACK_SKILL_SAD);
                displayName = "test eye sad";
                break;
            case EyeId.TEST_EYE_ANGRY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(AttackSkillId.TEST_ATTACK_SKILL_ANGRY);
                displayName = "test eye angry";
                break;
        }
    }
}
