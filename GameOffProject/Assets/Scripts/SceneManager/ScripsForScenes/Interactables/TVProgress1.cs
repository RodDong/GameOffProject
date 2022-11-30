using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVProgress1 : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [HideInInspector] public GameObject player;
    private DialogueManager dialogueManager;
    private PlayerMove playerObject;
    bool playerInRange = false;
    bool dialogueEnded = false;
    private BedroomProgressManager bedroomProgressManager;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        bedroomProgressManager = FindObjectOfType<BedroomProgressManager>();
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
    }

    private void Update() {
        if (gameObject.name == "TV" && playerInRange && Input.GetMouseButtonUp(1))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
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
            if (Input.GetMouseButtonUp(1) && inkJSON != null && playerObject.CanUseInteractables())
            {
                playerObject.EnterDialogueMode();
                Debug.Log(dialogueManager);
                dialogueManager.EnterDialogueMode(inkJSON);
                GetComponent<Collider2D>().enabled = false;
                dialogueEnded = true;
            }
        } else if (dialogueEnded && !dialogueManager.dialogueIsPlaying) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        } else
        {
            visualCue.SetActive(false);
        }
    }
}
