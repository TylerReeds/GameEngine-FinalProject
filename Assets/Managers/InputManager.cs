using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Keybindings keybindings; 

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this); 
    }

    public bool GetKeyDown(string key)
    {
        if (Input.GetKeyDown(keybindings.CheckKey(key)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetKey(string key)
    {
        if (Input.GetKey(keybindings.CheckKey(key)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
