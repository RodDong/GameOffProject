using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] GameObject Buttons;
    [SerializeField] Button buttonPrefab;
    [SerializeField] GameObject Description;
    [SerializeField] GameObject Stats;
    [SerializeField] GameObject Inventory;
    [SerializeField] Button eyeButton;
    [SerializeField] Button eyeBrowButton;
    [SerializeField] Button mouthButton;
    GameObject mPlayer;
    PlayerStatus mPlayerStatus;

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mPlayerStatus = mPlayer.GetComponent<PlayerStatus>();
        UpdateEquippedItems();
    }
    public void UISwitch()
    {
        if(Inventory.activeSelf)
        {
            Inventory.SetActive(false);
        }
        else
        {
            Inventory.SetActive(true);
            DisplayEyeBrowInv();
            SetButtonHighLightedSprite(eyeBrowButton, mPlayer.GetComponent<PlayerStatus>().getEquippedEyeBrow());
            DisplayStats();
        }
    }

    public void DisplayStats()
    {
        string happyAttack = "HappyAttack: ";
        happyAttack += mPlayerStatus.getATKbyAttribute(SkillAttribute.HAPPY) + "\n";

        string angryAttack = "AngryAttack: ";
        angryAttack += mPlayerStatus.getATKbyAttribute(SkillAttribute.ANGRY) + "\n";

        string sadAttack = "SadAttack: ";
        sadAttack += mPlayerStatus.getATKbyAttribute(SkillAttribute.SAD) + "\n";

        string happyDefense = "HappyDefense: ";
        happyDefense += mPlayerStatus.getDEFbyAttribute(SkillAttribute.HAPPY)+ "\n";

        string angryDefense = "AngryDefense: ";
        angryDefense += mPlayerStatus.getDEFbyAttribute(SkillAttribute.ANGRY) + "\n";

        string sadDefense = "SadDefense: ";
        sadDefense += mPlayerStatus.getDEFbyAttribute(SkillAttribute.SAD) + "\n";

        string stats = happyAttack + angryAttack + sadAttack + happyDefense + angryDefense + sadDefense;

        Stats.GetComponentInChildren<TextMeshProUGUI>().text = stats;
    }

    public void UpdateEquippedItems()
    {
        Item equipedEyeBrow = mPlayerStatus.getEquippedEyeBrow();
        Item equipedEye = mPlayerStatus.getEquippedEyes();
        Item equipedMouth = mPlayerStatus.getEquippedMouth();
        SetButtonSprite(eyeBrowButton, equipedEyeBrow);
        SetButtonSprite(eyeButton, equipedEye);
        SetButtonSprite(mouthButton, equipedMouth);
    }

    public void DisplayEyeBrowInv()
    {
        float posX = -372.0f;
        float posY = -30.0f;
        List<EyeBrow> ownedEyeBrows = mPlayerStatus.getOwnedEyeBrows();
        Item equipedEye = mPlayerStatus.getEquippedEyes();
        Item equipedMouth = mPlayerStatus.getEquippedMouth();
        SetButtonSprite(eyeButton, equipedEye);
        SetButtonSprite(mouthButton, equipedMouth);
        foreach (Transform child in Buttons.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ownedEyeBrows.Count; i++)
        {
            Button tempButton = Button.Instantiate(buttonPrefab);
            Transform tempButtonTrans = tempButton.GetComponent<Transform>();
            tempButtonTrans.SetParent(Buttons.transform);
            tempButtonTrans.localPosition = new Vector3(posX, posY, 0.0f);
            tempButtonTrans.localScale = new Vector3(1.25f, 1.25f, 0);
            EyeBrow curItem = ownedEyeBrows[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem); });
            tempButton.onClick.AddListener(delegate { mPlayerStatus.setEquippedEyeBrow((EyeBrow)curItem); });
            tempButton.onClick.AddListener(delegate{ UpdateEquippedItems();});
            tempButton.onClick.AddListener(delegate { mPlayerStatus.updateStatus(); });
            tempButton.onClick.AddListener(delegate { DisplayStats(); });
            tempButton.onClick.AddListener(delegate { SetButtonHighLightedSprite(eyeBrowButton, curItem); });
            tempButton.onClick.AddListener(delegate { SetButtonSprite(Buttons.GetComponentInChildren<Button>(), (Item)ownedEyeBrows[0]); });
            //tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedEyeBrows[i].getDisplayName();

            SetButtonSprite(tempButton, curItem);
            if (curItem.Equals(mPlayerStatus.getEquippedEyeBrow()))
            {
                Debug.Log("1");
                SetButtonHighLightedSprite(tempButton, curItem);
            }
            posY -= 160.0f;
        }
        SetButtonHighLightedSprite(Buttons.GetComponentInChildren<Button>(), (Item)ownedEyeBrows[0]);
        DisplayDescription((Item)ownedEyeBrows[0]);
    }

    public void DisplayEyesInv()
    {
        float posX = -372.0f;
        float posY = -30.0f;
        List<Eye> ownedEyes = mPlayerStatus.getOwnedEyes();
        Item equipedEyeBrow = mPlayerStatus.getEquippedEyeBrow();
        Item equipedMouth = mPlayerStatus.getEquippedMouth();
        SetButtonSprite(eyeBrowButton, equipedEyeBrow);
        SetButtonSprite(mouthButton, equipedMouth);
        
        foreach (Transform child in Buttons.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ownedEyes.Count; i++)
        {
            Button tempButton = Button.Instantiate(buttonPrefab);
            Transform tempButtonTrans = tempButton.GetComponent<Transform>();
            tempButtonTrans.SetParent(Buttons.transform);
            tempButtonTrans.localPosition = new Vector3(posX, posY, 0.0f);
            tempButtonTrans.localScale = new Vector3(1.25f, 1.25f, 0);
            Eye curItem = ownedEyes[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem); });
            tempButton.onClick.AddListener(delegate { mPlayerStatus.setEquippedEyes((Eye)curItem); });
            tempButton.onClick.AddListener(delegate { UpdateEquippedItems(); });
            tempButton.onClick.AddListener(delegate { mPlayerStatus.updateStatus(); });
            tempButton.onClick.AddListener(delegate { DisplayStats(); });
            tempButton.onClick.AddListener(delegate { SetButtonHighLightedSprite(eyeButton, curItem); });
            tempButton.onClick.AddListener(delegate { SetButtonSprite(Buttons.GetComponentInChildren<Button>(), (Item)ownedEyes[0]); });
            //tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedEyes[i].getDisplayName();
            
            SetButtonSprite(tempButton, curItem);
            if (curItem.Equals(mPlayerStatus.getEquippedEyes()))
            {
                SetButtonHighLightedSprite(tempButton, curItem);
            }
            posY -= 160.0f;
        }
        
        DisplayDescription((Item)ownedEyes[0]);
    }

    public void DisplayMouthInv()
    {
        float posX = -372.0f;
        float posY = -30.0f;
        List<Mouth> ownedMouth = mPlayerStatus.getOwnedMouths();
        Item equipedEyeBrow = mPlayerStatus.getEquippedEyeBrow();
        Item equipedEye = mPlayerStatus.getEquippedEyes();
        SetButtonSprite(eyeBrowButton, equipedEyeBrow);
        SetButtonSprite(eyeButton, equipedEye);
        foreach (Transform child in Buttons.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ownedMouth.Count; i++)
        {
            Button tempButton = Button.Instantiate(buttonPrefab);
            Transform tempButtonTrans = tempButton.transform;
            tempButtonTrans.SetParent(Buttons.transform);
            tempButtonTrans.localPosition = new Vector3(posX, posY, 0.0f);
            tempButtonTrans.localScale = new Vector3(1.25f, 1.25f, 0);
            Mouth curItem = ownedMouth[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem); });
            tempButton.onClick.AddListener(delegate { mPlayerStatus.setEquippedMouth((Mouth)curItem); });
            tempButton.onClick.AddListener(delegate { UpdateEquippedItems(); });
            tempButton.onClick.AddListener(delegate { mPlayerStatus.updateStatus(); });
            tempButton.onClick.AddListener(delegate { DisplayStats(); });
            tempButton.onClick.AddListener(delegate { SetButtonHighLightedSprite(mouthButton, curItem); });
            tempButton.onClick.AddListener(delegate { SetButtonSprite(Buttons.GetComponentInChildren<Button>(), (Item)ownedMouth[0]); });
            //tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedMouth[i].getDisplayName();
            
            SetButtonSprite(tempButton, curItem);
            if (curItem.Equals(mPlayerStatus.getEquippedMouth()))
            {
                SetButtonHighLightedSprite(tempButton, curItem);
            }
            posY -= 160.0f;
        }
        SetButtonHighLightedSprite(Buttons.GetComponentInChildren<Button>(), (Item)ownedMouth[0]);
        DisplayDescription((Item)ownedMouth[0]);
    }

    public void DisplayDescription(Item item)
    {
        Description.GetComponent<TextMeshProUGUI>().text = item.getDisplayName() + "\n" + item.getDescription();
    }

    private void SetButtonSprite(Button tempButton, Item curItem)
    {
        SpriteState sState = new SpriteState();
        sState.selectedSprite = Resources.Load<Sprite>(curItem.getSelectedImage());
        sState.highlightedSprite = Resources.Load<Sprite>(curItem.getHighLightedImage());
        tempButton.spriteState = sState;
        ((Image)tempButton.targetGraphic).sprite = Resources.Load<Sprite>(curItem.getImageSrc());
    }

    private void SetButtonHighLightedSprite(Button tempButton, Item curItem)
    {
/*        SpriteState sState = new SpriteState();
        sState.selectedSprite = Resources.Load<Sprite>(curItem.getSelectedImage());
        sState.highlightedSprite = Resources.Load<Sprite>(curItem.getHighLightedImage());
        tempButton.spriteState = sState;*/
        ((Image)tempButton.targetGraphic).sprite = Resources.Load<Sprite>(curItem.getSelectedImage());
    }
}
