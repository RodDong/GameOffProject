using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public enum BuffId {
        // attack up
        TEST_BUFF_1,
    }

    private BuffId id;
    private int duration;

    public Buff(BuffId id) {
        switch(id) {
            case BuffId.TEST_BUFF_1:
                duration = 3;
                break;
            default:
                break;
        }
    }

    public bool processBuff() {
        if (duration > 0) {
            duration -= 1;
            return true;
        } else {
            return false;
        }
    }
}
