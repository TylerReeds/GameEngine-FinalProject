using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    public KeyCode up, down, left, right, attack, pause; 

    public KeyCode CheckKey(string key)
    {
        switch (key)
        {
            case "Up":
                return up;

            case "Down":
                return down;

            case "Left":
                return left;

            case "Right":
                return right;

            case "Attack":
                return attack;

            case "Pause":
                return pause;

            default:
                return KeyCode.None;    
        }
    }
}

