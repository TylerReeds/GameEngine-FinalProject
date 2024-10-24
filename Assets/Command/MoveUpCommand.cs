using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpCommand : ICommand
{
    private PlayerController _playerController;

    public MoveUpCommand(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Execute()
    {
        _playerController.MoveUp();
    }
}
