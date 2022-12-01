using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCienmamachineFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FollowObjectWithName(string name) {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag(name).transform;
    }
}
