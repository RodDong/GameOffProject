using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BarProgressManager : MonoBehaviour
{
    [SerializeField] SetCienmamachineFollow camera;
    private ProgressManager progressManager;
    private GameObject player, lust;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress36, progress36_1, progress36_2, progress36_3, progress36_4;
    [SerializeField] List<Collider2D> interactables = new List<Collider2D>();
    [SerializeField] GameObject sim_stand, sim_sit;
    [SerializeField] GameObject blackScreen;
    [SerializeField] Collider2D door_to_workspace, door_to_street;
    private bool hasSeenBar, hasSeenLust;
    private bool trigger1, trigger2, trigger3, trigger4, trigger5, trigger6;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();   
        player = GameObject.FindGameObjectWithTag("Player");
        lust = GameObject.FindGameObjectWithTag("Lust");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress != 2) {
            foreach(Collider2D interactable in interactables) {
                interactable.enabled = false;
            }
        }

        ProcessProgress_36();
        
    }


    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {
             if (trigger1 && !trigger2) {
                ProcessProgress_36_1();
             } else if (trigger2 && !trigger3) {
                ProcessProgress_36_2();
             } else if (trigger3 && !trigger4) {
                ProcessProgress_36_3();
             } else if (trigger4 && !trigger5) {
                ProcessProgress_36_4();
             } else if (trigger5 && !trigger6) {
                ProcessBlackScreen();
                sim_stand.SetActive(false);
                player.transform.position = new Vector3(-16.5f, -1.2f, 0f);
                door_to_street.enabled = false;
                trigger6 = true;
             }
        }
    }

    private async void ProcessProgress_36() {
        
        trigger1 = true;
        sim_sit.SetActive(false);
        sim_stand.SetActive(true);
        await Task.Delay(500);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36);
    }

    private async void ProcessProgress_36_1() {
        await Task.Delay(200);
        camera.FollowObjectWithName("Lust");
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36_1);
        trigger2 = true;
    }
    private async void ProcessProgress_36_2() {
        await Task.Delay(200);
        ProcessBlackScreen();
        camera.FollowObjectWithName("Player");
        lust.transform.position = new Vector3(3f,-1.2f,0f);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36_2);
        trigger3 = true;
    }

    private async void ProcessProgress_36_3() {
        await Task.Delay(200);
        lust.transform.position = new Vector3(-13f,-1.2f,0f);
        sim_sit.SetActive(false);
        sim_stand.SetActive(true);
        player.transform.position = new Vector3(-11f, -1.2f, 0f);
        blackScreen.SetActive(true);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36_3);
        trigger4 = true;
    }

    private async void ProcessProgress_36_4() {
        await Task.Delay(200);
        blackScreen.SetActive(false);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36_4);
        trigger5 = true;
    }

    private async void ProcessBlackScreen() {
        blackScreen.SetActive(true);
        await Task.Delay(400);
        blackScreen.SetActive(false);
    }
}
