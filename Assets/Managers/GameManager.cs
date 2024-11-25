using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton Design Pattern
    public WorldBuilderController levelGenerator;

    public GameObject basicEnemy;
    public List<GameObject> enemyList;

    public int currentLevel = 1;

    public int numOfEnemiesAlive = 0;

    public UnityEvent playerInput;
    public UnityEvent enemyDeath;

    public float[] potentialEnemySpawnsX = new float[10]{ // X values of every tile that spawns in a level.
        -6.76f, -5.26f, -3.76f, -2.26f, -0.76f, 0.74f, 2.24f, 3.74f, 5.24f, 6.74f
    };

    public float[] potentialEnemySpawnsY = new float[10]{ // Y values of every tile that spawns in a level.
        -8.11f, -6.61f, -5.11f, -3.61f, -2.11f, -0.61f, 0.89f, 2.39f, 3.89f, 5.39f
    };

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        numOfEnemiesAlive = enemyList.Count;

        playerInput.AddListener(enemyMovement);
        enemyDeath.AddListener(enemyDeathEvent);
        enemySpawning();
    }

    void Update()
    {
        if(numOfEnemiesAlive <= 0) // meaning, if level is beaten
        {
            levelGenerator.Rebuild();
            //rebuild, reset enemyList, reset numOfEnemies Alive
            enemyList.Clear();
            numOfEnemiesAlive = enemyList.Count;
            currentLevel += 1;
            enemySpawning();
        }
    }

    public void enemySpawning()
    {
        int numOfCurrentEnemies = currentLevel * 2;
        numOfEnemiesAlive = numOfCurrentEnemies;

        for(int i = numOfCurrentEnemies; i > 0; i--)
        {
            int xSpawn = Random.Range(0, 10);
            int ySpawn = Random.Range(0, 10);
            Instantiate(basicEnemy, new Vector3(potentialEnemySpawnsX[xSpawn], potentialEnemySpawnsY[ySpawn], 0), Quaternion.identity);
        }

        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in foundEnemies)
        {
            enemyList.Add(enemy);
        }
    }

    public void enemyDeathEvent()
    {
        numOfEnemiesAlive--;

        enemyList.Clear();

        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in foundEnemies)
        {
            enemyList.Add(enemy);
        }
    }

    public void enemyMovement() // The enemies will only move after the player moves
    {
        if(enemyList is not null)
        {
            foreach (GameObject enemy in enemyList)
            {
                int UpDownORLeftRight = Random.Range(0, 2); //Determines if enemy is going up and down, or left and right
                bool UpDown = false;
                bool LeftRight = false;

                int nextYRandom = Random.Range(0, 2); // If enemy is going up and down, determines whether going up or down
                float plusY = 0;

                int nextXRandom = Random.Range(0, 2); // If enemy is going left and right, determines whether going left or right
                float plusX = 0;

                if (UpDownORLeftRight == 0)  // Enemy is going up or down
                {
                    UpDown = true;
                    LeftRight = false;
                }
                else // Enemy is going left or right
                {
                    UpDown = false;
                    LeftRight = true;
                }

                if (UpDown == true)
                {
                    if (nextYRandom == 0) // Enemy is going up
                    {
                        if (enemy.transform.position.y! < 5.39f) // Highest Tiles
                        {
                            plusY = 1.5f;
                        }
                    }
                    else // Enemy is going down
                    {
                        if (enemy.transform.position.y! > -8.11f) // Lowest Tiles
                        {
                            plusY = -1.5f;
                        }
                    }
                }

                if (LeftRight == true)
                {
                    if (nextXRandom == 0) // Enemy is going right
                    {
                        if (enemy.transform.position.x! < 6.74f) // Rightmost Tiles
                        {
                            plusX = 1.5f;
                        }
                    }
                    else // Enemy is going left
                    {
                        if (enemy.transform.position.x! > -6.76f) // Leftmost Tiles
                        {
                            plusX = -1.5f;
                        }
                    }
                }

                enemy.transform.position = new Vector3(enemy.transform.position.x + plusX, enemy.transform.position.y + plusY, 0);

                plusY = 0;
                plusX = 0;
            }
        }

        //Debug.Log("test");
    }
}
