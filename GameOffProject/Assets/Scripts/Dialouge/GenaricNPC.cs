using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenaricNPC : MonoBehaviour
{

    public GameObject Button;
    public GameObject talkUI;

    [Header("Dialouge Script")]
    public TextAsset textFile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.gameObject.GetComponent<DialougeSystem>().textFile = textFile;
            talkUI.SetActive(true);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Button.SetActive(false);
            talkUI.SetActive(false);
            talkUI.gameObject.GetComponent<DialougeSystem>().index = 0;
            //talkUI.
        }

    }
}
