using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BedroomProgressManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] GameObject blackScreen;
    [SerializeField] TextAsset progress1;
    [SerializeField] Collider2D TVcollider;
    [SerializeField] TextAsset progress2;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress != 1) {
            TVcollider.enabled = false;
        }
        if (progressManager.currentProgress == 1) {
            ProcessProgress_1();
        }
        if (progressManager.currentProgress == 2) {
            ProcessProgress_2();
        }
    }

    private float time = 0;
    private float triggerTime = 0;
    async void Update() {
        if (time < 2.0) {
            time += Time.deltaTime;
        } else {
            if (!dialogueManager.dialogueIsPlaying && triggerTime == 0) {
                triggerTime = time;
                blackScreen.SetActive(true);
                await Task.Delay(2000);
                blackScreen.SetActive(false);
            } 
        }
    }
    

    private async void ProcessProgress_1() {
        await Task.Delay(200);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }

    private async void ProcessProgress_2() {
        await Task.Delay(200);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2);
    }
}

