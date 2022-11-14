using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] GameObject Buttons;
    [SerializeField] Button buttonPrefab;
    [SerializeField] GameObject Description;
    [SerializeField] GameObject Stats;
    [SerializeField] GameObject Inventory;
    GameObject mPlayer;
    PlayerStatus mPlayerStatus;

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mPlayerStatus = mPlayer.GetComponent<PlayerStatus>();
    }
    public void UISwitch()
    {
        if(Inventory.active)
        {
            Inventory.SetActive(false);
        }
        else
        {
            Inventory.SetActive(true);
        }
    }

    public void DisplayStats()
    {
        string happyAttack = "HappyAttack: ";
        happyAttack += mPlayerStatus.getHappyATK() + "\n";

        string angryAttack = "AngryAttack: ";
        angryAttack += mPlayerStatus.getAngryATK() + "\n";

        string sadAttack = "SadAttack: ";
        sadAttack += mPlayerStatus.getSadATK() + "\n";

        string happyDefense = "HappyDefense: ";
        happyDefense += mPlayerStatus.getHappyDEF() + "\n";

        string angryDefense = "AngryDefense: ";
        angryDefense += mPlayerStatus.getAngryDEF() + "\n";

        string sadDefense = "SadDefense: ";
        sadDefense += mPlayerStatus.getSadDEF() + "\n";

        string stats = happyAttack + angryAttack + sadAttack + happyDefense + angryDefense + sadDefense;

        Stats.GetComponentInChildren<TextMeshProUGUI>().text = stats;
    }

    public void DisplayEyeBrowInv()
    {
        float posX = -372.0f;
        float posY = -10.0f;
        List<EyeBrow> ownedEyeBrows = mPlayerStatus.getOwnedEyeBrows();
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
            Item curItem = ownedEyeBrows[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem); });
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedEyeBrows[i].getDisplayName();
            posY -= 130.0f;
        }
    }

    public void DisplayEyesInv()
    {
        float posX = -372.0f;
        float posY = -10.0f;
        List<Eye> ownedEyes = mPlayerStatus.getOwnedEyes();
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
            Item curItem = ownedEyes[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem); });
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedEyes[i].getDisplayName();
            posY -= 130.0f;
        }
    }

    public void DisplayMouthInv()
    {
        float posX = -372.0f;
        float posY = -10.0f;
        List<Mouth> ownedMouth = mPlayerStatus.getOwnedMouths();
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
            Item curItem = ownedMouth[i];
            tempButton.onClick.AddListener(delegate { DisplayDescription(curItem); });
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = ownedMouth[i].getDisplayName();
            posY -= 130.0f;
        }
    }

    public void DisplayDescription(Item item)
    {
        Description.GetComponent<TextMeshProUGUI>().text = item.getDescription();
    }
}
