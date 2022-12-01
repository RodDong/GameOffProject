using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarManager : MonoBehaviour
{
    GameObject player;
    PlayerStatus playerStatus;
    EnemyStatus enemyStatus;
    [SerializeField] BattleManager battleManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
        enemyStatus = GameObject.FindObjectOfType<EnemyStatus>();
        if (enemyStatus == null)
        {
            Debug.LogWarning("No Enemy Object in Scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowBuffDeBuffDetails(int index)
    {
        bool isPlayer = index >= 0;

        if (isPlayer)
        {
            GameObject effectIcon = battleManager.GetPlayerEffectTransfroms()[index-1].gameObject;
            GameObject descriptionMenu = effectIcon.GetComponentsInChildren<RectTransform>(true)[1].gameObject;
            descriptionMenu.SetActive(true);
            string description = playerStatus.GetActiveEffects()[index - 1].GetDescription();
            descriptionMenu.GetComponent<TextMeshProUGUI>().text = description;
            SetTextMaterial(descriptionMenu.GetComponent<TextMeshProUGUI>());
            descriptionMenu.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x + 10.0f, Input.mousePosition.y + 10.0f);
        }
        else
        {
            index = -index;
            GameObject effectIcon = battleManager.GetEnemyEffectTransfroms()[index - 1].gameObject;
            GameObject descriptionMenu = effectIcon.GetComponentsInChildren<RectTransform>(true)[1].gameObject;
            descriptionMenu.SetActive(true);
            string description = enemyStatus.GetActiveEffects()[index - 1].GetDescription();
            descriptionMenu.GetComponent<TextMeshProUGUI>().text = description;
            SetTextMaterial(descriptionMenu.GetComponent<TextMeshProUGUI>());
            descriptionMenu.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x + 10.0f, Input.mousePosition.y + 10.0f);
        }
        
    }

    public void HideBuffDeBuffDetails(int index)
    {
        bool isPlayer = index >= 0;

        if (isPlayer)
        {
            GameObject effectIcon = battleManager.GetPlayerEffectTransfroms()[index - 1].gameObject;
            GameObject descriptionMenu = effectIcon.GetComponentsInChildren<RectTransform>(true)[1].gameObject;
            descriptionMenu.SetActive(false);
        }
        else
        {
            index = -index;
            GameObject effectIcon = battleManager.GetEnemyEffectTransfroms()[index - 1].gameObject;
            GameObject descriptionMenu = effectIcon.GetComponentsInChildren<RectTransform>(true)[1].gameObject;
            descriptionMenu.SetActive(false);
        }
    }

    public void ResetEnemyStatus() {
        enemyStatus = FindObjectOfType<EnemyStatus>();
    }

    private void SetTextMaterial(TextMeshProUGUI text)
    {
        text.color = Color.cyan;
        text.fontStyle = FontStyles.Bold;
        text.fontSize = 15.0f;
    }
}
