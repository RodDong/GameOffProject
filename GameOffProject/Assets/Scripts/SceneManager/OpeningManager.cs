using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class OpeningManager : MonoBehaviour
{
    GameObject player;
    GameObject blackScreen;
    private DialogueManager dialogueManager;
    private ProgressManager progressManager;
    [SerializeField] TextAsset dreamJson;
    [SerializeField] TextAsset endDreamJson;
    [SerializeField] GameObject canvas;
    bool endDream = false;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        blackScreen = GameObject.FindGameObjectWithTag("Black");
        player.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0.5f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            blackScreen.SetActive(false);
        }

        PlayerMove.State curState = player.GetComponent<PlayerMove>().GetCurState();

        if (curState != PlayerMove.State.Talk && curState != PlayerMove.State.Battle)
        {
            if (endDream) {
                exitEndDream();
            } else {
                player.GetComponent<PlayerMove>().EnterDialogueMode();
                dialogueManager.EnterDialogueMode(dreamJson);
            }
        }

    }

    public void playEndDream() {
        endDream = true;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerMove>().EnterDialogueMode();
        dialogueManager.EnterDialogueMode(endDreamJson);
    }

    async void exitEndDream() {
        canvas.SetActive(false);
        SceneManager.LoadScene("1MCRoom", LoadSceneMode.Single);
        progressManager.transitionToNextState(0);
        await Task.Delay(200);
        player.transform.position = new Vector3(-10.0f, 0.0f, 1.0f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        canvas.SetActive(true);
    }
}
