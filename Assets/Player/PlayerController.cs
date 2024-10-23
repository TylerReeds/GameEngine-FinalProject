using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Keybindings keybinds;
    private float moveDistance = 1.5f;
    public int PlayerHP = 100;

    public delegate void PlayerDied();
    public event PlayerDied playerDiedEvent;

    public delegate void EnemyKilled();
    public event EnemyKilled enemyKilledEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keybinds.CheckKey("Up")))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(keybinds.CheckKey("Down")))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(keybinds.CheckKey("Left")))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(keybinds.CheckKey("Right")))
        {
            MoveRight();
        }

        if (PlayerHP <= 0)
        {
            playerDiedEvent();
            enemyKilledEvent();
        }
    }

    void MoveUp()
    {
        transform.position += new Vector3(0, moveDistance, 0);
    }
    void MoveDown()
    {
        transform.position -= new Vector3(0, moveDistance, 0);
    }
    void MoveLeft()
    {
        transform.position -= new Vector3(moveDistance, 0, 0);
    }
    void MoveRight()
    {
        transform.position += new Vector3(moveDistance, 0, 0);
    }


}
