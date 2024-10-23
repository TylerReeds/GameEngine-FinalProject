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
}
