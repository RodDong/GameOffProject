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
    [SerializeField] TextAsset chefAEncounter;
    [SerializeField] GameObject subwayStation, door_to_home;
    [SerializeField] GameObject chef;
    bool playerCollidedWithChef = false;
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

        if(progressManager.currentProgress == 60 && !playerCollidedWithChef)
        {
            chef.SetActive(true);
        }

    }

    private async void ProcessProgress_1() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }

    private void ProcessProgress_2() {
        //subwayStation.SetActive(false);        
    }

    private async void ProcessProgress_60()
    {
        await Task.Delay(300);
        bool playerCollideWithChef = chef.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>());
        if (playerCollideWithChef && !playerCollidedWithChef)
        {
            player.GetComponent<PlayerMove>().EnterDialogueMode();
            dialogueManager.EnterDialogueMode(chefAEncounter);
            playerCollidedWithChef = true;
        }
    }
}
