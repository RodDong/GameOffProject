using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarManager : MonoBehaviour
{
    GameObject player;
    PlayerStatus playerStatus;
    [SerializeField] BattleManager battleManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBuffDeBuffDetails(int i)
    {
        GameObject effectIcon = battleManager.GetEffectTransfroms()[i - 1].gameObject;
        GameObject descriptionMenu = effectIcon.GetComponentsInChildren<RectTransform>(true)[1].gameObject;
        descriptionMenu.SetActive(true);
        string description = playerStatus.GetActiveEffects()[i-1].GetDescription();
        descriptionMenu.GetComponent<TextMeshProUGUI>().text = description;
        descriptionMenu.GetComponent<Transform>().position = new Vector2(Input.mousePosition.x + 10.0f, Input.mousePosition.y + 10.0f);
    }

    public void HideBuffDeBuffDetails(int i)
    {
        GameObject effectIcon = battleManager.GetEffectTransfroms()[i - 1].gameObject;
        GameObject descriptionMenu = effectIcon.GetComponentsInChildren<RectTransform>(true)[1].gameObject;
        descriptionMenu.SetActive(false);
    }
}
