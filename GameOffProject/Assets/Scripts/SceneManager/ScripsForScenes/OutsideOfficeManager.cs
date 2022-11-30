using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class OutsideOfficeManager : MonoBehaviour
{
    public bool hasTriggered1;
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress1, progress1_1;
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
    }

    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {
            if (progressManager.currentProgress == 1 && hasTriggered1) {
                ProcessProgress_1();
            } else if (progressManager.currentProgress == 2) {
                blackScreen.SetActive(true);
                SceneManager.LoadScene("7restaurant");
                player.transform.position = new Vector3(-12.58f, -1.23f, 0.0f);
                player.transform.localScale = new Vector3(-1, 1, 0);
            } else if (progressManager.currentProgress == 22) {
                //
            } else if (progressManager.currentProgress == 36) {
                //
            } else if (progressManager.currentProgress == 60) {
                //
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
}
