using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Keybindings keybindings;
    public Dictionary<InputAction, ICommand> movement = new Dictionary<InputAction, ICommand>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetCommand(InputAction action, ICommand command)
    {
        movement[action] = command;
    }

    public void HandleInput(InputAction action)
    {
        if (Input.GetKeyDown(keybindings.CheckKey(action)))
        {
            if (movement.ContainsKey(action))
            {
                movement[action].Execute();
            }
        }
    }
}
