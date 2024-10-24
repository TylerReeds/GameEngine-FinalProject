using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoCommand : ICommand
{
    private PlayerController _playerController;

    public UndoCommand(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Execute()
    {
        _playerController.Undo();
    }
}
