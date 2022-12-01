using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
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
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();
        chef.SetActive(false);
        if (progressManager.currentProgress == 1) {
            door_to_home.SetActive(false);
            ProcessProgress_1();
        } else if (progressManager.currentProgress == 2) {
            ProcessProgress_2();
        }

        if (progressManager.date == 2) {
            subwayStation.SetActive(true);
            door_to_home.SetActive(false);
        }
        Debug.Log(progressManager.playerCollidedWithChef);
        if(progressManager.currentProgress == 60 && !progressManager.playerCollidedWithChef)
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
        
        if (chefCollider.IsTouching(playerCollider) && !dialogueManager.dialogueIsPlaying)
        {
            player.GetComponent<PlayerMove>().EnterDialogueMode();
            dialogueManager.EnterDialogueMode(chefAEncounter);
            progressManager.playerCollidedWithChef = true;
        }
    }

}
