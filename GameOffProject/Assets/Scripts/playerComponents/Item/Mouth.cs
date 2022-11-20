using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffSkill;

public class Mouth : Item
{
    public Mouth(SkillAttribute attribute) {
        this.attribute = attribute;
        itemType = "Mouth";

        switch(attribute) {
            case SkillAttribute.NONE:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test mouth";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthA_N";
                highLightedImage = imgRoot + "3MouthA_H";
                selectedImage = imgRoot + "3MouthA_S";
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                break;
            case SkillAttribute.HAPPY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test mouth happy";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthH_N";
                highLightedImage = imgRoot + "3MouthH_H";
                selectedImage = imgRoot + "3MouthH_S";
                skillImage = imgRoot + "EmptyButton/" + "buff_happy";
                break;
            case SkillAttribute.SAD:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test mouth sad";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthS_N";
                highLightedImage = imgRoot + "3MouthS_H";
                selectedImage = imgRoot + "3MouthS_S";
                skillImage = imgRoot + "EmptyButton/" + "debuff_happy";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test mouth angry";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthA_N";
                highLightedImage = imgRoot + "3MouthA_H";
                selectedImage = imgRoot + "3MouthA_S";
                skillImage = imgRoot + "EmptyButton/" + "bubuff_angry";
                break;
        }
        skill = new BuffSkill(attribute);
    }
}
