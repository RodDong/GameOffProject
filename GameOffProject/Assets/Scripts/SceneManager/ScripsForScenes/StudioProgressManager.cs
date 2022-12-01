using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StudioProgressManager : MonoBehaviour
{
    [SerializeField] SetCienmamachineFollow camera;
    private ProgressManager progressManager;
    private GameObject player, lust;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress36, progress36_1, progress36_2, progress36_3, progress36_4;
    [SerializeField] List<Collider2D> interactables = new List<Collider2D>();
    [SerializeField] GameObject sim_stand, sim_sit, sim_dead;
    [SerializeField] GameObject blackScreen;
    [SerializeField] Collider2D to_bathroom, to_bar, clues;
    [SerializeField] InteractableManager bathroomInteractable;
    private bool hasSeenBar, hasSeenLust;
    private bool trigger1, trigger2, trigger3, trigger4, trigger5, trigger6;
    void Start()
    {
        clues.enabled = false;
        progressManager = FindObjectOfType<ProgressManager>();   
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 36) {
            
            ProcessProgress_36();
        }
    }


    private void Update() {

        if (!dialogueManager.dialogueIsPlaying) {
            if (bathroomInteractable.isInBath) {
                player.SetActive(true);
                player.transform.localScale = new Vector3(-1f,1f,1f);
                trigger1 = true;
                camera.FollowObjectWithName("Player");
                bathroomInteractable.isInBath = false;
                to_bathroom.enabled = false;
            }
            if (trigger1 && !trigger2) {
                print("...........");
                trigger2 = true;
                ProcessProgress_36_1();
            }
        } else {
            if (bathroomInteractable.isInBath && !trigger1 && player.activeSelf) {
                player.SetActive(false);
                camera.FollowObjectWithName("Lust");
            }
        }

        
    }

    private async void ProcessProgress_36() {
        
        sim_sit.SetActive(false);
        sim_stand.SetActive(true);
        sim_dead.SetActive(false);
        await Task.Delay(500);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36);
    }

    
    private void ProcessProgress_36_1() {
        
        print("...");
        sim_stand.transform.localScale = new Vector3(-1f,1f,1f);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36_2);
        
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
