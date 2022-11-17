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
                break;
            case SkillAttribute.HAPPY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test mouth happy";
                break;
            case SkillAttribute.SAD:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test mouth sad";
                break;
            case SkillAttribute.ANGRY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                displayName = "test mouth angry";
                break;
        }
        skill = new BuffSkill(attribute);
    }
}
