using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using Unity.VisualScripting;

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
            switch (name.Substring(5))
            {
                case "10street_outside_home":
                    player.transform.position = new Vector3(9.0f, -4.0f, 1.0f);
                    player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    break;
                case "11street_outside_restaurant":
                    player.transform.position = new Vector3(0.0f, 0.0f, 1.0f);
                    player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    break;
                case "12street_outside_clinic":
                    player.transform.position = new Vector3(-14.0f, -8.0f, 1.0f) ;
                    player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    break;
                default:
                    player.transform.position = new Vector3(0.0f, 0.0f, 1.0f);
                    player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;

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
