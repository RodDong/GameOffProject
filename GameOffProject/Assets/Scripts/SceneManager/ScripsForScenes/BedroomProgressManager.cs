using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BedroomProgressManager : MonoBehaviour
{
    public bool hasTriggered2, hasTriggered2_1;
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress1, progress2_0, progress2_1;
    [SerializeField] TextAsset finalText;
    [SerializeField] Collider2D TVcollider, bedCollider, doorCollider;
    [SerializeField] GameObject blackScreen;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 1) {
            TVcollider.enabled = true;
            ProcessProgress_1();
        }
        if (progressManager.currentProgress == 2) {
            bedCollider.enabled = true;
            ProcessProgress_2();
        }
        if (progressManager.currentProgress == -1) {
            ProcessProgress_final();
        }
    }

    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {
            if (hasTriggered2) {
                hasTriggered2 = false;
                ProcessBlackScreen();
                player.GetComponent<PlayerMove>().EnterDialogueMode();
                dialogueManager.EnterDialogueMode(progress2_1);
                hasTriggered2_1 = true;
                progressManager.date = 2;
            } else if (hasTriggered2_1) {
                hasTriggered2_1 = false;
                doorCollider.enabled = true;
                bedCollider.enabled = false;
            }
        }
    }
    

    private async void ProcessProgress_1() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }

    private void ProcessProgress_2() {
        doorCollider.enabled = false;
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2_0);
    }

    private async void ProcessBlackScreen() {
        blackScreen.SetActive(true);
        await Task.Delay(400);
        blackScreen.SetActive(false);
    }

    private async void ProcessProgress_final() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(finalText);
    }
}

