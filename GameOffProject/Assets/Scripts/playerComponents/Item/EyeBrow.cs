using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DefenseSkill;

public class EyeBrow : Item
{

    public EyeBrow(SkillAttribute attribute) {
        this.attribute = attribute;
        itemType = "EyeBrow";
        
        switch (attribute) {
            case SkillAttribute.NONE:
                happyATK = 0;
                happyDEF = 0;
                sadATK = 0;
                sadDEF = 0;
                angryATK = 0;
                angryDEF = 0;
                displayName = "test eyebrow";
                itemDescription = "test description";
                imageSrc = imgRoot + "1EyebrowDefault_N";
                highLightedImage = imgRoot + "1EyebrowDefault_H";
                selectedImage = imgRoot + "1EyebrowDefault_S";
                break;
            case SkillAttribute.HAPPY:
                happyATK = 0f;
                happyDEF = 50f;
                sadATK = 0f;
                sadDEF = 30f;
                angryATK = 0f;
                angryDEF = 10f;
                displayName = "test eyebrow happy";
                itemDescription = "test description";
                imageSrc = imgRoot + "1EyebrowH_N";
                highLightedImage = imgRoot + "1EyebrowH_H";
                selectedImage = imgRoot + "1EyebrowH_S";
                break;
            case SkillAttribute.SAD:
                happyATK = 0f;
                happyDEF = 0f;
                sadATK = 20f;
                sadDEF = 50f;
                angryATK = 0f;
                angryDEF = 10f;
                displayName = "test eyebrow sad";
                itemDescription = "test description";
                imageSrc = imgRoot + "1EyebrowS_N";
                highLightedImage = imgRoot + "1EyebrowS_H";
                selectedImage = imgRoot + "1EyebrowS_S";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 30f;
                happyDEF = 0f;
                sadATK = 30f;
                sadDEF = 0f;
                angryATK = 0f;
                angryDEF = 50f;
                displayName = "test eyebrow angry";
                imageSrc = imgRoot + "1EyebrowA_N";
                highLightedImage = imgRoot + "1EyebrowA_H";
                selectedImage = imgRoot + "1EyebrowA_S";
                break;
        }
        
        skill = new DefenseSkill(attribute);
    }
}