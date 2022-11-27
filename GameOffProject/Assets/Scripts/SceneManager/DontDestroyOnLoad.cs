using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        } else if (instance != this) {
            while (transform.childCount > 0) {
                foreach (Transform child in transform) {
                    DestroyImmediate(child.gameObject);
                }
            }
            Destroy(transform.gameObject);
        }
    }
}
