using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffSkill;
using static DebuffSkill;

public class Mouth : Item
{
    public enum MouthId {
        TEST_MOUTH_1,
    }
    private MouthId id;

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
                break;
        }
    }
}
