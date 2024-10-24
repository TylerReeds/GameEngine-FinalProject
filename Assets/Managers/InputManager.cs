using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Keybindings keybindings;
    public Dictionary<string, ICommand> movement = new Dictionary<string, ICommand>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void SetCommand(string key, ICommand command)
    {
        movement[key] = command;
    }

    public void HandleInput(string key)
    {
        if (Input.GetKeyDown(keybindings.CheckKey(key)))
        {
            if (movement.ContainsKey(key))
            {
                movement[key].Execute();
            }
        }
    }
}
