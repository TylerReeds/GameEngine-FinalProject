using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkTile : BaseTile
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
            damage = -1;
        }
    }
}
