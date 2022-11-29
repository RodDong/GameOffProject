using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantProgressManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress2;
    [SerializeField] List<Collider2D> interactables = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();   
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress != 2) {
            foreach(Collider2D interactable in interactables) {
                interactable.enabled = false;
            }
        }

        if (progressManager.currentProgress == 2) {
            ProcessProgress_2();
        }
    }

    private void ProcessProgress_2() {
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2);
    }
}
