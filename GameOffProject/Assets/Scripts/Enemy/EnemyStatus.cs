using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus
{
    protected float MAX_HEALTH;
    protected float health;
    protected float happyATK;
    protected float happyDEF;
    protected float sadATK;
    protected float sadDEF;
    protected float angryATK;
    protected float angryDEF;

    public bool TakeDamage(float damage) {
        if (health <= damage) {
            health = 0;
            return true;
        } else {
            health -= damage;
            return false;
        }
    }

    void makeMove() {
        // ???????
    }
}
