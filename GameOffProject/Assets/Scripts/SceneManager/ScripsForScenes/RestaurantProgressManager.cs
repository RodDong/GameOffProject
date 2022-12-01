using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class RestaurantProgressManager : MonoBehaviour
{
    public bool hasTriggered2, hasEndedTalk2_0;
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress2, progress2_1, progress_60;
    [SerializeField] List<Collider2D> interactables = new List<Collider2D>();
    [SerializeField] GameObject boss_stand, boss_sit;
    [SerializeField] GameObject chef;
    [SerializeField] GameObject blackScreen;
    [SerializeField] Collider2D door_to_street, door_to_kitchen;
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
            door_to_street.enabled = false;
            door_to_kitchen.enabled = false;
            boss_sit.SetActive(false);
            boss_stand.SetActive(true);
            ProcessProgress_2();
        }
        chef.SetActive(progressManager.currentProgress == 60);


    }

    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {
            if (hasTriggered2 && progressManager.currentProgress == 2) {
                blackScreen.SetActive(true);
                player.transform.position = new Vector3(2.55f, -3.45f, 0.0f);
                player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                SceneManager.LoadScene("11street_outside_restaurant");
            } else if (hasEndedTalk2_0) {
                hasEndedTalk2_0 = false;
                boss_stand.SetActive(false);
                boss_sit.SetActive(true);
                ProcessBlackScreen();
            }
        }
    }

    private async void ProcessProgress_2() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2);
        hasEndedTalk2_0 = true;
    }

    private async void ProcessBlackScreen() {
        blackScreen.SetActive(true);
        await Task.Delay(400);
        blackScreen.SetActive(false);
    }

    private async void ProcessProgress_60()
    {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress_60);
    }
}
