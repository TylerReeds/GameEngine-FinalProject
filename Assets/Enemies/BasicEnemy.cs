using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBehaviour
{
    void Start()
    {
        health = 1;
        damage = 1;
        tileMovement = 1;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameManager.enemyDeath.Invoke(); // When this object is about to be destroyed, lets the GameManager know.
    }
}
