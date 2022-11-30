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
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.GetCurState() == PlayerMove.State.Talk || playerObject.GetCurState() == PlayerMove.State.Battle) {
            return;
        }
        
        if (!dialogueManager)
        {
            dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        }
        UpdateDoor();
        UpdateDialogue();
    }

    private async void UpdateDoor()
    {
        string name = gameObject.name;
        if (name.StartsWith("Door_") && playerInRange && Input.GetMouseButtonUp(1))
        {
            string curScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(name.Substring(5), LoadSceneMode.Single);
            await Task.Delay(200);
            switch (name.Substring(5))
            {
                case "10street_outside_home":
                    player.transform.position = new Vector3(9.0f, -4.0f, 1.0f);
                    player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingOutsideClip;
                    break;
                case "11street_outside_restaurant":
                    if(curScene == "7restaurant")
                    {
                        player.transform.position = new Vector3(-2.69f, 0.0f, 1.0f);
                        player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    }
                    else
                    {
                        player.transform.position = new Vector3(2.62f, 0.0f, 1.0f);
                        player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    }
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingOutsideClip;
                    break;
                case "12street_outside_clinic":
                    Debug.Log(curScene);
                    if(curScene == "2outside_office")
                    {
                        player.transform.position = new Vector3(12.5f, -2.0f, 1.0f);
                        player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    }else if(curScene == "4outside_clinic")
                    {
                        player.transform.position = new Vector3(-7.57f, -2.0f, 1.0f);
                        player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    }
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingOutsideClip;
                    break;
                case "1MCRoom":
                    player.transform.position = new Vector3(6.7f, -2.0f, 1.0f);
                    player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "2outside_office":
                    if(curScene == "3inside_office")
                    {
                        player.transform.position = new Vector3(19.33f, -2.0f, 1.0f);
                        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        player.transform.position = new Vector3(-20.9f, -2.0f, 1.0f);
                        player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    }
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "4outside_clinic":
                    if (curScene == "5inside_clinic")
                    {
                        player.transform.position = new Vector3(1.45f, -1.0f, 1.0f);
                        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        player.transform.position = new Vector3(8.24f, 0.0f, 1.0f);
                        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "3inside_office":
                    player.transform.position = new Vector3(7.83f, -2.0f, 1.0f);
                    player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "5inside_clinic":
                    player.transform.position = new Vector3(-10.7f, -2.0f, 1.0f);
                    player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "6kitchen":
                    player.transform.position = new Vector3(-11.4f, -1.0f, 1.0f);
                    player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "7restaurant":
                    player.transform.position = new Vector3(-13.5f, -1.0f, 1.0f);
                    player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "8bar":
                    if(curScene == "11street_outside_restaurant")
                    {
                        player.transform.position = new Vector3(14.79f, -1.0f, 1.0f);
                        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        player.transform.position = new Vector3(-14.79f, -1.0f, 1.0f);
                        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
                    break;
                case "9studio":
                    player.transform.position = new Vector3(13.47f, -1.0f, 1.0f);
                    player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerMove>().walkingClip;
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
