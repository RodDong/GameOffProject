using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProgress2 : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [HideInInspector] public GameObject player;
    private DialogueManager dialogueManager;
    private PlayerMove playerObject;
    bool playerInRange = false;
    private RestaurantProgressManager restaurantProgressManager;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        restaurantProgressManager = FindObjectOfType<RestaurantProgressManager>();
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
            if (Input.GetMouseButtonUp(1) && inkJSON != null && playerObject.CanUseInteractables())
            {
                playerObject.EnterDialogueMode();
                Debug.Log(dialogueManager);
                dialogueManager.EnterDialogueMode(inkJSON);
                restaurantProgressManager.hasTriggered2 = true;
            }
        } else
        {
            visualCue.SetActive(false);
        }
    }
}
