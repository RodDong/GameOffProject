using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;

public class InteractableManager : MonoBehaviour
{

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [HideInInspector] public GameObject player;
    private DialogueManager dialogueManager;

    private PlayerMove playerObject;
    bool playerInRange = false;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindWithTag("Player");
        //blackScreen = GameObject.FindGameObjectWithTag("Black");
        if (gameObject.name == "TV")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueManager)
        {
            dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        }
        UpdateTV();
        UpdateDoor();
        UpdateDialogue();
    }

    private void UpdateTV()
    {
        if (gameObject.name == "TV" && playerInRange && Input.GetMouseButtonUp(1))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private async void UpdateDoor()
    {
        string name = gameObject.name;
        if (name.StartsWith("Door_") && playerInRange && Input.GetMouseButtonUp(1))
        {
            
            SceneManager.LoadScene(name.Substring(5), LoadSceneMode.Single);
            await Task.Delay(200);
            player.transform.position = new Vector3(0.0f, 0.0f, 1.0f);
            if (name.Substring(5) == "10street_outside_home" || name.Substring(5) == "11street_outside_restaurant" || name.Substring(5) == "12street_outside_clinic")
            {
                player.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            }
            else
            {
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
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
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
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

}
