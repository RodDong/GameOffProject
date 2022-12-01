using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightAudio : MonoBehaviour
{
    [SerializeField] AudioSource supervisorAudio, chefAudio, lustAudio, doctorAudio;
    GameObject player;
    PlayerMove playerMove;
    EnemyStatus enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        enemy = GameObject.FindObjectOfType<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMove.GetCurState() == PlayerMove.State.Battle)
        {
            switch (enemy.enemyName)
            {
                case "Chef":
                    if (!chefAudio.isPlaying)
                    {
                        chefAudio.Play();
                    }
                    break;
                case "Doctor":
                    if (!doctorAudio.isPlaying)
                    {
                        doctorAudio.Play();
                    }
                    break;
                case "Lust":
                    if (!lustAudio.isPlaying)
                    {
                        lustAudio.Play();
                    }
                    break;
                case "Supervisor":
                    if (!supervisorAudio.isPlaying)
                    {
                        supervisorAudio.Play();
                    }
                    break;
            }
        }
        else
        {
            chefAudio.Stop();
            doctorAudio.Stop();
            lustAudio.Stop();
            supervisorAudio.Stop();
        }
    }
}
