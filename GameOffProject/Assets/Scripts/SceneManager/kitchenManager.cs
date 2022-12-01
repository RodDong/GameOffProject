using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using static UnityEngine.Rendering.DebugUI;

public class kitchenManager : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    [SerializeField] TextAsset playerNarrative, beforeBattle;
    [SerializeField] List<Collider2D> interactables = new List<Collider2D>();
    [SerializeField] GameObject chef_stand, chefDead;
    [SerializeField] GameObject blackScreen;
    [SerializeField] SetCienmamachineFollow camera;
    [SerializeField] TextAsset MakeChoice;
    Collider2D chefCollider, playerCollider;
    PlayerMove playerMove;
    bool isInAnim = false;
    bool narrativePlayed = false;
    bool beforeBattlePlayed = false;
    public bool hasTriggeredClue = true;
    public int clueCounter = 0;
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerMove = player.GetComponent<PlayerMove>();
        foreach (Collider2D interactable in interactables)
        {
            interactable.enabled = false;
        }

        chef_stand.SetActive(true);
        chefDead.SetActive(false);
        chefCollider = chef_stand.GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(clueCounter == 3 && !dialogueManager.dialogueIsPlaying)
        {
            player.GetComponent<PlayerMove>().EnterDialogueMode();
            dialogueManager.EnterDialogueMode(MakeChoice);
        }
        ProcessPlayerNarrative();
        ProcessBeforeBattle();
        if (isInAnim && playerMove.GetCurState() == PlayerMove.State.Talk && GameObject.FindGameObjectWithTag("Chef"))
        {
            camera.FollowObjectWithName("Chef");
        }
        else
        {
            camera.FollowObjectWithName("Player");
        }
    }

    private async void ProcessPlayerNarrative()
    {
        await Task.Delay(200);
        if (!dialogueManager.dialogueIsPlaying && !narrativePlayed)
        {
            playerMove.EnterDialogueMode();
            dialogueManager.EnterDialogueMode(playerNarrative);
            narrativePlayed = true;
        }
    }

    private void ProcessBeforeBattle()
    {
        if (playerCollider.IsTouching(chefCollider) && !dialogueManager.dialogueIsPlaying && !beforeBattlePlayed && narrativePlayed)
        {
            beforeBattlePlayed = true;
            isInAnim = true;
            playerMove.EnterDialogueMode();
            dialogueManager.EnterDialogueMode(beforeBattle);
        }
    }

    public async void ProcessPlayerDeath()
    {
        SceneManager.LoadScene("6kitchen", LoadSceneMode.Single);
        await Task.Delay(300);
        player.transform.position = new Vector3(-11.04f, -0.8f, 1.0f);
        player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

    public void ProcessEnemyDeath()
    {
        chef_stand.SetActive(false);
        chefDead.SetActive(true);
        foreach (Collider2D interactable in interactables)
        {
            interactable.enabled = true;
        }
    }
}
