using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class InsideClinicManager : MonoBehaviour
{
    public bool hasTalkedDoctor, hasSpotted, hasTriggeredClue;
    private ProgressManager progressManager;
    private GameObject player;
    private PlayerMove playerObject;
    private DialogueManager dialogueManager;
    private BattleManager battleManager;
    private bool hasTriggered22;
    [SerializeField] GameObject doctor_sit, doctor_stand, doctor_died, blackScreen, door_outside, clues;
    [SerializeField] TextAsset progress22, progress22_1, progress_doc_end;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerObject = GameObject.FindObjectOfType<PlayerMove>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        battleManager = FindObjectOfType<BattleManager>();
        battleManager.gameObjectsInScene = GameObject.FindGameObjectWithTag("ObjectsToHide");

        // if (progressManager.currentProgress == 22) {
            doctor_sit.SetActive(true);
        // }
    }

    void Update()
    {
        if (!dialogueManager.dialogueIsPlaying) {
            if (hasTalkedDoctor) {
                hasTalkedDoctor = false;
                ProcessProgress22();
            }
            if (hasTriggered22) {
                hasTriggered22 = false;
                ProcessProgress22_1();
            }
            if (hasSpotted) {
                hasSpotted = false;
                ProcessProgress22_2();
            }
            if (hasTriggeredClue) {
                ProcessDoctorClues();
            }
        }
    }

    private async void ProcessProgress22() {
        blackScreen.SetActive(true);
        doctor_sit.SetActive(false);
        await Task.Delay(500);
        blackScreen.SetActive(false);
        hasTriggered22 = true;
    }

    private async void ProcessProgress22_1() {
        await Task.Delay(5000);

        blackScreen.SetActive(true);
        player.transform.position = new Vector3(-9.97f, -0.6679425f, 0.0f);
        playerObject.PlayerTurnLeft();
        await Task.Delay(500);
        blackScreen.SetActive(false);

        playerObject.EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress22);
        hasSpotted = true;
    }

    private async void ProcessProgress22_2() {
        blackScreen.SetActive(true);
        playerObject.PlayerTurnLeft();
        await Task.Delay(500);
        blackScreen.SetActive(false);

        doctor_stand.SetActive(true);
        playerObject.EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress22_1);
    }

    public void ProcessEnemyDeath() {
        doctor_stand.SetActive(false);
        doctor_died.SetActive(true);
        clues.SetActive(true);
    }

    public void ProcessPlayerDeath() {
        blackScreen.SetActive(true);
        player.transform.position = new Vector3(-9.97f, -0.6679425f, 0.0f);
        playerObject.PlayerTurnRight();
        SceneManager.LoadScene("5inside_clinic");
    }

    private async void ProcessDoctorClues() {
        hasTriggeredClue = false;
        await Task.Delay(400);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress_doc_end);
    }
}
