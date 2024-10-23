using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTile : BaseTile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damage = -1; // Healing Tile
        }
    }
}
