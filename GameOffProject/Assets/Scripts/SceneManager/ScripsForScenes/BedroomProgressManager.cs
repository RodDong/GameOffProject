using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BedroomProgressManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress1;
    [SerializeField] Collider2D TVcollider;
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
    }

    private async void ProcessProgress_1() {
        await Task.Delay(200);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }
}
