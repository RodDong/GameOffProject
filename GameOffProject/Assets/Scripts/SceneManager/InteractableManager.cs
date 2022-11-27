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
        }
    }

    private void UpdateClues()
    {

    }

    private void UpdateDialogue()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetMouseButtonUp(1) && inkJSON != null)
            {
                playerObject.EnterDialogueMode();
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
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
