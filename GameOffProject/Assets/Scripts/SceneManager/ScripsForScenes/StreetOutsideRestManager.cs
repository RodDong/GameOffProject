using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StreetOutsideRestManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress2;
    [SerializeField] TextAsset progress36, progress60;
    [SerializeField] GameObject doorToBar;
    [SerializeField] GameObject doorToRestaurant, subway;
    [SerializeField] GameObject backGroundLight, characterLight;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 2) {
            ProcessProgress_2();
        }
        if (progressManager.currentProgress == 36) {
            ProcessProgress_36();
        }
        if(progressManager.currentProgress == 60)
        {
            subway.SetActive(false);
            backGroundLight.GetComponent<Light>().color = new Color(48, 58, 84);
            characterLight.GetComponent<Light>().color = new Color(48, 58, 84);
        }
        else
        {
            backGroundLight.GetComponent<Light>().color = new Color(255, 255, 255);
            characterLight.GetComponent<Light>().color = new Color(255, 255, 255);
            ProcessProgress_60();
        }
    }

    private async void ProcessProgress_2() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2);
        doorToBar.SetActive(false);
        doorToRestaurant.SetActive(false);
    }

    private async void ProcessProgress_36() {
        await Task.Delay(200);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36);
        doorToRestaurant.SetActive(false);
    }

    private async void ProcessProgress_60()
    {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress60);
    }
}
