using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class StreetOutsideHomeManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress1;
    [SerializeField] GameObject subwayStation, door_to_home;
    [SerializeField] GameObject chef;
    Collider2D chefCollider, playerCollider;
    [SerializeField] TextAsset chefAEncounter;
    [SerializeField] GameObject blackCanvas;
    bool enteredDialogue = false;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();
        chef.SetActive(false);
        blackCanvas.SetActive(false);
        if (progressManager.currentProgress == 1) {
            door_to_home.SetActive(false);
            ProcessProgress_1();
        } else if (progressManager.currentProgress == 2) {
            ProcessProgress_2();
            if (progressManager.date == 1) {
                subwayStation.SetActive(false);
            }
        }

        if (progressManager.date == 2) {
            subwayStation.SetActive(true);
        }
        List<int> state_fight_chef = new List<int>(){60, 5, 9, 11, 17, 20, 21, 23, 26, 30, 32, 38, 69, 47, 48};
        if(state_fight_chef.Contains(progressManager.currentProgress) && !progressManager.playerCollidedWithChef)
        {
            chef.SetActive(true);
            subwayStation.SetActive(false);
        }
        else
        {
            chef.SetActive(false);
        }
        playerCollider = player.GetComponent<Collider2D>();
        chefCollider = chef.GetComponent<Collider2D>();

        if (progressManager.currentProgress == -1) {
            subwayStation.SetActive(false);
        }

    }

    private void Update()
    {
        ProcessProgress_60();
    }

    private async void ProcessProgress_1() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }

    private void ProcessProgress_2() {
        //subwayStation.SetActive(false);        
    }

    private void ProcessProgress_60()
    {
        
        if (chefCollider.IsTouching(playerCollider) && !dialogueManager.dialogueIsPlaying && !enteredDialogue)
        {
            player.GetComponent<PlayerMove>().EnterDialogueMode();
            dialogueManager.EnterDialogueMode(chefAEncounter);
            progressManager.playerCollidedWithChef = true;
            enteredDialogue = true;
        }
        if (!dialogueManager.dialogueIsPlaying && enteredDialogue)
        {
            blackCanvas.SetActive(true);
            TeleportToRestaurant();
        }
    }

    public async void TeleportToRestaurant()
    {
        enteredDialogue = false;
        SceneManager.LoadScene("7restaurant");
        await Task.Delay(300);
        player.transform.position = new Vector3(-14.32f, 0.0f, 1.0f);
        player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

}
