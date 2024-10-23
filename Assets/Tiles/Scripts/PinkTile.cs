using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkTile : BaseTile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damage = 0;
        }
    }
}
