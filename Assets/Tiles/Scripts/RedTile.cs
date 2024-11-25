using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTile : BaseTile
{
    void Start()
    {
        damage = 2;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            controller.PlayerHP -= damage;
        }
    }
}
