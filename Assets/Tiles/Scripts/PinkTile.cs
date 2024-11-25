using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkTile : BaseTile
{
    void Start()
    {
        damage = 1; // Healing, in this case.
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            controller.PlayerHP += damage; // += is healing
        }
    }
}
