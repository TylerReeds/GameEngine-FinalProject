using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoCommand : ICommand
{
    private PlayerController _playerController;

    public RedoCommand(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Execute()
    {
        _playerController.Redo();
    }
}
