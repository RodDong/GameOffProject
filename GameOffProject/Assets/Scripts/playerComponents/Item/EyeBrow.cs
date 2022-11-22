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
                happyATK = 150f;
                happyDEF = 150f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test eyebrow happy";
                itemDescription = "test description";
                imageSrc = imgRoot + "1EyebrowH_N";
                highLightedImage = imgRoot + "1EyebrowH_H";
                selectedImage = imgRoot + "1EyebrowH_S";
                break;
            case SkillAttribute.SAD:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 150f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test eyebrow sad";
                itemDescription = "test description";
                imageSrc = imgRoot + "1EyebrowS_N";
                highLightedImage = imgRoot + "1EyebrowS_H";
                selectedImage = imgRoot + "1EyebrowS_S";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 100f;
                angryDEF = 100f;
                displayName = "test eyebrow angry";
                imageSrc = imgRoot + "1EyebrowA_N";
                highLightedImage = imgRoot + "1EyebrowA_H";
                selectedImage = imgRoot + "1EyebrowA_S";
                break;
        }
        
        skill = new DefenseSkill(attribute);
    }
}