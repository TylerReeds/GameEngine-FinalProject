using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeTile : BaseTile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damage = 1; // Fire Tile
        }
    }
}
