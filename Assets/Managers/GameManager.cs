using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance; // Singleton Design Pattern
    public WorldBuilderController levelGenerator;

    public GameObject basicEnemy;
    public GameObject[] enemyList;

    public int currentLevel = 1;

    public int numOfEnemiesAlive = 0;

    void Start()
    {
        if(instance != this && instance != null)
        {
            instance = this;
        }

        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        numOfEnemiesAlive = enemyList.Length;
    }

    void Update()
    {
        if(numOfEnemiesAlive <= 0) // meaning, if level is beaten
        {
            //rebuild, reset enemyList, reset numOfEnemies Alive
            System.Array.Clear(enemyList, 0, enemyList.Length);
            enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            numOfEnemiesAlive = enemyList.Length;
            currentLevel += 1;
        }
    }

    public IEnumerator enemyMovement()
    {
        yield return new WaitUntil(() => Input.anyKeyDown); // Waits for the player to move
    }
}
