using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public class BlackScreenManager : MonoBehaviour
{
    float timer = 0.0f;

    private void Start()
    {
        FindObjectOfType<InteractableManager>().enabled = false;
        FindObjectOfType<PlayerMove>().enabled = false;
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
            gameObject.SetActive(false);
            FindObjectOfType<InteractableManager>().enabled = true;
            FindObjectOfType<PlayerMove>().enabled = true;
        }
    }
}
