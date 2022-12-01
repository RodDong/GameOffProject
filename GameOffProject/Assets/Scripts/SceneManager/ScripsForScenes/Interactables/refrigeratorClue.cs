using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refrigeratorClue : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [HideInInspector] public GameObject player;
    private DialogueManager dialogueManager;
    private PlayerMove playerObject;
    bool playerInRange = false;
    private kitchenManager kitchenManager;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        kitchenManager = FindObjectOfType<kitchenManager>();
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
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
        if (playerInRange && !dialogueManager.dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetMouseButtonUp(1) && playerObject.CanUseInteractables())
            {
                kitchenManager.hasTriggeredClue = true;
                playerObject.EnterDialogueMode();
                dialogueManager.EnterDialogueMode(inkJSON);
                kitchenManager.clueCounter++;
                GetComponent<Collider2D>().enabled = false;
                player.GetComponent<PlayerStatus>().addClue(2);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
}
