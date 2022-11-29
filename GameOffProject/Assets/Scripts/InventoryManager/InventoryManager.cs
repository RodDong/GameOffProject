using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Cecil;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject Buttons;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject Description;
    [SerializeField] private GameObject Stats;
    [SerializeField] private GameObject Inventory;
    [SerializeField] private Button eyeButton;
    [SerializeField] private Button eyeBrowButton;
    [SerializeField] private Button mouthButton;
    [SerializeField] private GameObject eyeBrowMask;
    [SerializeField] private GameObject mouthMask;
    [SerializeField] private GameObject eyeMask;
    private GameObject player;
    private PlayerStatus playerStatus;

    const float posX = -372.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
        UpdateEquippedItems();
        eyeBrowButton.onClick.AddListener(delegate {HighlightItemIcon(eyeBrowButton, playerStatus.getEquippedEyeBrow());});
        eyeButton.onClick.AddListener(delegate {HighlightItemIcon(eyeButton, playerStatus.getEquippedEyes());});
        mouthButton.onClick.AddListener(delegate {HighlightItemIcon(mouthButton, playerStatus.getEquippedMouth());});
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
            UpdateEyebrowDisplay();
            HighlightItemIcon(eyeBrowButton, player.GetComponent<PlayerStatus>().getEquippedEyeBrow());
            UpdateStatus();
        }
    }

    public void UpdateStatus()
    {
        string happyAttack = "HappyAttack: ";
        happyAttack += playerStatus.getATKbyAttribute(SkillAttribute.HAPPY) + "\n";

        string angryAttack = "AngryAttack: ";
        angryAttack += playerStatus.getATKbyAttribute(SkillAttribute.ANGRY) + "\n";

        string sadAttack = "SadAttack: ";
        sadAttack += playerStatus.getATKbyAttribute(SkillAttribute.SAD) + "\n";

        string happyDefense = "HappyDefense: ";
        happyDefense += playerStatus.getDEFbyAttribute(SkillAttribute.HAPPY)+ "\n";

        string angryDefense = "AngryDefense: ";
        angryDefense += playerStatus.getDEFbyAttribute(SkillAttribute.ANGRY) + "\n";

        string sadDefense = "SadDefense: ";
        sadDefense += playerStatus.getDEFbyAttribute(SkillAttribute.SAD) + "\n";

        string stats = happyAttack + angryAttack + sadAttack + happyDefense + angryDefense + sadDefense;

        Stats.GetComponentInChildren<TextMeshProUGUI>().text = stats;
    }

    public void UpdateEquippedItems()
    {
        Item equippedEyebrow = playerStatus.getEquippedEyeBrow();
        Item equippedEyes = playerStatus.getEquippedEyes();
        Item equippedMouth = playerStatus.getEquippedMouth();
        InitializeButtonSpriteState(eyeBrowButton, equippedEyebrow);
        InitializeButtonSpriteState(eyeButton, equippedEyes);
        InitializeButtonSpriteState(mouthButton, equippedMouth);
        eyeMask.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyes.getPlayerMaskImage());
        eyeBrowMask.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedEyebrow.getPlayerMaskImage());
        mouthMask.GetComponent<Image>().sprite = Resources.Load<Sprite>(equippedMouth.getPlayerMaskImage());
    }

    public void UpdateEyebrowDisplay()
    {
        float posY = 0.0f;
        List<EyeBrow> ownedEyeBrows = playerStatus.getOwnedEyeBrows();
        Item equippedEyes = playerStatus.getEquippedEyes();
        Item equippedMouth = playerStatus.getEquippedMouth();
        InitializeButtonSpriteState(eyeButton, equippedEyes);
        InitializeButtonSpriteState(mouthButton, equippedMouth);
        foreach (Transform child in Buttons.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ownedEyeBrows.Count; i++)
        {
            Button newButton = Button.Instantiate(buttonPrefab);
            Transform newButtonTransform = newButton.GetComponent<Transform>();
            newButtonTransform.SetParent(Buttons.transform);
            newButtonTransform.localPosition = new Vector3(posX, posY, 0.0f);
            newButtonTransform.localScale = new Vector3(1.0f, 1.0f, 0);
            EyeBrow curItem = ownedEyeBrows[i];

            newButton.onClick.AddListener(delegate { UpdateDescription(curItem); });
            newButton.onClick.AddListener(delegate { playerStatus.setEquippedEyeBrow((EyeBrow)curItem); });
            newButton.onClick.AddListener(delegate { UpdateEquippedItems();});
            newButton.onClick.AddListener(delegate { playerStatus.updateStatus(); });
            newButton.onClick.AddListener(delegate { UpdateStatus(); });
            newButton.onClick.AddListener(delegate { HighlightItemDetail(newButton, curItem);});
            newButton.onClick.AddListener(delegate { HighlightItemIcon(eyeBrowButton, curItem); });
            newButton.onClick.AddListener(delegate { InitializeButtonSpriteState(Buttons.GetComponentInChildren<Button>(), ownedEyeBrows[0]); });

            InitializeButtonSpriteState(newButton, curItem);
            if (curItem.Equals(playerStatus.getEquippedEyeBrow()))
            {
                HighlightItemDetail(newButton, curItem);
            }
            posY -= 130.0f;
        }
        UpdateDescription(ownedEyeBrows[0]);
    }

    public void UpdateEyesDisplay()
    {
        float posY = 0.0f;
        List<Eye> ownedEyes = playerStatus.getOwnedEyes();
        Item equippedEyebrow = playerStatus.getEquippedEyeBrow();
        Item equippedMouth = playerStatus.getEquippedMouth();
        InitializeButtonSpriteState(eyeBrowButton, equippedEyebrow);
        InitializeButtonSpriteState(mouthButton, equippedMouth);
        
        foreach (Transform child in Buttons.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < ownedEyes.Count; i++)
        {
            Button newButton = Button.Instantiate(buttonPrefab);
            Transform newButtonTransform = newButton.GetComponent<Transform>();
            newButtonTransform.SetParent(Buttons.transform);
            newButtonTransform.localPosition = new Vector3(posX, posY, 0.0f);
            newButtonTransform.localScale = new Vector3(1.0f, 1.0f, 0);
            Eye curItem = ownedEyes[i];

            newButton.onClick.AddListener(delegate { UpdateDescription(curItem); });
            newButton.onClick.AddListener(delegate { playerStatus.setEquippedEyes((Eye)curItem); });
            newButton.onClick.AddListener(delegate { UpdateEquippedItems(); });
            newButton.onClick.AddListener(delegate { playerStatus.updateStatus(); });
            newButton.onClick.AddListener(delegate { UpdateStatus(); });
            newButton.onClick.AddListener(delegate { HighlightItemDetail(newButton, curItem);});
            newButton.onClick.AddListener(delegate { HighlightItemIcon(eyeButton, curItem); });
            newButton.onClick.AddListener(delegate { InitializeButtonSpriteState(Buttons.GetComponentInChildren<Button>(), ownedEyes[0]); });
            
            InitializeButtonSpriteState(newButton, curItem);
            if (curItem.Equals(playerStatus.getEquippedEyes()))
            {
                HighlightItemDetail(newButton, curItem);
            }
            posY -= 130.0f;
        }
        UpdateDescription(ownedEyes[0]);
    }

    public void UpdateMouthDisplay()
    {
        float posY = 0.0f;
        List<Mouth> ownedMouth = playerStatus.getOwnedMouths();
        Item equippedEyebrow = playerStatus.getEquippedEyeBrow();
        Item equippedEyes = playerStatus.getEquippedEyes();
        InitializeButtonSpriteState(eyeBrowButton, equippedEyebrow);
        InitializeButtonSpriteState(eyeButton, equippedEyes);
        foreach (Transform child in Buttons.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ownedMouth.Count; i++)
        {
            Button newButton = Button.Instantiate(buttonPrefab);
            Transform newButtonTransform = newButton.transform;
            newButtonTransform.SetParent(Buttons.transform);
            newButtonTransform.localPosition = new Vector3(posX, posY, 0.0f);
            newButtonTransform.localScale = new Vector3(1.0f, 1.0f, 0);
            Mouth curItem = ownedMouth[i];

            newButton.onClick.AddListener(delegate { UpdateDescription(curItem); });
            newButton.onClick.AddListener(delegate { playerStatus.setEquippedMouth((Mouth)curItem); });
            newButton.onClick.AddListener(delegate { UpdateEquippedItems(); });
            newButton.onClick.AddListener(delegate { playerStatus.updateStatus(); });
            newButton.onClick.AddListener(delegate { UpdateStatus(); });
            newButton.onClick.AddListener(delegate { HighlightItemDetail(newButton, curItem);});
            newButton.onClick.AddListener(delegate { HighlightItemIcon(mouthButton, curItem); });
            newButton.onClick.AddListener(delegate { InitializeButtonSpriteState(Buttons.GetComponentInChildren<Button>(), ownedMouth[0]); });
            InitializeButtonSpriteState(newButton, curItem);
            if (curItem.Equals(playerStatus.getEquippedMouth()))
            {
                HighlightItemDetail(newButton, curItem);
            }
            posY -= 130.0f;
        }
        UpdateDescription(ownedMouth[0]);
    }

    public void UpdateDescription(Item item)
    {
        Description.GetComponent<TextMeshProUGUI>().text = item.getDisplayName() + "\n" + item.getDescription();
    }

    private void InitializeButtonSpriteState(Button newButton, Item curItem)
    {
        newButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(curItem.getImageSrc());
        SpriteState sState = new SpriteState();
        sState.selectedSprite = Resources.Load<Sprite>(curItem.getImageSrc());
        sState.highlightedSprite = Resources.Load<Sprite>(curItem.getHighLightedImage());
        sState.pressedSprite = Resources.Load<Sprite>(curItem.getImageSrc());
        sState.disabledSprite = Resources.Load<Sprite>(curItem.getSelectedImage());
        newButton.spriteState = sState;
    }

    private void HighlightItemIcon(Button newButton, Item curItem)
    {
        UpdateDescription(curItem);
        eyeBrowButton.interactable = true;
        eyeButton.interactable = true;
        mouthButton.interactable = true;
        newButton.interactable = false;
    }

    private void HighlightItemDetail(Button newButton, Item curItem)
    {
        UpdateDescription(curItem);
        foreach (Button button in Buttons.GetComponentsInChildren<Button>()) {
            button.interactable = true;
        }
        newButton.interactable = false;
    }
}
