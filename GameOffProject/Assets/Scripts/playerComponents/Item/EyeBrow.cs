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
                displayName = "The Sweating brows";
                itemDescription = "happyATK = 0\r\nhappyDEF = 0\r\nsadATK = 0\r\nsadDEF = 0\r\nangryATK = 0\r\nangryDEF = 0";
                imageSrc = imgRoot + "1EyebrowDefault_N";
                highLightedImage = imgRoot + "1EyebrowDefault_H";
                selectedImage = imgRoot + "1EyebrowDefault_S";
                playerMaskImageSrc = playerMaskRoot + "1EyebrowDefault_N";
                break;
            case SkillAttribute.HAPPY:
                happyATK = 0f;
                happyDEF = 50f;
                sadATK = 0f;
                sadDEF = 30f;
                angryATK = 0f;
                angryDEF = 10f;
                displayName = "Roof high anxiousness";
                itemDescription = "happyATK = 0\r\nhappyDEF = 50\r\nsadATK = 0\r\nsadDEF = 30\r\nangryATK = 0\r\nangryDEF = 10";
                imageSrc = imgRoot + "1EyebrowH_N";
                highLightedImage = imgRoot + "1EyebrowH_H";
                selectedImage = imgRoot + "1EyebrowH_S";
                playerMaskImageSrc = playerMaskRoot + "1EyebrowH_N";
                break;
            case SkillAttribute.SAD:
                happyATK = 0f;
                happyDEF = 0f;
                sadATK = 20f;
                sadDEF = 50f;
                angryATK = 0f;
                angryDEF = 10f;
                displayName = "Knitting in Frown";
                itemDescription = "happyATK = 0\r\nhappyDEF = 0\r\nsadATK = 20\r\nsadDEF = 50\r\nangryATK = 0\r\nangryDEF = 10";
                imageSrc = imgRoot + "1EyebrowS_N";
                highLightedImage = imgRoot + "1EyebrowS_H";
                selectedImage = imgRoot + "1EyebrowS_S";
                playerMaskImageSrc = playerMaskRoot + "1EyebrowS_N";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 30f;
                happyDEF = 0f;
                sadATK = 30f;
                sadDEF = 0f;
                angryATK = 0f;
                angryDEF = 50f;
                displayName = "Sopracci-rossa";
                imageSrc = imgRoot + "1EyebrowA_N";
                highLightedImage = imgRoot + "1EyebrowA_H";
                selectedImage = imgRoot + "1EyebrowA_S";
                playerMaskImageSrc = playerMaskRoot + "1EyebrowA_N";
                itemDescription = "happyATK = 30\r\nhappyDEF = 0\r\nsadATK = 30\r\nsadDEF = 0\r\nangryATK = 0\r\nangryDEF = 50";
                break;
        }
        
        skill = new DefenseSkill(attribute);
    }
}