using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputAction
{
    Up,
    Down,
    Left,
    Right,
    Undo,
    Redo,
}

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    public KeyCode up, down, left, right, undo, redo, none; 

    public KeyCode CheckKey(InputAction action)
    {
        switch (action)
        {
            case InputAction.Up:
                return up;

            case InputAction.Down:
                return down;

            case InputAction.Left:
                return left;

            case InputAction.Right:
                return right;

            case InputAction.Undo:
                return undo;

            case InputAction.Redo:
                return redo;

            default:
                return none;
        }
    }
}

