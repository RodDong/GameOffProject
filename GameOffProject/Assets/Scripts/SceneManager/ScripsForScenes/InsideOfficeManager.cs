using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public class InsideOfficeManager : MonoBehaviour
{
    public bool hasTriggeredClue;
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    private List<int> state_fight_bossB = new List<int>(){24, 30, 34, 42, 61, 70, 67, 52};
    [SerializeField] TextAsset progress2, progress2_1, progress2B;
    [SerializeField] GameObject boss_sit, boss_stand, boss_die;
    [SerializeField] GameObject door_to_outside, table;
    [SerializeField] GameObject blackScreen;

    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (progressManager.currentProgress == 2) {
            door_to_outside.SetActive(false);
            table.SetActive(false);
            ProcessProgress_2();
        }
        if (state_fight_bossB.Contains(progressManager.currentProgress)) {
            door_to_outside.SetActive(false);
            table.SetActive(false);
            ProcessProgress2B();
        }
    }

    void Update()
    {
        if (!dialogueManager.dialogueIsPlaying) {
            if (hasTriggeredClue) {
                ProcessProgress2_1();
            }
        }
    }

    private async void ProcessProgress_2() {
        boss_stand.SetActive(true);
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2);
    }

    private async void ProcessProgress2B() {
        boss_stand.SetActive(true);
        await Task.Delay(300);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2B);
    }

    private async void ProcessProgress2_1() {
        hasTriggeredClue = false;
        await Task.Delay(400);
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(progress2_1);
    }

    private void ProcessGoHome() {
        blackScreen.SetActive(true);
    }

    public async void ProcessPlayerDeath() {
        blackScreen.SetActive(true);
        SceneManager.LoadScene("2outside_office");
        await Task.Delay(200);
        player.transform.position = new Vector3(-19.54886f, -1.29f, 0.0f);
        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        player.GetComponent<PlayerMove>().PlayerTurnRight();
    }

    public void ProcessEnemyDeath() {
        boss_stand.SetActive(false);
        boss_die.SetActive(true);
        table.SetActive(true);
    }
}
