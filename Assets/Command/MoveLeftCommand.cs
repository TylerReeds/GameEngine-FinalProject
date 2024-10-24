using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private PlayerController _playerController;

    public MoveLeftCommand(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Execute()
    {
        _playerController.MoveLeft();
    }
}
