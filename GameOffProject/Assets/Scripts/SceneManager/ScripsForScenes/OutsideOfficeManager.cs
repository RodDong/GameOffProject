using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class OutsideOfficeManager : MonoBehaviour
{
    public bool hasTriggered1, hasTriggered22;
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    private List<int> state_fight_bossB = new List<int>(){24, 30, 34, 42, 61, 70, 67, 52};
    [SerializeField] TextAsset progress1, progress1_1;
    [SerializeField] TextAsset progress2, progress2B;
    [SerializeField] TextAsset progress22;
    [SerializeField] Collider2D door_to_outside, door_to_office, workplace;
    [SerializeField] GameObject blackScreen, bossSprite;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 1) {
            door_to_outside.enabled = false;
            door_to_office.enabled = false;
        }
        if (progressManager.currentProgress == 2) {
            door_to_outside.enabled = false;
            workplace.enabled = false;
            ProcessProgress_2();
        }
        if (state_fight_bossB.Contains(progressManager.currentProgress)) {
            door_to_outside.enabled = false;
            workplace.enabled = false;
            Processprogress2B();
        }
    }

    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {
            if (progressManager.currentProgress == 1 && hasTriggered1) {
                ProcessProgress_1();
            } else if (progressManager.currentProgress == 2) {
                if (progressManager.date == 1) {
                    blackScreen.SetActive(true);
                    SceneManager.LoadScene("7restaurant");
                    player.transform.position = new Vector3(-12.58f, -1.23f, 0.0f);
                    player.transform.localScale = new Vector3(-1, 1, 0);
                }
            } else if (progressManager.currentProgress == 22) {
                if (progressManager.date == 1 && !hasTriggered22) {
                    hasTriggered22 = true;
                    ProcessProgress_22();
                }
            } else if (progressManager.currentProgress == 36) {
                blackScreen.SetActive(true);
                SceneManager.LoadScene("11street_outside_restaurant", LoadSceneMode.Single);
                player.transform.position = new Vector3(-13.0f, -4.0f, 1.0f);
                player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
            } else if (progressManager.currentProgress == 60) {
                blackScreen.SetActive(true);
                SceneManager.LoadScene("10street_outside_home", LoadSceneMode.Single);
                player.transform.position = new Vector3(-8.78f, -3.0f, 1.0f);
                player.transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
            }
        }
    }

    private async void ProcessProgress_1() {
        hasTriggered1 = false;
        blackScreen.SetActive(true);
        bossSprite.SetActive(true);
        player.GetComponent<PlayerMove>().PlayerTurnLeft();
        await Task.Delay(800);
        blackScreen.SetActive(false);
        workplace.enabled = false;
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1_1);
    }

    private async void ProcessProgress_2() {
        await Task.Delay(500);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2);
    }

    private async void Processprogress2B() {
        await Task.Delay(500);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2B);
    }

    private async void ProcessProgress_22() {
        blackScreen.SetActive(true);
        bossSprite.SetActive(false);
        door_to_office.enabled = false;
        door_to_outside.enabled = true;
        await Task.Delay(500);
        blackScreen.SetActive(false);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress22);
    }
}
