using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    public GameObject player;

    public int damage;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        // Child classes will override this and do x thing with Player
    }
}
