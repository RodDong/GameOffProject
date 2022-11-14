using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff
{
    public enum DebuffId {
        // miss rate
        TEST_DEBUFF_1,
    }

    private DebuffId id;
    private int duration;

    public Debuff(DebuffId id) {
        switch(id) {
            case DebuffId.TEST_DEBUFF_1:
                duration = 3;
                break;
            default:
                break;
        }
    }
    
    public bool decreaseCounter() {
        duration -= 1;
        return duration <= 0;
    }
}
