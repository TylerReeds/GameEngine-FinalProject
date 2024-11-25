using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Observer
{
    private int currentHP;
    private PlayerController player;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(925, 150, 100, 200));

        // Display score
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Player Health: " + currentHP);
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    public override void Notify(Subject subject)
    {
        if (subject is PlayerController player)
        {
            currentHP = player.PlayerHP; // Update UIManager's HP value
            Debug.Log("Player HP updated to " + currentHP);
        }
    }
}