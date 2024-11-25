using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerController : Subject
{
    public Keybindings keybinds;
    public float moveDistance = 1.5f;

    public int _playerHP = 10;
    public int PlayerHP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            NotifyObservers(); // Notify observers whenever PlayerHP changes
        }
    }

    private UIManager uiManager; 

    public delegate void PlayerDied();
    public event PlayerDied playerDiedEvent;

    public delegate void EnemyKilled();
    public event EnemyKilled enemyKilledEvent;

    private InputManager inputManager;
    [SerializeField] private GameManager gameManager;

    private Stack<ICommand> previousInputs = new Stack<ICommand>();
    private ICommand inputToRedo;

    void Awake()
    {
        uiManager = gameObject.AddComponent<UIManager>();
    }

    void OnEnable()
    {
        if (uiManager)
            Attach(uiManager);       
    }

    void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.SetCommand(InputAction.Up, new MoveUpCommand(this));
        inputManager.SetCommand(InputAction.Down, new MoveDownCommand(this));
        inputManager.SetCommand(InputAction.Left, new MoveLeftCommand(this));
        inputManager.SetCommand(InputAction.Right, new MoveRightCommand(this));
        inputManager.SetCommand(InputAction.Undo, new UndoCommand(this));
        inputManager.SetCommand(InputAction.Redo, new RedoCommand(this));
    }

    // Update is called once per frame
    void Update()
    {
        // Define the grid bounds
        float minX = -6.76f;
        float maxX = 6.74f;
        float minY = -8.11f;
        float maxY = 5.39f;

        //Logic to ensure the player cannot move past bounds when moveDistance is 1 tile
        if (moveDistance == 1.5f)
        {
            if (transform.position.x > minX)
            {
                inputManager.HandleInput(InputAction.Left);
            }

            if (transform.position.x < maxX)
            {
                inputManager.HandleInput(InputAction.Right);
            }
 
            if (transform.position.y > minY)
            {
                inputManager.HandleInput(InputAction.Down);
            }

            if (transform.position.y < maxY)
            {
                inputManager.HandleInput(InputAction.Up);
            }
        }
        //Logic to ensure the player cannot move past bounds when moveDistance is 2 tiles
        else if (moveDistance == 3.0f)
        {
            if (transform.position.x > (minX + 1.5f))
            {
                inputManager.HandleInput(InputAction.Left);
            }

            if (transform.position.x < (maxX - 1.5f))
            {
                inputManager.HandleInput(InputAction.Right);
            }

            if (transform.position.y > (minY + 1.5f))
            {
                inputManager.HandleInput(InputAction.Down);
            }

            if (transform.position.y < (maxY - 1.5f))
            {
                inputManager.HandleInput(InputAction.Up);
            }
        }
        //Logic to ensure the player cannot move past bounds when moveDistance is 3 tiles
        else if (moveDistance == 4.5f)
        {
            if (transform.position.x > (minX + 3.0f))
            {
                inputManager.HandleInput(InputAction.Left);
            }

            if (transform.position.x < (maxX - 3.0f))
            {
                inputManager.HandleInput(InputAction.Right);
            }

            if (transform.position.y > (minY + 3.0f))
            {
                inputManager.HandleInput(InputAction.Down);
            }

            if (transform.position.y < (maxY - 3.0f))
            {
                inputManager.HandleInput(InputAction.Up);
            }
        }

        inputManager.HandleInput(InputAction.Undo);
        inputManager.HandleInput(InputAction.Redo);

        if (PlayerHP <= 0)
        {
            Debug.Log("Player Dead");
            playerDiedEvent();
            enemyKilledEvent();
            StartCoroutine(Delay());
        }

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
