using System;
using UnityEngine;

/// <summary>
/// Snake controller
/// </summary>
public class InputHandler : MonoBehaviour
{
    private SnakeController _snakeController;
    private CommandInvoker _commandInvoker;
    private UIController _uiController;
    private MoveDirection _currentMoveDirection = MoveDirection.Right;
    private MoveDirection _nextMoveDirection = MoveDirection.Right;

    public void Initialize(SnakeController snakeController, UIController uiController, CommandInvoker commandInvoker)
    {
        _uiController = uiController;
        _snakeController = snakeController;
        _commandInvoker = commandInvoker;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var command = new ShowHideMenuCommand(_uiController);
            _commandInvoker.ExecuteCommand(command);
        }
        
        if (UpdateInput())
            OnInputChanged();
    }

    private bool UpdateInput()
    {
        //using horizontal and vertical so its usable on joystick too
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        
        if (x > 0)
        {
            _nextMoveDirection = MoveDirection.Right;
            return true;
        }
        
        if (x < 0)
        {
            _nextMoveDirection = MoveDirection.Left;
            return true;
        }

        if (y > 0)
        {
            _nextMoveDirection = MoveDirection.Up;
            return true;
        }
        
        if (y < 0)
        {
            _nextMoveDirection = MoveDirection.Down;
            return true;
        }
        
        return false;
    }

    private void OnInputChanged()
    {
        var command = new MoveCommand(_snakeController, _nextMoveDirection);
        _commandInvoker.ExecuteCommand(command);
        
        _currentMoveDirection = _nextMoveDirection;
    }
}
