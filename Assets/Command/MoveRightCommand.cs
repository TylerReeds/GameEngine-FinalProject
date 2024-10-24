using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightCommand : ICommand
{
    private PlayerController _playerController;

    public MoveRightCommand(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Execute()
    {
        _playerController.MoveRight();
    }
}
