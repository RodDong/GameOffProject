using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OpeningManager : MonoBehaviour
{
    GameObject player;
    private DialogueManager dialogueManager;
    GameObject blackScreen;
    [SerializeField] TextAsset inkJson;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
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

        if (curState == PlayerMove.State.Battle)
        {
            gameObject.SetActive(false);
        }

        
        if (curState != PlayerMove.State.Talk && curState != PlayerMove.State.Battle)
        {
            player.GetComponent<PlayerMove>().EnterDialogueMode();
            dialogueManager.EnterDialogueMode(inkJson);
        }
    }
}
