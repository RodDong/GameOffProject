using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class BedroomProgressManager : MonoBehaviour
{
    public bool hasTriggered2, hasTriggered2_1;
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset progress1, progress2_0, progress2_1, progress60;
    [SerializeField] TextAsset finalText;
    [SerializeField] Collider2D TVcollider, bedCollider, doorCollider;
    [SerializeField] GameObject blackScreen;
    bool awake = false, end = false;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 1) {
            TVcollider.enabled = true;
            ProcessProgress_1();
        }
        if (progressManager.currentProgress == 2) {
            bedCollider.enabled = true;
            ProcessProgress_2();
        }
        if (progressManager.currentProgress == -1) {
            ProcessProgress_final();
        }
        if (progressManager.currentProgress == 60)
        {
            doorCollider.enabled = false;
            bedCollider.enabled = true;
        }
    }

    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {
            if (hasTriggered2) {
                hasTriggered2 = false;
                ProcessBlackScreen();
                if(progressManager.currentProgress == 2)
                {
                    player.GetComponent<PlayerMove>().EnterDialogueMode();
                    dialogueManager.EnterDialogueMode(progress2_1);
                }
                
                hasTriggered2_1 = true;
                progressManager.date = 2;
            } else if (hasTriggered2_1) {
                hasTriggered2_1 = false;
                doorCollider.enabled = true;
                bedCollider.enabled = false;
            }

            if (progressManager.currentProgress == 60 && awake)
            {
                awake = false;
                player.GetComponent<PlayerMove>().EnterDialogueMode();
                dialogueManager.EnterDialogueMode(progress60);
            }

            if (end) {

            }
        }
    }
    

    private async void ProcessProgress_1() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress1);
    }

    private void ProcessProgress_2() {
        doorCollider.enabled = false;
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2_0);
    }

    private async void ProcessBlackScreen() {
        blackScreen.SetActive(true);
        await Task.Delay(400);
        blackScreen.SetActive(false);
        awake = true;
    }

    private async void ProcessProgress_final() {
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(finalText);
        end = true;
    }

    public async void ProcessPlayerDeath() {
        SceneManager.LoadScene("1MCRoom", LoadSceneMode.Single);
        await Task.Delay(300);
        player.transform.position = new Vector3(3.47f, -1.0f, 1.0f);
        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}

