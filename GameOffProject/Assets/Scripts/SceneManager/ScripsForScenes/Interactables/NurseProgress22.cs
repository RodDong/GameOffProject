using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class NurseProgress22 : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [HideInInspector] public GameObject player;
    private DialogueManager dialogueManager;
    private PlayerMove playerObject;
    bool playerInRange = false;
    private OutsideClinicManager outsideClinicManager;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        outsideClinicManager = FindObjectOfType<OutsideClinicManager>();
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
                outsideClinicManager.hasTalkedNurse = true;
                playerObject.EnterDialogueMode();
                dialogueManager.EnterDialogueMode(inkJSON);
            }
        } else
        {
            visualCue.SetActive(false);
        }
    }
}
