using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class ClueManager : MonoBehaviour
{
    [SerializeField] private GameObject clueMenu;
    [SerializeField] private GameObject clues;
    [SerializeField] private GameObject clueDescription;
    [SerializeField] private GameObject clueImage;
    private PlayerStatus playerStatus;
    private int selectedClueNum;

    private void Start() {
        playerStatus = GameObject.FindObjectOfType<PlayerStatus>();
        selectedClueNum = 0;
        clues.GetComponentsInChildren<Button>()[selectedClueNum].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
        Clue selectedClue = playerStatus.getClue(selectedClueNum);
        clueDescription.GetComponent<TextMeshProUGUI>().text = selectedClue.getClueDescription();
        clueImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(selectedClue.getClueImageSrc());
    }

    public void UISwitch() {
        if (clueMenu.activeSelf) {
            clueMenu.SetActive(false);
        } else {
            Button[] clueButtons = clues.GetComponentsInChildren<Button>();
            for (int i = 0; i < clueButtons.Length; i++) {
                clueButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStatus.getClue(i).getClueName();
            }
            clueMenu.SetActive(true);
        }
    }

    public void updateClue(int clueNum) {
        selectedClueNum = clueNum;
        Clue selectedClue = playerStatus.getClue(selectedClueNum);
        Button[] clueButtons = clues.GetComponentsInChildren<Button>();
        for (int i = 0; i < clueButtons.Length; i++) {
            if (i == selectedClueNum) {
                clueButtons[i].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
            } else {
                clueButtons[i].GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            }
        }
        clueDescription.GetComponent<TextMeshProUGUI>().text = selectedClue.getClueDescription();
        clueImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(selectedClue.getClueImageSrc());
    }
}
