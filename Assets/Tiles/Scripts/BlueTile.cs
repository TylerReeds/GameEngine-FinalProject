using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTile : BaseTile
{
    void Start()
    {
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damage = 0;

            Debug.Log("test");

            int randomX = Random.Range(0, 10);
            int randomY = Random.Range(0, 10);

            player.gameObject.transform.position = new Vector3(gameManager.potentialEnemySpawnsX[randomX], gameManager.potentialEnemySpawnsY[randomY], 0);
        }
    }
}
