using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackSkill;

public class Eye : Item
{
    public Eye(SkillAttribute attribute) {
        this.attribute = attribute;
        itemType = "Eye";

        switch(attribute) {
            case SkillAttribute.NONE:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(SkillAttribute.NONE);
                displayName = "test eye";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeA_N";
                highLightedImage = imgRoot + "2EyeA_H";
                selectedImage = imgRoot + "2EyeA_S";
                skillImage = imgRoot + "EmptyButton/" + "atk_angry";
                break;
            case SkillAttribute.HAPPY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(SkillAttribute.HAPPY);
                displayName = "test eye happy";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeH_N";
                highLightedImage = imgRoot + "2EyeH_H";
                selectedImage = imgRoot + "2EyeH_S";
                skillImage = imgRoot + "EmptyButton/" + "atk_happy";
                break;
            case SkillAttribute.SAD:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(SkillAttribute.SAD);
                displayName = "test eye sad";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeS_N";
                highLightedImage = imgRoot + "2EyeS_H";
                selectedImage = imgRoot + "2EyeS_S";
                skillImage = imgRoot + "EmptyButton/" + "atk_sad";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new AttackSkill(SkillAttribute.ANGRY);
                displayName = "test eye angry";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeA_N";
                highLightedImage = imgRoot + "2EyeA_H";
                selectedImage = imgRoot + "2EyeA_S";
                skillImage = imgRoot + "EmptyButton/" + "atk_angry";
                break;
        }
    }
}
