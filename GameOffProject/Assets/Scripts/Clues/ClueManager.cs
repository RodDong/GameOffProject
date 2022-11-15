using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;

public class ClueManager : MonoBehaviour
{

    [SerializeField] GameObject clueButtons;
    [SerializeField] Button buttonPrefab;
    [SerializeField] GameObject Description;
    [SerializeField] GameObject Clues;
    GameObject mPlayer;
    PlayerStatus mPlayerStatus;

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mPlayerStatus = mPlayer.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
