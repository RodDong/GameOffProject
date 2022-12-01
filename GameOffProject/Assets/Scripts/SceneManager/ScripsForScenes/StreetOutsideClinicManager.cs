using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class StreetOutsideClinicManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress1;
    [SerializeField] Collider2D clinicDoorCollider, subwayCollider;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 1) {
            clinicDoorCollider.enabled = false;
            subwayCollider.enabled = false;
            ProcessProgress_1();
        } else if (progressManager.currentProgress == 2) {
            clinicDoorCollider.enabled = false;
            subwayCollider.enabled = false;
        }

    }

    private async void ProcessProgress_1() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }
}
