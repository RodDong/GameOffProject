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
                happyATK = 10f;
                happyDEF = 10f;
                sadATK = 10f;
                sadDEF = 10f;
                angryATK = 10f;
                angryDEF = 10f;
                skill = new AttackSkill(SkillAttribute.NONE);
                displayName = "The Foggy Notion";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeDefault_N";
                highLightedImage = imgRoot + "2EyeDefault_H";
                selectedImage = imgRoot + "2EyeDefault_S";
                playerMaskImageSrc = playerMaskRoot + "2EyeDefault_N";
                break;
            case SkillAttribute.HAPPY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 20f;
                sadDEF = 10f;
                angryATK = 0f;
                angryDEF = 20f;
                skill = new AttackSkill(SkillAttribute.HAPPY);
                displayName = "Blinded by the Light";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeH_N";
                highLightedImage = imgRoot + "2EyeH_H";
                selectedImage = imgRoot + "2EyeH_S";
                playerMaskImageSrc = playerMaskRoot + "2EyeH_N";
                break;
            case SkillAttribute.SAD:
                happyATK = 10f;
                happyDEF = 20f;
                sadATK = 50f;
                sadDEF = 10f;
                angryATK = 0f;
                angryDEF = 0f;
                skill = new AttackSkill(SkillAttribute.SAD);
                displayName = "Pale Blue Eyes";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeS_N";
                highLightedImage = imgRoot + "2EyeS_H";
                selectedImage = imgRoot + "2EyeS_S";
                playerMaskImageSrc = playerMaskRoot + "2EyeS_N";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 30f;
                happyDEF = 0f;
                sadATK = 30f;
                sadDEF = 0f;
                angryATK = 50f;
                angryDEF = 0f;
                skill = new AttackSkill(SkillAttribute.ANGRY);
                displayName = "Shiny Crazy Diamonds";
                itemDescription = "test description";
                imageSrc = imgRoot + "2EyeA_N";
                highLightedImage = imgRoot + "2EyeA_H";
                selectedImage = imgRoot + "2EyeA_S";
                playerMaskImageSrc = playerMaskRoot + "2EyeA_N";
                break;
        }
    }
}
