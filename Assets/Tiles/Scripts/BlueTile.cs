using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTile : BaseTile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int randomX = Random.Range(0, 10);
            int randomY = Random.Range(0, 10);

            player.gameObject.transform.position = new Vector3(gameManager.potentialEnemySpawnsX[randomX], gameManager.potentialEnemySpawnsY[randomY], 0);
        }
    }
}
