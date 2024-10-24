using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownCommand : ICommand
{
    private PlayerController _playerController;

    public MoveDownCommand(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Execute()
    {
        _playerController.MoveDown();
    }
}
