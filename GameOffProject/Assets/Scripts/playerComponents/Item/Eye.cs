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
                happyATK = 70f;
                happyDEF = 100f;
                sadATK = 70f;
                sadDEF = 100f;
                angryATK = 70f;
                angryDEF = 100f;
                skill = new AttackSkill(SkillAttribute.NONE);
                displayName = "The Foggy Notion";
                itemDescription = "happyATK = 70\r\nhappyDEF = 100\r\nsadATK = 70\r\nsadDEF = 100\r\nangryATK = 70\r\nangryDEF = 100";
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
                itemDescription = "happyATK = 50\r\nhappyDEF = 50\r\nsadATK = 20\r\nsadDEF = 10\r\nangryATK = 0\r\nangryDEF = 20";
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
                itemDescription = "happyATK = 10\r\nhappyDEF = 20\r\nsadATK = 50\r\nsadDEF = 10\r\nangryATK = 0\r\nangryDEF = 0";
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
                itemDescription = "happyATK = 30\r\nhappyDEF = 0\r\nsadATK = 30\r\nsadDEF = 0\r\nangryATK = 50\r\nangryDEF = 0";
                imageSrc = imgRoot + "2EyeA_N";
                highLightedImage = imgRoot + "2EyeA_H";
                selectedImage = imgRoot + "2EyeA_S";
                playerMaskImageSrc = playerMaskRoot + "2EyeA_N";
                break;
        }
    }
}
