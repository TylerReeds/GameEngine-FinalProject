using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GameManager gameManager;

    private Stack<ICommand> previousInputs = new Stack<ICommand>();
    private ICommand inputToRedo;

    void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.SetCommand("Up", new MoveUpCommand(this));
        inputManager.SetCommand("Down", new MoveDownCommand(this));
        inputManager.SetCommand("Left", new MoveLeftCommand(this));
        inputManager.SetCommand("Right", new MoveRightCommand(this));
        inputManager.SetCommand("U", new UndoCommand(this));
        inputManager.SetCommand("R", new RedoCommand(this));
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

        inputManager.HandleInput("U");
        inputManager.HandleInput("R");

        if (PlayerHP <= 0)
        {
            playerDiedEvent();
            enemyKilledEvent();
        }
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, moveDistance, 0);
        previousInputs.Push(new MoveUpCommand(this));
        gameManager.playerInput.Invoke();
    }
    public void MoveDown()
    {
        transform.position -= new Vector3(0, moveDistance, 0);
        previousInputs.Push(new MoveDownCommand(this));
        gameManager.playerInput.Invoke();
    }
    public void MoveLeft()
    {
        transform.position -= new Vector3(moveDistance, 0, 0);
        previousInputs.Push(new MoveLeftCommand(this));
        gameManager.playerInput.Invoke();
    }
    public void MoveRight()
    {
        transform.position += new Vector3(moveDistance, 0, 0);
        previousInputs.Push(new MoveRightCommand(this));
        gameManager.playerInput.Invoke();
    }

    public void Undo()
    {
        if(previousInputs.Count > 0)
        {
            if (previousInputs.ElementAt(0).ToString() == "MoveUpCommand")
            {
                MoveDown();
            }
            if (previousInputs.ElementAt(0).ToString() == "MoveDownCommand")
            {
                MoveUp();
            }
            if (previousInputs.ElementAt(0).ToString() == "MoveLeftCommand")
            {
                MoveRight();
            }
            if (previousInputs.ElementAt(0).ToString() == "MoveRightCommand")
            {
                MoveLeft();
            }

            inputToRedo = previousInputs.ElementAt(0);

            previousInputs.Pop();
        }
    }

    public void Redo()
    {
        if(inputToRedo != null)
        {
            if (inputToRedo.ToString() == "MoveUpCommand")
            {
                MoveDown();
            }
            if (inputToRedo.ToString() == "MoveDownCommand")
            {
                MoveUp();
            }
            if (inputToRedo.ToString() == "MoveLeftCommand")
            {
                MoveRight();
            }
            if (inputToRedo.ToString() == "MoveRightCommand")
            {
                MoveLeft();
            }
        }
    }
}
