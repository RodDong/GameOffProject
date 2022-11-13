using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    protected float MAX_HEALTH;
    protected float health;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float damage) {
        health -= damage;
    }

    void makeMove() {
        // ???????
    }
}
