using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorClues : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [HideInInspector] public GameObject player;
    private DialogueManager dialogueManager;
    private PlayerMove playerObject;
    bool playerInRange = false;
    private InsideClinicManager insideClinicManager;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        insideClinicManager = FindObjectOfType<InsideClinicManager>();
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
    }

    private void Update() {
        UpdateDialogue();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerObject = collider.gameObject.GetComponent<PlayerMove>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerObject = null;
            playerInRange = false;
        }
    }

    private void UpdateDialogue()
    {
        if(playerInRange && !dialogueManager.dialogueIsPlaying && name == "OnRoomEnter")
        {
            playerObject.EnterDialogueMode();
            dialogueManager.EnterDialogueMode(inkJSON);
            gameObject.SetActive(false);
        }
        else if (playerInRange && !dialogueManager.dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetMouseButtonUp(1) && playerObject.CanUseInteractables()) {
                insideClinicManager.hasTriggeredClue = true;
                playerObject.EnterDialogueMode();
                dialogueManager.EnterDialogueMode(inkJSON);
                GetComponent<Collider2D>().enabled = false;
            }
        } else
        {
            visualCue.SetActive(false);
        }
    }
}
