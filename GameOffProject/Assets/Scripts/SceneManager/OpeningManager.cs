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
        TeleportBasedOnProgress();
    }

    public async void TeleportBasedOnProgress() {
        switch (progressManager.currentProgress)
        {
            case 11:
                SceneManager.LoadScene("10street_outside_home", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(9.0f, -4.0f, 1.0f);
                player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                break;
            case 2:
                SceneManager.LoadScene("11street_outside_restaurant", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(-2.69f, 0.0f, 1.0f);
                player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                break;
            case 4:
                SceneManager.LoadScene("12street_outside_clinic", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(12.5f, -2.0f, 1.0f);
                player.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                break;
            case 1: case -1: case 8:
                SceneManager.LoadScene("1MCRoom", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(6.7f, -2.0f, 1.0f);
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 96:
                SceneManager.LoadScene("2outside_office", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(-20.9f, -2.0f, 1.0f);
                player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                break;
            case 95:
                SceneManager.LoadScene("4outside_clinic", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(8.24f, 0.0f, 1.0f);
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 94:
                SceneManager.LoadScene("3inside_office", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(7.83f, -2.0f, 1.0f);
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 93:
                SceneManager.LoadScene("5inside_clinic", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(-10.7f, -2.0f, 1.0f);
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 92:
                SceneManager.LoadScene("6kitchen", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(-11.4f, -1.0f, 1.0f);
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 91:
                SceneManager.LoadScene("7restaurant", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(-13.5f, -1.0f, 1.0f);
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 36:
                SceneManager.LoadScene("8bar", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(14.79f, -1.0f, 1.0f);
                        player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 89:
                SceneManager.LoadScene("9studio", LoadSceneMode.Single);
                await Task.Delay(200);
                player.transform.position = new Vector3(13.47f, -1.0f, 1.0f);
                player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            default:
                break;

        }

        if (progressManager.currentProgress == 0)
            return;

        await Task.Delay(200); 
        player.GetComponent<SpriteRenderer>().enabled = true;
        canvas.SetActive(true);
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

        if (curState != PlayerMove.State.Talk && curState != PlayerMove.State.Battle && progressManager.currentProgress == 0)
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
