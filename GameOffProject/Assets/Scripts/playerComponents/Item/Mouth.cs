using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : Item
{
    public Mouth(SkillAttribute attribute) {
        this.attribute = attribute;
        itemType = "Mouth";

        switch(attribute) {
            case SkillAttribute.NONE:
                happyATK = 0f;
                happyDEF = 0f;
                sadATK = 0f;
                sadDEF = 0f;
                angryATK = 0f;
                angryDEF = 0f;
                displayName = "test mouth";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthDefault_N";
                highLightedImage = imgRoot + "3MouthDefault_H";
                selectedImage = imgRoot + "3MouthDefault_S";
                playerMaskImageSrc = playerMaskRoot + "3MouthDefault_N";
                break;
            case SkillAttribute.HAPPY:
                happyATK = 30f;
                happyDEF = 30f;
                sadATK = 20f;
                sadDEF = 20f;
                angryATK = 20f;
                angryDEF = 20f;
                displayName = "test mouth happy";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthH_N";
                highLightedImage = imgRoot + "3MouthH_H";
                selectedImage = imgRoot + "3MouthH_S";
                playerMaskImageSrc = playerMaskRoot + "3MouthH_N";
                break;
            case SkillAttribute.SAD:
                happyATK = 10f;
                happyDEF = 10f;
                sadATK = 10f;
                sadDEF = 30f;
                angryATK = 20f;
                angryDEF = 10f;
                displayName = "test mouth sad";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthS_N";
                highLightedImage = imgRoot + "3MouthS_H";
                selectedImage = imgRoot + "3MouthS_S";
                playerMaskImageSrc = playerMaskRoot + "3MouthS_N";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 30f;
                happyDEF = 0f;
                sadATK = 30f;
                sadDEF = 0f;
                angryATK = 30f;
                angryDEF = 0f;
                displayName = "test mouth angry";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthA_N";
                highLightedImage = imgRoot + "3MouthA_H";
                selectedImage = imgRoot + "3MouthA_S";
                playerMaskImageSrc = playerMaskRoot + "3MouthA_N";

                break;
        }
        skill = new EffectSkill(attribute);
    }
}
