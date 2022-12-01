using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    bool startDialogue = false;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 2) {
            ProcessProgress_2();
        }
        List<int> progressInvestigateSim = new List<int>(){36, 6, 10, 12, 13, 16, 25, 27, 28, 58, 63, 64, 66};
        if (progressInvestigateSim.Contains(progressManager.currentProgress)) {
            ProcessProgress_36();
        }
        if (!progressInvestigateSim.Contains(progressManager.currentProgress)) {
            doorToBar.SetActive(false);
        }
        //List<int> progressStatesWithoutChef = new List<int>(){5, 9, 11, 12, 13, 14, 15, 16, 17, 18, 20, 23, 26, 27, 32, 34, 35, 38, 39, 40, 44, 45, 47, 48, 49, 50, 52, 53, 54, 55, 56, 57, 58, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73};
        //if (progressStatesWithoutChef.Contains(progressManager.currentProgress)) {
        doorToRestaurant.SetActive(false);
        // }
        if(progressManager.currentProgress == 60)
        {
            doorToBar.SetActive(false);
            subway.SetActive(false);
            ProcessProgress_60();
        }
    }

    private void Update()
    {
        if(!dialogueManager.dialogueIsPlaying && startDialogue)
        {
            Teleport();
        }
    }

    private async void Teleport()
    {
        startDialogue = false;
        SceneManager.LoadScene("6kitchen");
        await Task.Delay(300);
        player.transform.position = new Vector3(-11.04f, -0.8f, 1.0f);
        player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
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
        startDialogue = true;
    }
}
