using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
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
    protected string highLightedImage;
    protected string skillImage;
    protected string selectedImage;
    protected string itemDescription;
    protected string imgRoot = "Art/UI/Buttons/";
    protected string itemType;
    protected SkillAttribute attribute;

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

    public string getHighLightedImage()
    {
        return highLightedImage;
    }

    public string getSelectedImage()
    {
        return selectedImage;
    }
    public string getDescription()
    {
        return itemDescription;
    }

    public string getSkillImage()
    {
        return skillImage;
    }

    // override object.Equals
    public override bool Equals(object obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //
        
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        return this.attribute == ((Item)obj).attribute;
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
         return base.GetHashCode();
    }
}
