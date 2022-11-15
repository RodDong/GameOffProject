using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffSkill;
using static DebuffSkill;

public class Mouth : Item
{
    public enum MouthId {
        TEST_MOUTH_1,
        TEST_MOUTH_HAPPY,
        TEST_MOUTH_SAD,
        TEST_MOUTH_ANGRY
    }
    private MouthId id;
    public MouthId getID() {
        return id;
    }
    public Mouth(MouthId id) {
        this.id = id;

        switch(id) {
            case MouthId.TEST_MOUTH_1:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new BuffSkill(BuffSkillId.TEST_BUFF_SKILL_1);
                displayName = "test mouth";
                itemDescription = "test description";
                imageSrc = imgRoot + "3MouthA_N";
                highLightedImage = imgRoot + "3MouthA_H";
                selectedImage = imgRoot + "3MouthA_S";
                break;
            case MouthId.TEST_MOUTH_HAPPY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new BuffSkill(BuffSkillId.TEST_BUFF_SKILL_1);
                displayName = "test mouth happy";
                break;
            case MouthId.TEST_MOUTH_SAD:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new BuffSkill(BuffSkillId.TEST_BUFF_SKILL_1);
                displayName = "test mouth sad";
                break;
            case MouthId.TEST_MOUTH_ANGRY:
                happyATK = 50f;
                happyDEF = 50f;
                sadATK = 50f;
                sadDEF = 50f;
                angryATK = 50f;
                angryDEF = 50f;
                skill = new BuffSkill(BuffSkillId.TEST_BUFF_SKILL_1);
                displayName = "test mouth angry";
                break;
        }
    }
}
