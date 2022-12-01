using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class OutsideClinicManager : MonoBehaviour
{
    public bool hasTalkedNurse, hasTalkedDoc;
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    private PlayerMove playerObject;
    [SerializeField] Collider2D nurse, door_inside_clinic;
    [SerializeField] GameObject doctor_stand, blackScreen;
    [SerializeField] TextAsset progress22;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerObject = GameObject.FindObjectOfType<PlayerMove>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 22) {
            nurse.enabled = true;
        }
    }

    void Update()
    {
        if (!dialogueManager.dialogueIsPlaying) {
            if (hasTalkedNurse) {
                hasTalkedNurse = false;
                ProcessProgress_22();
            }
            if (hasTalkedDoc) {
                hasTalkedDoc = false;
                ProcessProgress22_1();
            }
        }
    }

    private async void ProcessProgress_22() {
        blackScreen.SetActive(true);
        doctor_stand.SetActive(true);
        player.GetComponent<PlayerMove>().PlayerTurnRight();
        await Task.Delay(500);
        blackScreen.SetActive(false);
        playerObject.EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress22);
        hasTalkedDoc = true;
    }

    private async void ProcessProgress22_1() {
        blackScreen.SetActive(true);
        doctor_stand.SetActive(false);
        await Task.Delay(500);
        blackScreen.SetActive(false);
        nurse.enabled = false;
        door_inside_clinic.enabled = true;
    }
}
