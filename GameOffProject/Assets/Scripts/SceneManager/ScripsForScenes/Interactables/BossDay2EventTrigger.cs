using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDay2EventTrigger : MonoBehaviour
{
    private ProgressManager progressManager;
    private GameObject player;
    private DialogueManager dialogueManager;
    private bool hasTriggered = false;
    [SerializeField] TextAsset progress2B;
    private List<int> state_fight_bossB = new List<int>(){24, 30, 34, 42, 61, 70, 67, 52};
    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !hasTriggered) {
            hasTriggered = true;
            player.GetComponent<PlayerMove>().EnterDialogueMode();
            dialogueManager.EnterDialogueMode(progress2B);
        }
    }
}
