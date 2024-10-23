using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //Factory Design Pattern
    //Provides a basis for all enemies to be built off of, child scripts inherit from this parent script and can just modify from there.
    public int health;
    public int damage;
    public int tileMovement; // If 1, an enemy can move 1 tile. If 2, an enemy can move 2 tiles, and so on.

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
