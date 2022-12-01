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
    private List<int> state_fight_bossB = new List<int>(){1, 2, 24, 30, 34, 42, 61, 70, 67, 52};
    private List<int> state_fight_doctor = new List<int>(){22, 4, 14, 15, 40, 41, 49, 50, 57, 62, 65, 71, 73};
    [SerializeField] TextAsset progress1;
    [SerializeField] Collider2D clinicDoorCollider, subwayCollider, workCollider;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (state_fight_bossB.Contains(progressManager.currentProgress)) {
            clinicDoorCollider.enabled = false;
            subwayCollider.enabled = false;
        } else {
            workCollider.enabled = false;
        }

        if (!state_fight_doctor.Contains(progressManager.currentProgress)) {
            clinicDoorCollider.enabled = false;
        }

        if (progressManager.currentProgress == 1) {
            ProcessProgress_1();
        }
        
        if (progressManager.currentProgress == 22) {
            subwayCollider.enabled = false;
        }
    }

    private async void ProcessProgress_1() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }
}
