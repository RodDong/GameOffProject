using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenManager : MonoBehaviour
{
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0.5f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
