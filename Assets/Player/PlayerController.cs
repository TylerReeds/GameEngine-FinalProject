using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Keybindings keybinds;
    public float moveDistance = 1.5f;
    public int PlayerHP = 10;

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
        inputManager.SetCommand("Undo", new UndoCommand(this));
        inputManager.SetCommand("Redo", new RedoCommand(this));
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

        inputManager.HandleInput("Undo");
        inputManager.HandleInput("Redo");

        if (PlayerHP <= 0)
        {
            Debug.Log("Player Dead");
            playerDiedEvent();
            enemyKilledEvent();
            StartCoroutine(Delay());
        }

        Debug.Log(PlayerHP);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        GameObject.Destroy(gameObject);
        //EditorApplication.ExitPlaymode();
        Application.Quit();
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
        if (previousInputs.Count > 0)
        {
            ICommand lastCommand = previousInputs.Peek();

            if (lastCommand is MoveUpCommand)
            {
                Debug.Log(lastCommand);
                MoveDown();
            }
            else if (lastCommand is MoveDownCommand)
            {
                Debug.Log(lastCommand);
                MoveUp();
            }
            else if (lastCommand is MoveLeftCommand)
            {
                Debug.Log(lastCommand);
                MoveRight();
            }
            else if (lastCommand is MoveRightCommand)
            {
                Debug.Log(lastCommand);
                MoveLeft();
            }
            inputToRedo = previousInputs.Pop();
        }
    }


    public void Redo()
    {
        if(inputToRedo != null)
        {
            if (inputToRedo.ToString() == "MoveUpCommand")
            {
                MoveDown();
                inputToRedo = null;
            }
            if (inputToRedo.ToString() == "MoveDownCommand")
            {
                MoveUp();
                inputToRedo = null;
            }
            if (inputToRedo.ToString() == "MoveLeftCommand")
            {
                MoveRight();
                inputToRedo = null;
            }
            if (inputToRedo.ToString() == "MoveRightCommand")
            {
                MoveLeft();
                inputToRedo = null;
            }
        }
    }
}
