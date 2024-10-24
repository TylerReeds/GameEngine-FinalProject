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

    private InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.SetCommand("Up", new MoveUpCommand(this));
        inputManager.SetCommand("Down", new MoveDownCommand(this));
        inputManager.SetCommand("Left", new MoveLeftCommand(this));
        inputManager.SetCommand("Right", new MoveRightCommand(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -6.76f)
        {
            inputManager.HandleInput("Left");
        }

        if (transform.position.x < 6.74f)
        {
            inputManager.HandleInput("Right");
        }

        if (transform.position.y > -8.11f)
        {
            inputManager.HandleInput("Down");
        }

        if (transform.position.y < 5.39f)
        {
            inputManager.HandleInput("Up");
        }

        if (PlayerHP <= 0)
        {
            playerDiedEvent();
            enemyKilledEvent();
        }
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, moveDistance, 0);
    }
    public void MoveDown()
    {
        transform.position -= new Vector3(0, moveDistance, 0);
    }
    public void MoveLeft()
    {
        transform.position -= new Vector3(moveDistance, 0, 0);
    }
    public void MoveRight()
    {
        transform.position += new Vector3(moveDistance, 0, 0);
    }


}
