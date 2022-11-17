using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class ClueManager : MonoBehaviour
{

    [SerializeField] GameObject clueButtons;
    [SerializeField] Button buttonPrefab;
    [SerializeField] GameObject Description;
    [SerializeField] GameObject ClueImg;
    [SerializeField] GameObject CluesUI;
    public GameObject mPlayer;
    private List<Clue> allClues;

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        allClues = mPlayer.GetComponent<PlayerStatus>().playerClues;
        DisplayClues();
    }

    [System.Obsolete]
    public void ClueUISwitch()
    {
            CluesUI.SetActive(!CluesUI.active);
    }

    private void DisplayClues()
    {
        float posY = 0f;
        foreach (Clue c in allClues)
        {
            Button tempButton = Button.Instantiate(buttonPrefab);
            Transform tempButtonTrans = tempButton.GetComponent<Transform>();
            tempButtonTrans.SetParent(clueButtons.transform);
            tempButtonTrans.localPosition = new Vector3(0f, posY, 0f);
            tempButtonTrans.localScale = new Vector3(0.6f, 0.6f, 0);
            tempButton.onClick.AddListener(delegate { DisplayClue(c); });
            SetClueButtonSprite(tempButton, c);
            posY -= 67.0f;
        }
    }

    private void SetClueButtonSprite(Button tempButton, Clue clue)
    {
        ((Image)tempButton.targetGraphic).sprite = Resources.Load<Sprite>(clue.clueBtnSrc);
    }

    private void DisplayClue(Clue c)
    {
        Description.GetComponent<TextMeshProUGUI>().text = c.Name + "\n" + c.Content;
        ClueImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(c.clueImgSrc);
    }
}
