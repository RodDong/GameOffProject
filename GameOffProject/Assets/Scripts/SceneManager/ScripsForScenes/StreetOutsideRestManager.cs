using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StreetOutsideRestManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress2;
    [SerializeField] GameObject doorToBar;
    [SerializeField] GameObject doorToRestaurant;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 2) {
            ProcessProgress_2();
        }
    }

    private async void ProcessProgress_2() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2);
        doorToBar.SetActive(false);
        doorToRestaurant.SetActive(false);
    }
}
