using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTile : BaseTile
{
    void Start()
    {
        damage = 2; // Healing, in this case.
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            controller.PlayerHP += damage; // += is healing
        }
    }
}
