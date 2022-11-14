using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public enum BuffId {
        // attack up
        TEST_BUFF_1,
        IMMUNE,
        REFLECT,
        BOUNS_DAMAGE
    }

    private BuffId id;
    public BuffId GetBuffId() {
        return id;
    }

    private int duration;
    public void resetDuration() {
        switch(id) {
            case BuffId.TEST_BUFF_1:
                duration = 3;
                break;
            case BuffId.IMMUNE:
                duration = 1;
                break;
            case BuffId.REFLECT:
                duration = 1;
                break;
            case BuffId.BOUNS_DAMAGE:
                duration = 3;
                break;
            default:
                break;
        }
    }

    public Buff(BuffId id) {
        this.id = id;
        switch(id) {
            case BuffId.TEST_BUFF_1:
                duration = 3;
                break;
            case BuffId.IMMUNE:
                duration = 1;
                break;
            case BuffId.REFLECT:
                duration = 1;
                break;
            case BuffId.BOUNS_DAMAGE:
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
