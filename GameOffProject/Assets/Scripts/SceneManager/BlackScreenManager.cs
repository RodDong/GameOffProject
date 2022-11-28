using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenManager : MonoBehaviour
{
    float timer = 0.0f;

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
