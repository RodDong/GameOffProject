using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class StudioProgressManager : MonoBehaviour
{
    [SerializeField] SetCienmamachineFollow camera;
    private ProgressManager progressManager;
    private GameObject player, lust;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress36, progress36_1, progress36_2, makeChoiceJson;
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

        ProcessProgress_36();
        
    }


    private void Update() {

        if (!dialogueManager.dialogueIsPlaying) {
            if (bathroomInteractable.isInBath) {
                player.SetActive(true);
                player.transform.localScale = new Vector3(-1f,1f,1f);
                trigger1 = true;
                camera.FollowObjectWithName("Player");
                bathroomInteractable.isInBath = false;
                to_bathroom.gameObject.SetActive(false);
            }
            if (trigger1 && !trigger2) {
                ProcessProgress_36_1();
            } else if (trigger2 && !trigger3) {
                sim_stand.SetActive(false);
                /*if (player.GetComponent<PlayerMove>().GetCurState() != PlayerMove.State.Battle) {
                    ProcessProgress_36_2();
                }*/
            }


            if (trigger4 && !trigger5) {
                trigger5 = true;
                makeChoice();
            }

        } else {
            if (bathroomInteractable.isInBath && !trigger1 && player.activeSelf) {
                ProcessBlackScreen();
                sim_stand.transform.position = new Vector3(-6f, -1.2f,0f);
                player.SetActive(false);
                camera.FollowObjectWithName("Lust");
            }


            if (trigger3 && !trigger4) {
                trigger4 = true;
            }

            
        }

        
    }

    private async void ProcessProgress_36() {
        
        sim_sit.SetActive(false);
        sim_stand.SetActive(true);
        sim_dead.SetActive(false);
        to_bar.enabled = false;
        await Task.Delay(500);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36);
    }

    
    private void ProcessProgress_36_1() {
        sim_stand.transform.localScale = new Vector3(-1f,1f,1f);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress36_2);
        trigger2 = true;
    }
    public async void ProcessProgress_36_2() {
        ProcessBlackScreen();
        await Task.Delay(200);
        sim_dead.SetActive(true);
        clues.enabled = true;
        sim_stand.SetActive(false);
        trigger3 = true;
    }

    private async void ProcessBlackScreen() {
        blackScreen.SetActive(true);
        await Task.Delay(400);
        blackScreen.SetActive(false);
    }

    public async void ProcessPlayerDeath() {
        SceneManager.LoadScene("9Studio", LoadSceneMode.Single);
        await Task.Delay(300);
        player.transform.position = new Vector3(13.47f, -1.0f, 1.0f);
        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void makeChoice() {
        clues.gameObject.SetActive(false);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(makeChoiceJson);
    }
}
