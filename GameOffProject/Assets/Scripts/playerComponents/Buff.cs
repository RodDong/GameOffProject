using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType {
        // ???
    }

    public BuffType type;
    private int duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool DecreaseDuration() {
        duration--;
        return duration == 0;
    }
}
