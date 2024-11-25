using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    public GameObject player;
    public PlayerController controller;

    public GameObject gameManagerObj;
    public GameManager gameManager;

    public int damage;

    private void Awake()
    {
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Child classes will override this and do x thing with Player
    }
}
