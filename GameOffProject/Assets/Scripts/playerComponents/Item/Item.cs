using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected float happyATK;
    protected float happyDEF;
    protected float sadATK;
    protected float sadDEF;
    protected float angryATK;
    protected float angryDEF;
    protected Skill skill;
    protected string displayName;
    protected string imageSrc;

    public float getHappyATK() {
        return happyATK;
    }
    public float getHappyDEF() {
        return happyDEF;
    }
    public float getSadATK() {
        return sadATK;
    }
    public float getSadDEF() {
        return sadDEF;
    }
    public float getAngryATK() {
        return angryATK;
    }
    public float getAngryDEF() {
        return angryDEF;
    }
    public Skill getSkill() {
        return skill;
    }

    public string getDisplayName() {
        return displayName;
    }
    public string getImageSrc() {
        return imageSrc;
    }
}
